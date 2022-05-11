using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace XR_Gestures
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GraphObject))]
    public class GraphObjectEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(40);
            GraphObject graphObject = (GraphObject)target;
            if (GUILayout.Button("Open", GUILayout.Height(40)))
            {
                graphObject.window =  GraphEditorWindow.Open(graphObject);
            }
        }
    }
#endif
}
