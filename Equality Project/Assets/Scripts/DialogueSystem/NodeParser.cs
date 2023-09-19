using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class NodeParser : MonoBehaviour
{
    public static NodeParser instance = null;

    public DialogueGraph graph;
    Coroutine _parser;
    public TMPro.TMP_Text speakerText;
    public TMPro.TMP_Text characterText;
    public GameObject buttonPrefab;
    public Transform buttonSpawner;
    public List<GameObject> buttonHolder;
    public int buttonTracker;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }else if (instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        foreach (BaseNode b in graph.nodes) {
            if (b.GetString() == "Start") {
                graph.current = b;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }


    /// <summary>
    /// Replace this junk
    /// </summary>

    IEnumerator ParseNode() {
        foreach(GameObject go in buttonHolder) {
            Destroy(go);
        }
        BaseNode b = graph.current;
        string data = b.GetString();
        Debug.Log(data);
        string[] dataParts = data.Split('/');
        if (dataParts[0] == "Start") {
            NextNode("exit");
        }
        if (dataParts[0] == "DialogueNode") {
            characterText.text = dataParts[1];
            speakerText.text = dataParts[2];
            for (int i = 3; i < 7; i++) {
                if (dataParts[i] != null && dataParts[i] != string.Empty) {
                    string option = dataParts[i];
                    GameObject buttonGO = Instantiate(buttonPrefab, buttonSpawner);
                    Button button = buttonGO.GetComponent<Button>();
                    button.GetComponentInChildren<Text>().text = option;
                    OptionButton optionButton = buttonGO.GetComponent<OptionButton>();
                    optionButton.optionNumber = i;
                    buttonHolder.Add(buttonGO);
                }
            }
            yield return new WaitUntil(() => buttonTracker != 0); // change this to wait until the video has stopped playing
            if(buttonTracker == 2) {
                NextNode("exitOne");
            } else if (buttonTracker == 3) {
                NextNode("exitTwo");
            } else if (buttonTracker == 4) {
                NextNode("exitThree");
            } else if (buttonTracker == 5) {
                NextNode("exitFour");
            }
        }
    }

    // public void OnOptionSelect(Button thisButton) {
    //    OptionButton option = thisButton.GetComponent<OptionButton>();
    //   buttonTracker = option.optionNumber;
    // }

    public void NextNode(string fieldName) {

        for (int i = 0; i < buttonHolder.Count; i++) {
            Destroy(buttonSpawner.GetChild(i).gameObject);
        }

        buttonHolder.Clear();   
        buttonTracker = 0;
        // find the port
        if(_parser != null) {
            StopCoroutine(_parser);
            _parser = null;
        }
        foreach(NodePort p in graph.current.Ports) {
            if (p != null) {
                // check this port is the one we're looking for
                if (p.fieldName == fieldName) {
                    if(p.Connection != null) {
                        graph.current = p.Connection.node as BaseNode;
                        break;
                    } else {
                        Debug.Log("Ended Dialogue!");
                    }
                }
            }
        }
        _parser = StartCoroutine(ParseNode());

    }
}
