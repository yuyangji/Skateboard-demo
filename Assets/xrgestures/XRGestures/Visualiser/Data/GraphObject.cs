using System.Collections.Generic;
using UnityEngine;

namespace XR_Gestures
{
    [CreateAssetMenu(fileName = "New Visualizer", menuName = "Visualizer")]
    public class GraphObject : ScriptableObject
    {

        [SerializeField] public List<FunctionObject> funcObjects;

        public GraphEditorWindow window;

        public List<BaseNode> nodes;



        /*      public NodeData GetCurrentNode(string _currentGuid)
              {
                  int idx = nodes.FindIndex(n => n.guid == _currentGuid);
                  return idx >= 0 ? nodes[idx] : null;   
              }

              public NodeData GetNextNode(string _currentGuid)
              {
                  NodeData currentNode = GetCurrentNode(_currentGuid);
                  if (currentNode == null)
                      return currentNode;

                  Link link = links.Find(l => l.guid == _currentGuid);
                  return link == null ? null : GetCurrentNode(link.guid);

              }*/
    }
}
