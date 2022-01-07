using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class GraphEdge
    {
        private GraphEdgeData m_data;
        private GraphEdgeView m_view;

        public GraphPort inputPort;
        public GraphPort outputPort;

        public GraphEdgeData data
        {
            get { return m_data; }
        }

        public GraphEdgeView view
        {
            get { return m_view; }
        }

        public void Init(GraphEdgeData data, GraphEdgeView view)
        {
            m_data = data;
            m_view = view;
        }
    }
}
