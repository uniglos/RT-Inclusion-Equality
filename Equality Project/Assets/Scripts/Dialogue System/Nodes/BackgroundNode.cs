using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using static XNode.Node;

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

