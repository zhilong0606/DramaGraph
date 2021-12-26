using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GraphEditor
{
    public class GraphEdgeView : Edge
    {
        private GraphEdge m_owner;

        public GraphEdge owner
        {
            get { return m_owner; }
        }

        public void Init(GraphEdge owner)
        {
            m_owner = owner;
        }
    }
}
