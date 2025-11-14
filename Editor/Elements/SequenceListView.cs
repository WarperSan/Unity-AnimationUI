using System.Collections.Generic;
using AnimationUI.Sequences;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AnimationUI.Editor.Elements
{
    /// <summary>
    /// Element used to display a list of <see cref="Sequence"/>
    /// </summary>
    public class SequenceListView : ListView
    {
        private readonly SerializedProperty _property;

        public SequenceListView(SerializedProperty property)
        {
            _property = property;
            headerTitle = "Sequences";

            allowAdd = true;
            allowRemove = true;
            showAddRemoveFooter = true;
            showFoldoutHeader = true;
            showBorder = true;
            reorderable = true;
            selectionType = SelectionType.Multiple;

            style.maxHeight = 500f;
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;

            onAdd = OnAdd;
            onRemove = OnRemove;
            makeItem = MakeItem;
            bindItem = BindItem;

            RefreshList();
        }

        #region List Management

        private void RefreshList()
        {
            var list = new List<SerializedProperty>();

            for (var i = 0; i < _property.arraySize; i++)
                list.Add(_property.GetArrayElementAtIndex(i));

            itemsSource = list;
        }

        private void AddItem(Sequence sequence)
        {
            _property.InsertArrayElementAtIndex(_property.arraySize);
            _property.GetArrayElementAtIndex(_property.arraySize - 1).managedReferenceValue = sequence;
            _property.serializedObject.ApplyModifiedProperties();
            RefreshList();
        }

        private void RemoveItem(int index)
        {
            _property.DeleteArrayElementAtIndex(index);
            _property.serializedObject.ApplyModifiedProperties();
            RefreshList();
        }

        #endregion

        /// <summary>
        /// Called when the user uses the <c>Add</c> button
        /// </summary>
        private void OnAdd(BaseListView view)
        {
            Sequence newItem = new LoadSceneSequence();

            AddItem(newItem);
            RefreshItem(_property.arraySize - 1);
            ScrollToItem(_property.arraySize - 1);
        }

        /// <summary>
        /// Called when the user uses the <c>Remove</c> button
        /// </summary>
        private void OnRemove(BaseListView view)
        {
            if (itemsSource.Count == 0)
                return;

            RemoveItem(itemsSource.Count - 1);
            ScrollToItem(itemsSource.Count - 1);
        }

        /// <summary>
        /// Creates an empty element for the list
        /// </summary>
        private static VisualElement MakeItem() => new SequenceView();

        /// <summary>
        /// Updates the element for the item at the given index
        /// </summary>
        private void BindItem(VisualElement element, int index)
        {
            if (element is not SequenceView sequenceView)
            {
                Debug.LogWarning($"Cannot bind an element that is not a {typeof(SequenceView)}.");
                return;
            }

            var item = itemsSource[index];

            if (item is not SerializedProperty serializedProperty)
            {
                Debug.LogWarning($"Cannot bind an element with something that is not a {nameof(SerializedProperty)}.");
                return;
            }

            sequenceView.Bind(serializedProperty);
        }
    }
}