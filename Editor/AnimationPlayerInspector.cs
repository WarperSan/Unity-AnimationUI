using AnimationUI.Editor.Elements;
using UnityEditor;
using UnityEngine.UIElements;

namespace AnimationUI.Editor
{
    /// <summary>
    /// Inspector used to display nicely a <see cref="AnimationPlayer"/>
    /// </summary>
    [CustomEditor(typeof(AnimationPlayer))]
    public class AnimationPlayerInspector : UnityEditor.Editor
    {
        /// <inheritdoc/>
        public override VisualElement CreateInspectorGUI()
        {
            if (serializedObject.targetObject is not AnimationPlayer)
                return base.CreateInspectorGUI();

            var sequences = serializedObject.FindProperty(nameof(AnimationPlayer.sequences));

            var root = new VisualElement();

            var newListView = new SequenceListView(sequences);

            root.Add(newListView);

            return root;
        }
    }
}