using NaughtyAttributes.Editor;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace XR_Gestures.Editors
{
    [CustomEditor(typeof(TemporalGestureSO))]
    public class TemporalGestureEditor : NaughtyInspector
    {
        SerializedProperty listProperty;
        bool toggled = true;
        ReorderableList list;
        protected override void OnEnable()
        {
            base.OnEnable();
            listProperty = serializedObject.FindProperty("functions");
            toggled = true;

            list = new ReorderableList(serializedObject, listProperty, true, true, true, true);



            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Functions");
            };
            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += EditorGUIUtility.standardVerticalSpacing;

                SerializedProperty functions = element.FindPropertyRelative("functions");
                EditorGUI.PropertyField(new Rect(rect.x + 15, rect.y, rect.width - 15, EditorGUI.GetPropertyHeight(functions, true)), functions, new GUIContent("Node " + index.ToString()), true);
            };
            list.elementHeightCallback = (int index) =>
            {
                SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
                float height = EditorGUIUtility.standardVerticalSpacing;

                SerializedProperty transitions = element.FindPropertyRelative("functions");
                height += EditorGUI.GetPropertyHeight(transitions, true) + EditorGUIUtility.standardVerticalSpacing;

                return height + EditorGUIUtility.standardVerticalSpacing;
            };
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update(); // Update the array property's representation in the inspector

            list.DoLayoutList(); // Have the ReorderableList do its work

            // We need to call this so that changes on the Inspector are saved by Unity.
            serializedObject.ApplyModifiedProperties();
        }
        /* public override void OnInspectorGUI()
         {
             base.OnInspectorGUI();
             serializedObject.Update();

             var temporalGesture = (TemporalGestureSO) target;


             if (GUILayout.Button("Debug", GUILayout.Width(100)))
             {

             }

             EditorGUILayout.Space();


             //Draw the list without messy foldouts.
             for (int i = 0; i < listProperty.arraySize; i++)
             {
                 SerializedProperty elementRef = listProperty.GetArrayElementAtIndex(i);
                 SerializedProperty list = elementRef.FindPropertyRelative("functions");
                 EditorGUILayout.PropertyField(list, new GUIContent("Node " + i.ToString()), true);
                 if (GUILayout.Button("Remove node", GUILayout.Width(100)))
                 {
                     temporalGesture.RemoveNode(i);
                 }


             }

             EditorGUILayout.Space();

             if (GUILayout.Button("Add Node", GUILayout.Width(100)))
             {
                 temporalGesture.AddNode();
             }

             // Apply modfications to properties
             serializedObject.ApplyModifiedProperties();
         }*/
    }
}