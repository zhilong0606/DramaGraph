using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class Graph
    {
        private GraphViewBase m_view;
        private GraphData m_data;

        private List<GraphNode> m_nodeList = new List<GraphNode>();
        private List<GraphEdge> m_edgeList = new List<GraphEdge>();

        public GraphViewBase view
        {
            get { return m_view; }
        }

        public Graph()
        {

        }
    }
}