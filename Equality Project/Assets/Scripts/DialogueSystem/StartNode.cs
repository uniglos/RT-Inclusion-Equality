using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XnodeDialogue;

public class StartNode : BaseNode {

    [Output] public int exit;

    public override string GetString() {
        return "Start";
    }
}