using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialogue {
    public class EndNode : BaseNode {
        [Input] public int entry;

        [SerializeField] string changeScene;



        public override IEnumerator Run() {
            
            if(changeScene != null)
            {
                SceneManager.LoadScene(changeScene);
            }
            
            DialogueUIManager.Instance.EndDialogue();
            yield return null;

            GraphRunner.Current.graph = null;
        }
    }
}


