using System.Collections;
using UnityEngine;

namespace Dialogue {
    public class DialogueBaseNode : BaseNode {

        [Input()][HideInInspector] public int entry;

        [HideInInspector] public int characterNameIndex;
        [HideInInspector] public string speech;
        [HideInInspector] public Color nameColour;
        [HideInInspector] public Color textColour;

        public float FontSize;

        protected override void Create() {
            if(FontSize <= 0)
                FontSize = 45.0f;
        }

        public override IEnumerator Run() {
            DisplayUI();
            yield return null;
            CallNextNode();
        }

        /// <summary>
        /// Display this DialogueNode UI
        /// </summary>
        protected virtual void DisplayUI() {
            DialogueUIManager.Instance.SetFontSize(FontSize);
            DialogueUIManager.Instance.ClearButton();
            DialogueUIManager.Instance.DisplayText(this);
            DialogueUIManager.Instance.SetColour(nameColour, textColour);
        }

        protected virtual void CallNextNode() { }
    }
}