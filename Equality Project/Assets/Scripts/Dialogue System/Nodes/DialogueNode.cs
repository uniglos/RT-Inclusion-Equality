using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Dialogue {
    public class DialogueNode : BaseNode {

        [Input] public int entry;
        [Output] public int exit;

        public string character;
        public string speech;
        public Color fingerColour;
        public Color textColour;
        public Color nameColour;

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


