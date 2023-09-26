using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XnodeDialogue;

public class BackgroundNode : BaseNode {

    [Input] public int entry;
    [Output] public int exitOne;

    public Texture2D background;

    // Use this for initialization
    protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}