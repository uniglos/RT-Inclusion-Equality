using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace Dialogue {
    public class DialogueUIManager : MonoBehaviour {
        public static DialogueUIManager Instance { get; private set; }

        [Header("Dialogue UI Elements")]
        [SerializeField] private TMPro.TMP_Text characterText;
        [SerializeField] private TMPro.TMP_Text speechText;

        [SerializeField] private GameObject buttonObject;
        [SerializeField] private Transform buttonHolder;

        [SerializeField] private Image mouseIcon;

        [Header("Character Images")]
        [SerializeField] private List<Image> images = new List<Image>();
        [Header("Background Image")]
        [SerializeField] private Image background;

        public bool ShouldRefresh { get; set; }

        private List<GameObject> buttons = new List<GameObject>();

        private Color mouseColour = Color.white;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(Instance);
            }
        }

        /// <summary>
        /// Ends the Dialogue
        /// </summary>
        public void EndDialogue() {

        }

        public void ClearImageAtIndex(int index) {
            images[index].sprite = null;
        }

        public void ClearAllImages() {
            foreach (var image in images) {
                image.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Dispalys Text on the screen
        /// </summary>
        public void DisplayText(BaseNode node) {
            if (node is QuestionNode) {
                QuestionNode dialogueNode = (QuestionNode)node;
                characterText.text = dialogueNode.character;
                speechText.text = dialogueNode.speech;
            } else if (node is DialogueNode) {
                DialogueNode dialogueNode = (DialogueNode)node;
                characterText.text = dialogueNode.character;
                speechText.text = dialogueNode.speech;
            }
        }

        public void ClearButton()
        {
            buttons.Clear();

            foreach (Transform child in buttonHolder) {
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Displays the buttons on the screen
        /// </summary>
        public void DisplayButtons(BaseNode node) {
            QuestionNode dialogueNode = (QuestionNode)node;

            buttons.Clear();

            foreach (Transform child in buttonHolder) {
                Destroy(child.gameObject);
            }

            int index = 0;

            //Loops through the exits in the dialogue node
            foreach (string n in dialogueNode.exits) {
                //Creates the button on the UI
                GameObject button = Instantiate(buttonObject, buttonHolder);
                buttons.Add(button);
                //Unique index for each button
                int bIndex = index;
                button.GetComponentInChildren<TMPro.TMP_Text>().text = n;
                //Button event for answering buttons
                button.GetComponent<Button>().onClick.AddListener(() => { AnswerButton(node, bIndex); });
                index++;
            }
        }

        public void DisplayImages(BaseNode node) {
            if (node is not CharactersNode) {
                return;
            }

            CharactersNode charactersNode = (CharactersNode)node;

            ClearAllImages();

            if (charactersNode.imageLeft != null) {
                LoadImageAtIndex(0, charactersNode.imageLeft);
            }
            if (charactersNode.imageMiddle != null) {
                LoadImageAtIndex(1, charactersNode.imageMiddle);
            }
            if (charactersNode.imageRight != null) {
                LoadImageAtIndex(2, charactersNode.imageRight);
            }
        }


        private void AnswerButton(BaseNode node, int index) {
            node.NextNode("exits " + index);
        }

        /// <summary>
        /// Returns an Image at the index (0 = left, 1 = centre, 2 = right)
        /// </summary>
        private Image LoadImageAtIndex(int index, Texture2D imageSprite) {
            if (images.Count < 0) {
                return null;
            }

            Image image = images[index];
            image.sprite = Sprite.Create(imageSprite, new Rect(0, 0, imageSprite.width, imageSprite.height), Vector2.zero);
            image.gameObject.SetActive(true);
            return image;
        }

        public Image LoadBackground(Texture2D backgroundSprite) {

            if (backgroundSprite != null) {
                background.sprite = Sprite.Create(backgroundSprite, new Rect(0, 0, backgroundSprite.width, backgroundSprite.height), Vector2.zero);
            }

            return background;
        }

        public void ChangeColour(Color colour) {
            mouseColour = colour;
            mouseIcon.GetComponent<Image>().color = new Color(mouseColour.r, mouseColour.g, mouseColour.b, mouseColour.a);
        }

        public void SetMouseIconActive(bool active) {
            if (mouseIcon) {
                mouseIcon.enabled = active;
            }
        }
    }

}

