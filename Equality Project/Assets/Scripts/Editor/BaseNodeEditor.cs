using Dialogue;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(BaseNode))]
    public class BaseNodeEditor : NodeEditor {
        private Color nodeColour = Color.gray;

        public override void AddContextMenuItems(GenericMenu menu) {
            base.AddContextMenuItems(menu);
            //Adds the change colour menu to the scripts
            if (Selection.objects.Length == 1 && Selection.activeObject is BaseNode) {
                BaseNode node = Selection.activeObject as BaseNode;
                menu.AddItem(new GUIContent("Change Colour"), false, () => NodeColourWheel.Init(this));
            }
        }

        /// <summary>
        /// Automatically generates insepctor fields inside the Unity Inspector. Removes field with the name of position and graph
        /// </summary>
        /// <param name="node">The Node type to get the type from</param>
        /// <param name="serializedObject">The serializedObject of the node editor object</param>
        protected void GenerateFields(BaseNode node, SerializedObject serializedObject) {
            // Get all the fields from the target MonoBehaviour script
            Type targetType = node.GetType();
            FieldInfo[] fields = targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            // Create SerializedProperties for all the fields
            for (int i = 0; i < fields.Length; i++) {

                string fieldName = fields[i].Name;

                //Removes both the position and graph from within the inspector
                if (fieldName == "position") {
                    break;
                }

                if (fieldName == "graph") {
                    break;
                }

                //Creates the property inside the insepctor
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty(fieldName));
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
}


