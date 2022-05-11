using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEditor.UIElements;

namespace XR_Gestures
{
#if UNITY_EDITOR
    public class BaseNode : Node
    {
        public NodeData nodeData;
        public NodeViewer visuals;

        public BaseNode(NodeData _data)
        {
            nodeData = _data != null ? _data : new NodeData();
            visuals = new NodeViewer(this);
        }

        public void ChangeProgress(float progress)
        {
            ProgressBar progressBar = visuals.Query<ProgressBar>("progress_bar").First();
            progressBar.value = progress;
         
        }
    }

    public class NodeViewer : VisualElement
    {
        BaseNode node;

        public NodeViewer(BaseNode _node)
        {
            node = _node;

            VisualTreeAsset tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/VisDance/Visualiser/Graph/BaseNodeTemplate.uxml");
            tree.CloneTree(this);

            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/VisDance/Visualiser/Graph/BaseNodeStyle.uss"));

           
           

           


        }
    }
#endif
}
