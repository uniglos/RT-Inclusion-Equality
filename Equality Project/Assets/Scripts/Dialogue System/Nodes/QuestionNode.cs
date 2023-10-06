using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class QuestionNode : BaseNode {
        [Input()] public int entry;

		public int characterNameIndex;
		//public string character; 
		public Color nameColour;
        public string speech;
        public Color textColour;

        [Output(dynamicPortList = true)] public List<string> exits = new List<string>();

        public override IEnumerator Run() {
            DialogueUIManager.Instance.DisplayText(this);
            DialogueUIManager.Instance.DisplayButtons(this);
            DialogueUIManager.Instance.SetColour(nameColour, textColour);
            yield return null;
        }
    }
}