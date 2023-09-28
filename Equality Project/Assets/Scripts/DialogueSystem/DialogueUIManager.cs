using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XnodeDialogue;


public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager Instance { get; private set; }

    [Header("Dialogue UI Elements")]
    [SerializeField] private TMPro.TMP_Text characterText;
    [SerializeField] private TMPro.TMP_Text speechText;

    [SerializeField] private GameObject buttonObject;
    [SerializeField] private Transform buttonHolder;

    [Header("Character Images")]
    [SerializeField] private List<Image> images = new List<Image>();

    private List<GameObject> buttons = new List<GameObject>();

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(Instance);
        }
    }

    /// <summary>
    /// Dislays the Text and Buttons (if any) on the screen
    /// </summary>
    /// <param name="node">The current selected node</param>
    public void Draw(BaseNode node) {

        if(Instance == null) {
            DialogueGraphLogger.Log("UI Manager Instance: Has not been created, please add it to the scene!", DialogueGraphLogger.ELogError.Error);
            return;
        }
        
        //UI Elements should be drawn within the StartRefresh and EndRefresh functions
        StartRefresh();
        
        if(node is DialogueNode) {
            DisplayText(node);
            DisplayButtons(node);
        }

        if(node is CharactersNode) {
            DisplayImages(node);
        }

        EndRefresh();
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
        foreach(var image in images) {
            image.sprite = null;    
        }   
    }

    /// <summary>
    /// Clears the old UI of the screen
    /// </summary>
    private void StartRefresh() {
        buttons.Clear();

        foreach(Transform child in buttonHolder) {
            Destroy(child.gameObject);
        }

        characterText.text = string.Empty;
        speechText.text = string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    private void EndRefresh() {

    }

    /// <summary>
    /// Dispalys Text on the screen
    /// </summary>
    private void DisplayText(BaseNode node) {
        DialogueNode dialogueNode = (DialogueNode)node;
        characterText.text = dialogueNode.characterName;
        speechText.text = dialogueNode.speech;
    }

    /// <summary>
    /// Displays the buttons on the screen
    /// </summary>
    private void DisplayButtons(BaseNode node) {
        DialogueNode dialogueNode = (DialogueNode)node;

        GraphRunner runner = FindObjectOfType<GraphRunner>();

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
            button.GetComponent<Button>().onClick.AddListener(() => { AnswerButton(runner, bIndex); } ) ;
            index++;
        }
    }

    private void DisplayImages(BaseNode node) {
        if(node is not CharactersNode) {
            return;
        }

        CharactersNode charactersNode = (CharactersNode)node;

        ClearAllImages();
        
        LoadImageAtIndex(0, charactersNode.imageL);
        LoadImageAtIndex(1, charactersNode.imageM);
        LoadImageAtIndex(2, charactersNode.imageR);
    }

    private void AnswerButton(GraphRunner runner, int index) {
        //Calls the answer dialogue function in the graph runner
        runner.currentNode = runner.AnswerDialogue(index);
        //Resets the dialogue
        Draw(runner.currentNode);
    }

    /// <summary>
    /// Returns an Image at the index (0 = left, 1 = centre, 2 = right)
    /// </summary>
    private Image LoadImageAtIndex(int index, Texture2D imageSprite) {
        if (images.Count < 0) {
            DialogueGraphLogger.Log("Loading Image Fatal Error: The image array on UIManager object has not be assigned!", DialogueGraphLogger.ELogError.Error);
            return null;
        }

        Image image = images[index];
        image.sprite = Sprite.Create(imageSprite, new Rect(0, 0, imageSprite.width, imageSprite.height), Vector2.zero);
        return image;
    }

    /// <summary>
    /// Returns an Image at the index (0 = left, 1 = centre, 2 = right)
    /// </summary>
    private Image LoadImagesAtIndex(int index, Sprite[] imageSprites) {
        if (images.Count < 0 || index < images.Count || images.Count > index) {
            DialogueGraphLogger.Log("Loading Image Fatal Error: The image array on UIManager object has not be assigned!", DialogueGraphLogger.ELogError.Error);
            return null;
        }

        int amount = 0;

        foreach (Sprite sprite in imageSprites) {
            if (amount > 2) {
                continue;
            }

            images[amount].sprite = sprite;
            amount++;
        }

        return images[index];
    }
}
