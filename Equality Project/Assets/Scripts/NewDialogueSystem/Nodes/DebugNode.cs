using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class DebugNode : BaseNode {

        [Input] public int value;
        public string text;

        public override void Run() {
            Debug.Log(text);
        }
    }

}

