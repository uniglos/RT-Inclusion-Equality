using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public int optionNumber;

    public void OnOptionSelect() {
        NodeParser.instance.buttonTracker = optionNumber;
    }

}
