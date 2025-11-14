using System;
using System.Collections;
using UnityEngine.Events;

namespace AnimationUI.Sequences
{
    /// <summary>
    /// Sequence used to call other methods
    /// </summary>
    [Serializable]
    public class UnityEventSequence : Sequence
    {
        /// <summary>
        /// Event to call
        /// </summary>
        public UnityEvent @event;

        /// <inheritdoc/>
        public override IEnumerator Play()
        {
            @event.Invoke();
            yield return null;
        }
    }
}