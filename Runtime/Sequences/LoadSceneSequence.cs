using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace AnimationUI.Sequences
{
    /// <summary>
    /// Sequence used to load a scene
    /// </summary>
    [Serializable]
    public class LoadSceneSequence : Sequence
    {
        /// <summary>
        /// Name of the scene to load
        /// </summary>
        public string sceneToLoad = "";

        /// <inheritdoc/>
        public override IEnumerator Play()
        {
            var operation = SceneManager.LoadSceneAsync(sceneToLoad);
            
            if (operation == null)
                yield break;
            
            yield return operation;
        }
    }
}