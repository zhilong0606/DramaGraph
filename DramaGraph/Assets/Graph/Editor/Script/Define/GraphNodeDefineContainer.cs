using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [CreateAssetMenu(menuName = "GraphNode/GraphNodeDefineContainer")]
    [Serializable]
    public class GraphNodeDefineContainer : ScriptableObject
    {
        public List<GraphNodeDefine> nodeList = new List<GraphNodeDefine>();
    }
}
