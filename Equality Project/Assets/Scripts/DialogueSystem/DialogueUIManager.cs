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
    public void DisplayDialogue(BaseNode node) {

        if(Instance == null) {
            DialogueGraphLogger.Log("UI Manager Instance: Has not been created, please add it to the scene!", DialogueGraphLogger.ELogError.Error);
            return;
        }
        
        if(node is DialogueNode) {

            //UI Elements should be drawn within the StartRefresh and EndRefresh functions
            StartRefresh();

            DisplayText(node);
            DisplayButtons(node);

            EndRefresh();
        }

    }

    /// <summary>
    /// Ends the Dialogue
    /// </summary>
    public void EndDialogue() {

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

    private void AnswerButton(GraphRunner runner, int index) {
        //Calls the answer dialogue function in the graph runner
        runner.currentNode = runner.AnswerDialogue(index);
        //Resets the dialogue
        DisplayDialogue(runner.currentNode);
    }
}
