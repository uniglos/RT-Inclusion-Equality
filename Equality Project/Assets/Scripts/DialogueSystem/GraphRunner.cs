using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XnodeDialogue;
using XNodeEditor;

public class GraphRunner : MonoBehaviour {

    [SerializeField] private DialogueGraph graph;

    [HideInInspector] public BaseNode currentNode = null;

    private void Start() {
        RunGraph();
    }

    public void RunGraph() {
        //Find the first node in the graph
        currentNode = graph.nodes.Find(n => n is StartNode) as BaseNode;
        //Gets next node to the start node and returns what the node type is
        currentNode = (currentNode as StartNode).NextNode();

        currentNode = (currentNode as BackgroundNode).NextNode();

        currentNode = (currentNode as CharactersNode).NextNode();

        if (currentNode is DialogueNode) {
            //Returns the dialogue node based on the button index which is cliked in the list
            DialogueUIManager.Instance.DisplayDialogue(currentNode);
        }

    }

    /// <summary>
    /// Returns the Dialogue node based on the connection to the index
    /// </summary>
    /// <param name="index">The index in the button panel which the user has pressed</param>
    public BaseNode AnswerDialogue(int index) {
        if (currentNode is DialogueNode) {
            currentNode = (currentNode as DialogueNode).AnswerQuestion(index);
            Debug.Log("Dia found");
            return currentNode as DialogueNode;
        } else if (currentNode is CharactersNode) {
            currentNode = (currentNode as CharactersNode).NextNode();
            Debug.Log("character found");
            return currentNode as CharactersNode;
        }

        return currentNode;
    }
}
