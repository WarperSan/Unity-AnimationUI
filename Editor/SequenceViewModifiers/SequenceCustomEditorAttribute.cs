using System;
using AnimationUI.Sequences;

namespace AnimationUI.Editor.SequenceViewModifiers
{
    /// <summary>
    /// Attribute defining which <see cref="Sequence"/> to use for a specific <see cref="SequenceViewModifier"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SequenceCustomEditorAttribute : Attribute
    {
        /// <summary>
        /// Type of the sequence targeted by this editor
        /// </summary>
        public readonly Type SequenceType;

        public SequenceCustomEditorAttribute(Type sequenceType)
        {
            SequenceType = sequenceType;
        }
    }
}