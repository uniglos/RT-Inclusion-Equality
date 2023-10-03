using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;
[CustomNodeEditor(typeof(BaseNode))]
public class BaseNodeEditor : NodeEditor
{
    private Color nodeColour = Color.gray;

    public override void AddContextMenuItems(GenericMenu menu) {
        base.AddContextMenuItems(menu);

        if (Selection.objects.Length == 1 && Selection.activeObject is XNode.Node) {
            XNode.Node node = Selection.activeObject as XNode.Node;
            menu.AddItem(new GUIContent("Change Colour"), false, () => NodeColourWheel.Init(this));
        }
    }

    public override void OnBodyGUI() {
        base.OnBodyGUI();
    }

    public override Color GetTint() {
        return nodeColour;
    }

    public void ChangeColour(Color colour) {
        nodeColour = colour;
        NodeEditorWindow.current.Repaint();
    }
}
