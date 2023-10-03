using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XNodeEditor;

namespace Dialogue {
    public class DialogueNode : BaseNode {

        [Input] public int entry;
        [Output] public int exit;

        public string character;
        public string speech;
        public Color mouseColour;

        public override IEnumerator Run() {
            DialogueUIManager.Instance.SetMouseIconActive(true);
            DialogueUIManager.Instance.DisplayText(this);
            DialogueUIManager.Instance.ChangeColour(mouseColour);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            NextNode("exit");
            DialogueUIManager.Instance.SetMouseIconActive(false);
        }


        
    }
}


