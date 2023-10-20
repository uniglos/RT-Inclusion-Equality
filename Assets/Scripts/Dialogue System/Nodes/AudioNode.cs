using System.Collections;
using UnityEngine;

namespace Dialogue {
    public class AudioNode : BaseNode {

        [Input][HideInInspector] public int entry;
        [Output][HideInInspector] public int exit;

        [HideInInspector] public AudioClip audioSound;

        public override IEnumerator Run() {
            DialogueUIManager.Instance.PlaySound(audioSound);
            yield return null;
            NextNode("exit");
        }
    }
}

