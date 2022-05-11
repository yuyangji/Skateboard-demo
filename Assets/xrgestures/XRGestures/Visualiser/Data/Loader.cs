using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEngine.UIElements;

namespace XR_Gestures
{

    public class Loader
    {
        public List<BaseNode> LoadGraph(GraphObject _gesture, ExtendedGraphView _graph)
        {

            float x = 0;
            List<BaseNode> nodes = new List<BaseNode>();
            foreach(FunctionObject obj in _gesture.funcObjects)
            {
                NodeData nodeData = new NodeData();
                BaseNode _tempNode = _graph.CreateNode("asdfasdf", new Vector2(x+= 200f, 50f), nodeData);
                nodes.Add(_tempNode);
                _graph.AddElement(_tempNode);
                Debug.Log("added");
                
            }


            GenerateLinks(nodes, _graph);
            return nodes;
        }

        void GenerateLinks(List<BaseNode> nodes, ExtendedGraphView _graph)
        {
            List<BaseNode> _nodes = _graph.nodes.ToList().Cast<BaseNode>().ToList();

            for (int i = 0; i < _nodes.Count - 1; i++)
            {
                LinkNodes(_nodes[i].outputContainer[0].Q<Port>(), (Port)_nodes[i+1].inputContainer[0], _graph);
            }

        }

        void LinkNodes(Port _output, Port _input, ExtendedGraphView _graph)
        {
            //Debug.Log(_output);

            Edge _temp = new Edge
            {
                output = _output,
                input = _input
            };

            _temp.input.Connect(_temp);
            _temp.output.Connect(_temp);
            _graph.Add(_temp);
        }

    }
}