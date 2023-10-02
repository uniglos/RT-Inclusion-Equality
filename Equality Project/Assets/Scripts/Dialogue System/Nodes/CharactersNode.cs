using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using static XNode.Node;

/// <summary>
/// FIX On the dialogue node remove the Answer Questions and implement the NextNode function
/// </summary>

namespace Dialogue {
    public class CharactersNode : BaseNode {

        [Input] public int entry;
        [Output] public int exit;

        public Texture2D imageLeft;
        public Texture2D imageMiddle;
        public Texture2D imageRight;

        public override IEnumerator Run() {
            DialogueUIManager.Instance.DisplayImages(this);
            NextNode("exit");
            yield return null;
        }
    }
}

