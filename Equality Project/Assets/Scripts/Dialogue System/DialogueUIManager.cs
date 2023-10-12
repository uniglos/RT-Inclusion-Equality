using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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

		[SerializeField] private Image fingerIcon;

		[Header("Character Images")]
		[SerializeField] private List<Image> images = new List<Image>();
		[Header("Background Image")]
		[SerializeField] private Image background;

		public bool ShouldRefresh { get; set; }

		public bool waitedForMouse = false;

		private List<GameObject> buttons = new List<GameObject>();

		private Color fingerColour = Color.white;
		private Color textColour = Color.white;
		private Color nameColour = Color.white;

		CharacterNames characterNames;

		public Button tapButton;

		[Header("Text Settings")]
		private string itemInfo;
		[SerializeField] private float textSpeed = 0.01f;

		//private int currentDisplayingText = 0;


		private void Awake() {
			if (Instance == null) {
				Instance = this;
			} else {
				Destroy(Instance);
			}

			characterNames = AssetDatabase.LoadAssetAtPath<CharacterNames>("Assets/Scripts/Dialogue System/ScriptableObjects/CharacterNames.asset");
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
		/// Displays Text on the screen
		/// </summary>
		public void DisplayText(BaseNode node) {
			if (node is QuestionNode questionNode) {
				characterText.text = characterNames.list[questionNode.characterNameIndex];
				speechText.text = questionNode.speech;
			} else if (node is DialogueNode dialogueNode) {
				characterText.text = characterNames.list[dialogueNode.characterNameIndex];
				speechText.text = dialogueNode.speech;
			}
		}

		IEnumerator AnimateText() {
			for (int i = 0; i < itemInfo.Length + 1; i++) {
				speechText.text = itemInfo.Substring(0, i);
				yield return new WaitForSeconds(textSpeed);
			}
		}

		public void ClearButton() {
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
			fingerColour = colour;
			fingerIcon.GetComponent<Image>().color = new Color(fingerColour.r, fingerColour.g, fingerColour.b, fingerColour.a);
		}

		public void SetColour(Color Ncolour, Color colour) {
			nameColour = Ncolour;
			characterText.GetComponent<TMPro.TMP_Text>().color = new Color(nameColour.r, nameColour.g, nameColour.b, 1);
			textColour = colour;
			speechText.GetComponent<TMPro.TMP_Text>().color = new Color(textColour.r, textColour.g, textColour.b, 1);
		}

		public void SetMouseIconActive(bool active) {
			if (fingerIcon) {
				fingerIcon.enabled = active;
			}
		}

	}

}

