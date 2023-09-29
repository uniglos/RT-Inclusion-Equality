using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class TestNode : BaseNode {
        [Input] public int value;
        [Output] public int exit;

        public override void Run() {
            Debug.Log("Running the TestNode!");
            NextNode("exit");
        }
    }
}


