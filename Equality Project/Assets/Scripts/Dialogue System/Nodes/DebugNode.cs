using System.Collections;
using UnityEngine;

namespace Dialogue.Internal {
    public class DebugNode : BaseNode {

        [Input][HideInInspector] public int value;

        [SerializeField] private string text;

        public override IEnumerator Run() {
            Debug.Log(text);
            yield return null;
        }
    }
}

