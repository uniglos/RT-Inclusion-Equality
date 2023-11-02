using UnityEngine;

namespace Dialogue {
    public class DialogueNode : DialogueBaseNode {

        [Output][HideInInspector] public int exit;

        [HideInInspector] public Color fingerColour;

        protected override void DisplayUI() {
            base.DisplayUI();
            DialogueUIManager.Instance.SetMouseIconActive(true);
            DialogueUIManager.Instance.ChangeColour(fingerColour);
        }

        protected override void CallNextNode() {
            DialogueUIManager.Instance.TapButton.onClick.AddListener(() =>
            {
                DialogueUIManager.Instance.SetMouseIconActive(false);
                NextNode("exit");
                DialogueUIManager.Instance.TapButton.onClick.RemoveAllListeners(); //Removes the nodes duplicating.
            });
        }
    }
}


