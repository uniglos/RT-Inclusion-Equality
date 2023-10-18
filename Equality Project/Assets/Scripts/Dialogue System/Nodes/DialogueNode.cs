using UnityEngine;

namespace Dialogue {
    public class DialogueNode : DialogueBaseNode {

        [Output][HideInInspector] public int exit;

        [HideInInspector] public Color fingerColour;

        protected override void DisplayUI() {
            base.DisplayUI();
            DialogueUIManager.Instance.SetMouseIconActive(true);
            DialogueUIManager.Instance.ChangeColour(fingerColour);
<<<<<<< Updated upstream
        }

        protected override void CallNextNode() {
            DialogueUIManager.Instance.TapButton.onClick.AddListener(() => {
=======
            DialogueUIManager.Instance.SetColour(nameColour, textColour);
            yield return null;
            DialogueUIManager.Instance.tapButton.onClick.AddListener(() => 
            {
                //DialogueUIManager.Instance.ClearingText();
>>>>>>> Stashed changes
                DialogueUIManager.Instance.SetMouseIconActive(false);
                NextNode("exit");
            });
        }
    }
}


