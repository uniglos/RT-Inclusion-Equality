using UnityEngine;
using XNode;

namespace Dialogue {
    [CreateAssetMenu]
    public class DialogueGraph : NodeGraph {
        public BaseNode CurrentNode { get; set; }
    }
}