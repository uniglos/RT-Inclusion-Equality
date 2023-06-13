using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode {
    [Input] public int entry;
    [Output] public int exitOne;
    [Output] public int exitTwo;
    [Output] public int exitThree;
    [Output] public int exitFour;

    public string speakerName;
    public string dialogueOne;
    public string dialogueTwo;
    public string dialogueThree;
    public string dialogueFour;
    public string animationName;

    public override string GetString() {
        string stringBuilder = "DialogueNode/" + speakerName + "/" + dialogueOne;
        if (dialogueTwo != null) {
            stringBuilder += "/" + dialogueTwo;
        }
        if (dialogueThree != null) {
            stringBuilder += "/" + dialogueThree;
        }
        if (dialogueFour != null) {
            stringBuilder += "/" + dialogueFour;
        }
        return stringBuilder;
    }

}