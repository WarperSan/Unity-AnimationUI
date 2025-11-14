using System;
using System.Collections.Generic;
using AnimationUI.Editor.Elements;
using UnityEditor;

namespace AnimationUI.Editor.SequenceViewModifiers
{
    /// <summary>
    /// Class allowing to modify a <see cref="SequenceView"/>
    /// </summary>
    public abstract class SequenceViewModifier
    {
        /// <summary>
        /// Applies the modifications to the given view
        /// </summary>
        protected abstract void Modify(SequenceView view, SerializedProperty sequence);
        
        static SequenceViewModifier()
        {
            BuildViewModifiersCache();
        }

        /// <summary>
        /// Cache of every <see cref="SequenceViewModifier"/> with their appropriate <see cref="Sequences.Sequence"/>
        /// </summary>
        private static readonly Dictionary<Type, Type> ViewModifierPerType = new();

        /// <summary>
        /// Updates <see cref="ViewModifierPerType"/>
        /// </summary>
        private static void BuildViewModifiersCache()
        {
            ViewModifierPerType.Clear();

            var modifiers = TypeCache.GetTypesDerivedFrom<SequenceViewModifier>();
            
            foreach (var modifier in modifiers)
            {
                if (modifier.IsAbstract)
                    continue;

                // ReSharper disable once UseNegatedPatternMatching
                var attributes = modifier.GetCustomAttributes(
                    typeof(SequenceCustomEditorAttribute),
                    false
                ) as SequenceCustomEditorAttribute[];
                
                if (attributes == null || attributes.Length == 0)
                    continue;

                ViewModifierPerType.TryAdd(attributes[0].SequenceType, modifier);
            }
        }

        /// <summary>
        /// Modifies the given view using the modifier associated with the given data 
        /// </summary>
        public static void ModifySequenceView(SequenceView view, SerializedProperty sequenceProperty, string typeName)
        {
            foreach ((var sequenceType, var modifierType) in ViewModifierPerType)
            {
                if (sequenceType.FullName != typeName)
                    continue;

                var modifier = Activator.CreateInstance(modifierType) as SequenceViewModifier;

                modifier?.Modify(view, sequenceProperty);
            }
        }
    }
}