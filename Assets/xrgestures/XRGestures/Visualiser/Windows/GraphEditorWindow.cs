using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace XR_Gestures
{
#if UNITY_EDITOR
    public class GraphEditorWindow : EditorWindow
    {
        GraphObject gestureObj;
        ExtendedGraphView graphView;
        Loader loader = new Loader();

        private Vector2 mousePosition = new Vector2();

        public static GraphEditorWindow Open(GraphObject _gestureObj)
        {
            GraphEditorWindow window = GetWindow<GraphEditorWindow>("Visualiser");
            window.gestureObj = _gestureObj;
            window.CreateGraphView(_gestureObj.funcObjects != null);
            window.minSize = new Vector2(200, 100);
            return window;
        }

        void MouseDown(MouseDownEvent _e)
        {
/*            if (_e.button == 1)
            {
                mousePosition = Event.current.mousePosition;
                GenericMenu _menu = new GenericMenu();
                _menu.AddItem(new GUIContent("Add Node"), false, () => graphView.GenerateNode("", mousePosition));
                _menu.AddItem(new GUIContent("Save"), false, () => save.SaveGraph(gestureGraph, graphView));
                _menu.ShowAsContext();
            }*/
        }

        void CreateGraphView(bool hasNodes)
        {
            graphView = new ExtendedGraphView();
            graphView.RegisterCallback<MouseDownEvent>(MouseDown);
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
            if (hasNodes)
            {
                List<BaseNode> nodes = loader.LoadGraph(gestureObj, graphView);
                gestureObj.nodes = nodes;
            }
                

        }
    }
#endif
}


