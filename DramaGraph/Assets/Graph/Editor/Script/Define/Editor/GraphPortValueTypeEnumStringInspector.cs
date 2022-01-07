using System;
using System.Collections;
using System.Collections.Generic;
using EnumString;
using UnityEditor;
using UnityEngine;

namespace GraphEditor
{
    [CustomPropertyDrawer(typeof(GraphPortValueTypeEnumString))]
    public class GraphPortValueTypeEnumStringInspector : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnumStringEditorUtility.DrawProperty(position, property, label);
        }
    }
}
