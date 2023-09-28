using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XnodeDialogue;
using XNodeEditor;
using System.Linq;

public class GraphRunner : MonoBehaviour {

    [SerializeField] private DialogueGraph graph;

    public BaseNode currentNode = null;

    private void Start() {
        RunGraph();
    }

    public void RunGraph() {
        //Find the first node in the graph
        currentNode = graph.nodes.Find(n => n is StartNode) as BaseNode;

        //Gets next node to the start node and returns what the node type is
        currentNode = (currentNode as StartNode).NextNode();

        if (currentNode is CharactersNode) {
            currentNode = (currentNode as CharactersNode).NextNode();
        }

        if (currentNode is DialogueNode) {
            //Returns the dialogue node based on the button index which is cliked in the list
            DialogueUIManager.Instance.Draw(currentNode);
        }

        if(currentNode is BackgroundNode) {
            currentNode = (currentNode as BackgroundNode).NextNode();
        }
    }

    /// <summary>
    /// Returns the Dialogue node based on the connection to the index
    /// </summary>
    /// <param name="index">The index in the button panel which the user has pressed</param>
    public BaseNode AnswerDialogue(int index) {
        if (currentNode != null) {
            foreach (var port in currentNode.Ports) {
                if (port.Connection.node is DialogueNode) {
                    //Debug.Log("Dialogue Node");
                    (port.Connection.node as DialogueNode).AnswerQuestion(index);
                    DialogueUIManager.Instance.Draw(currentNode);
                    Debug.Log("Current Node is: " + currentNode.name);
                    return currentNode;
                } else if (port.Connection.node is CharactersNode) {
                    currentNode = (port.Connection.node as CharactersNode).DetectNodeType(port);
                    DialogueUIManager.Instance.Draw(currentNode);
                    currentNode.DetectNodeType(port);
                    Debug.Log("Current Node is: " + currentNode.name);
                    return currentNode;
                } else if (port.Connection.node is BackgroundNode) {
                    currentNode = (port.Connection.node as BackgroundNode).DetectNodeType(port);
                    //DialogueUIManager.Instance.Draw(currentNode);
                    currentNode.DetectNodeType(port);
                    Debug.Log("Current Node is: " + currentNode.name);
                    return currentNode;
                }
            }
        }

        Debug.Log("Current Node is null");

        return null;
    }
}
