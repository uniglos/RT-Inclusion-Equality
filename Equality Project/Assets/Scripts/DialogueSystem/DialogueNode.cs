using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using static XNode.Node;
using XnodeDialogue;


public class DialogueNode : BaseNode {

    [Input] public int entry;
    [Output] public int exitOne;
    [Output] public int exitTwo;
    [Output] public int exitThree;
    [Output] public int exitFour;

    public string characterName;
    public string speech;
    public string optionOne;
    public string optionTwo;
    public string optionThree;
    public string optionFour;
    public string animationName;
    public Texture2D imageL;
    public Texture2D imageM;
    public Texture2D imageR;

    public override string GetString() {
        string stringBuilder = "DialogueNode/" + characterName + "/" + speech + "/" + imageL.name + "/" + imageM.name + "/"  + imageR.name + "/" + optionOne;
        if (optionTwo != null) {
            stringBuilder += "/" + optionTwo;
        }
        if (optionThree != null) {
            stringBuilder += "/" + optionThree;
        }
        if (optionFour != null) {
            stringBuilder += "/" + optionFour;
        }
        return stringBuilder;
    }

    public override object GetValue(NodePort port) {
        return null;
    }
}