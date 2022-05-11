using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;
using UnityEngine.UIElements;

namespace XR_Gestures
{
    public class ExtendedGraphView : GraphView
    {
        private Vector2 mousePosition = new Vector2();

        public ExtendedGraphView()
        {
            SetupZoom(0.1f, 2);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();


   
        }

        public void GenerateNode(string _nodeName, Vector2 _mousePos)
        {
            AddElement(CreateNode(_nodeName, _mousePos));
        }

        public BaseNode CreateNode(string _nodeName, Vector2 _mousePos)
        {
            return CreateNode(_nodeName, _mousePos, new NodeData());
        }

        

        public BaseNode CreateNode(string _nodeName, Vector2 _mousePos, NodeData _data)
        {
            BaseNode _node = new BaseNode(_data);
            
            _node.title = _nodeName;
            _node.SetPosition(new Rect((new Vector2(viewTransform.position.x, viewTransform.position.y) * -(1 / scale)) + (_mousePos * (1 / scale)), Vector2.one));


            Port _inputPort = CreatePort(_node, Direction.Input, Port.Capacity.Single);
            _inputPort.portName = "Input";
            _node.inputContainer.Add(_inputPort);

            Port _outputPort = CreatePort(_node, Direction.Output, Port.Capacity.Single);
            _outputPort.portName = "Next";
            _node.outputContainer.Add(_outputPort);

            _node.mainContainer.Add(_node.visuals);

            _node.RefreshExpandedState();
            _node.RefreshPorts();

            return _node;
        }

        Port CreatePort(BaseNode _node, Direction _portDir, Port.Capacity _capacity)
        {
            return _node.InstantiatePort(Orientation.Horizontal, _portDir, _capacity, typeof(float));
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            ports.ForEach((port) =>
            {
                if (startPort != port && startPort.node != port.node)
                    compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        void MousePos(Vector2 _v2)
        {
            mousePosition = _v2;
        }
    }
}