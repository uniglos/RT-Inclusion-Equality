using System;
using UnityEditor;

namespace DialogueEditor {
    public struct PropertyActionChanged<T> {
        public PropertyActionChanged(SerializedProperty property, Action<T> action) : this() {
            this.property = property;
            this.action = action;
        }

        public SerializedProperty property;
        public Action<T> action;
    }
}


