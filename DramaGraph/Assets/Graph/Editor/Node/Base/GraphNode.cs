using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class GraphNode
    {
        private GraphNodeData m_data;
        private GraphNodeView m_view;

        public GraphNodeData data
        {
            get { return m_data; }
        }

        public GraphNodeView view
        {
            get { return m_view; }
        }

        public void Init(GraphNodeDefine define, Vector2 pos)
        {
            m_data = Activator.CreateInstance(define.dataType) as GraphNodeData;
            m_data.pos = pos;
            m_view = Activator.CreateInstance(define.dataType) as GraphNodeView;
        }
    }
}