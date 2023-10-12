using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XNodeEditor;

namespace Dialogue {
    public class DialogueNode : BaseNode {

        [Input] public int entry;
        [Output] public int exit;

        public int characterNameIndex;
        public string speech;
        public Color nameColour;
		public Color textColour;
		public Color fingerColour;

		public override IEnumerator Run() {
            DialogueUIManager.Instance.ClearButton();
            DialogueUIManager.Instance.SetMouseIconActive(true);
            DialogueUIManager.Instance.DisplayText(this);
            DialogueUIManager.Instance.ChangeColour(fingerColour);
            DialogueUIManager.Instance.SetColour(nameColour, textColour);
            yield return null;
            DialogueUIManager.Instance.tapButton.onClick.AddListener(() => 
            {
                DialogueUIManager.Instance.SetMouseIconActive(false);
                NextNode("exit");
            });
        }
    }
}


