using System.Collections;
using UnityEngine;

namespace Dialogue {
    public class BackgroundNode : BaseNode {

        [Input][HideInInspector] public int entry;
        [Output][HideInInspector] public int exit;

        [HideInInspector] public Texture2D background;

        public override IEnumerator Run() {
            DialogueUIManager.Instance.LoadBackground(background);
            yield return null;
            NextNode("exit");
        }
    }
}

