using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes.Editor;

namespace XR_Gestures
{
    [CustomEditor(typeof(XRAvatar))]
    public class XRAvatarEditor : NaughtyInspector
    {
        SerializedProperty listProperty;
        bool toggled = true;
        protected override void OnEnable()
        {
            base.OnEnable();
            listProperty = serializedObject.FindProperty("trackers");
            toggled = true;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        
            serializedObject.Update();
            
            GUI.color = Color.white;

            EditorGUILayout.Space();

        
            //Draw the list without messy foldouts.
            toggled = EditorGUILayout.Foldout(toggled, "Trackers");

            if (toggled)
            {
                for (int i = 0; i < listProperty.arraySize; i++)
                {
                    SerializedProperty elementRef = listProperty.GetArrayElementAtIndex(i);
                    SerializedProperty _tracker = elementRef.FindPropertyRelative("tracker");
                    SerializedProperty _name = elementRef.FindPropertyRelative("name");

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(_name.stringValue);
                    EditorGUILayout.ObjectField(_tracker, GUIContent.none);
                    EditorGUILayout.EndHorizontal();
                }
            }
          

            EditorGUILayout.Space();
            
            // Apply modfications to properties
            serializedObject.ApplyModifiedProperties();

        }
    }
}