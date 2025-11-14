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
            SetBackground(0, 0, 0, 0);
            SetTitle("");
            SetBody(null);
            
            var type = property.managedReferenceFullTypename.Split(' ')[^1].Replace('/', '+');

            SequenceViewModifiers.SequenceViewModifier.ModifySequenceView(
                this,
                property,
                type
            );
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

        /// <summary>
        /// Sets the given element as the body of this view
        /// </summary>
        public void SetBody([CanBeNull] VisualElement content)
        {
            if (content == null)
                _foldout.Clear();
            else
                _foldout.Add(content);
        }

        #endregion
    }
}