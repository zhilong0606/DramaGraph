using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public abstract class GraphNode
    {
        private GraphNodeData m_data;
        private GraphNodeView m_view;
    }
}