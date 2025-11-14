using AnimationUI.Editor.Elements;
using AnimationUI.Sequences;
using UnityEditor;
using UnityEditor.UIElements;

namespace AnimationUI.Editor.SequenceViewModifiers
{
    [SequenceCustomEditor(typeof(UnityEventSequence))]
    public class UnityEventSequenceModifier : SequenceViewModifier
    {
        /// <inheritdoc/>
        protected override void Modify(SequenceView view, SerializedProperty sequence)
        {
            var eventProp = sequence.FindPropertyRelative(nameof(UnityEventSequence.@event));

            var eventField = new PropertyField(eventProp);
            eventField.Bind(sequence.serializedObject);
            eventField.bindingPath = eventProp.propertyPath;
            
            view.SetBackground(0, 255, 255, 25);
            view.SetBody(eventField);
            
            eventField.TrackPropertyValue(eventProp, p => UpdateTitle(p, view));
            UpdateTitle(eventProp, view);
        }
        
        private static void UpdateTitle(SerializedProperty prop, SequenceView view)
        {
            var invokeCount = prop.FindPropertyRelative("m_PersistentCalls.m_Calls");

            view.SetTitle($"Call {invokeCount.arraySize} methods");
        }
    }
}