using System.Collections;
using UnityEngine;

namespace Dialogue {
    public class AudioNode : BaseNode {

        [Input][HideInInspector] public int entry;
        [Output][HideInInspector] public int exit;

        [HideInInspector] public AudioClip audioSound;
        [HideInInspector] public float volume;

        public override IEnumerator Run() {
            DialogueUIManager.Instance.PlaySound(audioSound, this);
            yield return null;
            NextNode("exit");
        }
    }
}

