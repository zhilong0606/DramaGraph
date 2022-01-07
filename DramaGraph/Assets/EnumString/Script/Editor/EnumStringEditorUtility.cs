using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EnumString
{
    public class EnumStringEditorUtility
    {
        public static void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            SerializedProperty enumValueProperty = property.FindPropertyRelative("value");
            SerializedProperty enumNameProperty = property.FindPropertyRelative("m_enumName");
            enumValueProperty.enumValueIndex = EditorGUI.Popup(position, enumValueProperty.enumValueIndex, enumValueProperty.enumNames);
            enumNameProperty.stringValue = enumValueProperty.enumNames[enumValueProperty.enumValueIndex];

            EditorGUI.EndProperty();
        }
    }
}