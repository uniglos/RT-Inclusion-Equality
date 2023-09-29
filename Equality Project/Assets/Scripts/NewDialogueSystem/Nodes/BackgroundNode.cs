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
    public class BackgroundNode : BaseNode {

        [Input] public int entry;
        [Output] public int exit;

        public Texture2D background;

        public override void Run() {
            DialogueUIManager.Instance.LoadBackground(background);
            NextNode("exit");
        }
    }
}

