using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeColourWheel : EditorWindow
{
    private static Color nodeColour = Color.white;
    private static BaseNodeEditor nodeEditor;

    public static void Init(BaseNodeEditor editor) {
        nodeEditor = editor;
        EditorWindow window = GetWindow(typeof(NodeColourWheel));
        window.Show();
    }

    void OnGUI() {
        nodeColour = EditorGUILayout.ColorField("New Color", nodeColour);
        if(GUILayout.Button("Change Colour")) {
            nodeEditor.ChangeColour(nodeColour);
            
        }
    }
}
