using AnimationUI.Editor.Elements;
using AnimationUI.Sequences;
using UnityEditor;
using UnityEditor.UIElements;

namespace AnimationUI.Editor.SequenceViewModifiers
{
    [SequenceCustomEditor(typeof(LoadSceneSequence))]
    public class LoadSceneSequenceModifier : SequenceViewModifier
    {
        /// <inheritdoc/>
        protected override void Modify(SequenceView view, SerializedProperty sequence)
        {
            var sceneProp = sequence.FindPropertyRelative(nameof(LoadSceneSequence.sceneToLoad));

            var sceneField = new PropertyField();
            sceneField.Bind(sequence.serializedObject);
            sceneField.bindingPath = sceneProp.propertyPath;

            view.SetBody(sceneField);
            view.SetBackground(153, 76, 0, 25);

            sceneField.TrackPropertyValue(sceneProp, p => UpdateTitle(p, view));
            UpdateTitle(sceneProp, view);
        }
        
        private static void UpdateTitle(SerializedProperty prop, SequenceView view)
        {
            view.SetTitle($"Load '{prop.stringValue}'");
        }
    }
}