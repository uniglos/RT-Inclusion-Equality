using System.Collections;
using UnityEngine;

namespace Dialogue {
    public class BGMNode : BaseNode {

        [Input][HideInInspector] public int entry;
        [Output][HideInInspector] public int exit;

        [HideInInspector] public AudioClip audioSound;
        [HideInInspector] public float volume = 1f;

        public override IEnumerator Run() {
            BGMManager.Instance.PlaySoundBGM(audioSound, this);
            yield return null;
            NextNode("exit");
        }
    }
}

