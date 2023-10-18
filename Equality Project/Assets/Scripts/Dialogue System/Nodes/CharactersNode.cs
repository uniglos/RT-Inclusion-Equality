using System.Collections;
using UnityEngine;

namespace Dialogue {
    public class CharactersNode : BaseNode {

        [Input][HideInInspector] public int entry;
        [Output][HideInInspector] public int exit;

        [HideInInspector] public Texture2D imageLeft;
        [HideInInspector] public Texture2D imageMiddle;
        [HideInInspector] public Texture2D imageRight;

        public override IEnumerator Run() {
            DialogueUIManager.Instance.DisplayImages(this);
            NextNode("exit");
            yield return null;
        }
    }
}

