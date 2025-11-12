using System.Collections;
using AnimationUI.Sequences;
using UnityEngine;

namespace AnimationUI
{
    public class AnimationPlayer : MonoBehaviour
    {
        [SerializeReference]
        public Sequence[] sequences = { };

        private void Start()
        {
            StartCoroutine(Play());
        }

        private IEnumerator Play()
        {
            foreach (var sequence in sequences)
                yield return sequence.Play();
        }
    }
}