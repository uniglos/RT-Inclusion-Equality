﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

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

    public override string GetString() {
        string stringBuilder = "DialogueNode/" + characterName + "/" + speech + "/" + optionOne;
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

}