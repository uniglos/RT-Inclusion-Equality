using System.Collections;
using UnityEngine;

namespace Dialogue {
    public class SoundEffectNode : BaseNode {

        [Input][HideInInspector] public int entry;
        [Output][HideInInspector] public int exit;

        [HideInInspector] public AudioClip audioSound;
        [HideInInspector] public float volume = 1f;

        public override IEnumerator Run() {
            DialogueUIManager.Instance.PlaySoundEffect(audioSound, this);
            yield return null;
            NextNode("exit");
        }
    }
}

