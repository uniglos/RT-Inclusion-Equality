using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Dialogue {
    public class DialogueBaseNode : BaseNode {

        [Input()][HideInInspector] public int entry;

        public bool ShowTextScrolling = true;

        [HideInInspector] public float textSpeed = 1.0f;

        [HideInInspector] public int characterNameIndex;
        [HideInInspector] public string speech;
        [HideInInspector] public Color nameColour;
        [HideInInspector] public Color textColour;

        public float FontSize;

        public bool isFinishedGeneratingTextColour = false;

        public string newSpeech = String.Empty;

        protected override void Create() {
            if(FontSize <= 0)
                FontSize = 45.0f;
        }

        public override IEnumerator Run() {
            CheckText();
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

        protected virtual void CallNextNode()
        {
        }

        public void CheckText()
        {
            HighlightWord();
        }

        void HighlightWord()
        {
            string wordToHighlight = "Equality";

            speech = speech.Replace("\n", string.Empty);

            newSpeech = speech;

            string[] words = newSpeech.Split(' ');

            foreach (string word in words) {

                if (word == wordToHighlight) // Check if the current word matches the wordToHighlight
                {
                    string newWord = "<color=#00ff44>" + wordToHighlight + "</color>";

                    newSpeech = newSpeech.Replace(wordToHighlight, newWord);
                }
            }

            isFinishedGeneratingTextColour = true;
        }
    }
}