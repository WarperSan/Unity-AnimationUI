using AnimationUI.Sequences;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AnimationUI.Editor.Elements
{
    /// <summary>
    /// Element used to display a <see cref="Sequence"/>
    /// </summary>
    public class SequenceView : VisualElement
    {
        private readonly VisualElement _background;
        private readonly Foldout _foldout;

        public SequenceView()
        {
            _background = new VisualElement
            {
                style =
                {
                    position = Position.Absolute,
                    left = 0,
                    top = 0,
                    right = 0,
                    bottom = 0
                }
            };
            Add(_background);

            _foldout = new Foldout
            {
                toggleOnLabelClick = false
            };
            _foldout.contentContainer.style.paddingLeft = 0;

            _foldout.RegisterValueChangedCallback(e =>
            {
                _foldout.contentContainer.style.display = e.newValue ? DisplayStyle.Flex : DisplayStyle.None;
            });
            Add(_foldout);

            _background.SendToBack();
        }

        /// <summary>
        /// Updates this element to use the given item
        /// </summary>
        public void Bind(SerializedProperty property)
        {
            var type = property.managedReferenceFullTypename.Split(' ')[^1].Replace('/', '+');

            if (type == typeof(LoadSceneSequence).FullName)
            {
                var sceneProp = property.FindPropertyRelative(nameof(LoadSceneSequence.sceneToLoad));

                var sceneField = new PropertyField();
                sceneField.Bind(property.serializedObject);
                sceneField.bindingPath = sceneProp.propertyPath;

                _foldout.Add(sceneField);

                SetBackground(new Color(0.6f,
                    0.3f,
                    0f,
                    0.1f));

                sceneField.TrackPropertyValue(sceneProp, UpdateTitle);
                UpdateTitle(sceneProp);

                void UpdateTitle(SerializedProperty prop)
                {
                    SetTitle($"Load '{prop.stringValue}'");
                }
            }
        }

        /// <summary>
        /// Clears this element of any information
        /// </summary>
        public void Unbind()
        {
            SetBackground(new Color(0,
                0,
                0,
                0));
            SetTitle("");
            _foldout.Clear();
        }

        #region Utils

        /// <summary>
        /// Sets the color of the background
        /// </summary>
        public void SetBackground(byte r, byte g, byte b, byte a = 255)
        {
            _background.style.backgroundColor = new Color(
                r / 255f,
                g / 255f,
                b / 255f,
                a / 255f
            );
        }

        /// <summary>
        /// Sets the title of this element
        /// </summary>
        public void SetTitle(string title)
        {
            _foldout.text = title;
        }

        #endregion
    }
}