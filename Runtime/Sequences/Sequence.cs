using System;
using System.Collections;

namespace AnimationUI.Sequences
{
    /// <summary>
    /// Element representing a transformation
    /// </summary>
    [Serializable]
    public abstract class Sequence
    {
        /// <summary>
        /// Plays the transformation
        /// </summary>
        public abstract IEnumerator Play();
    }
}