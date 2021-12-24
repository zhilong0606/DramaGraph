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
        private GraphNodeDefine m_define;

        public GraphNodeData data
        {
            get { return m_data; }
        }

        public GraphNodeView view
        {
            get { return m_view; }
        }

        public void Init(GraphNodeData data, GraphNodeDefine define, Vector2 pos)
        {
            m_define = define;
            m_data = new GraphNodeData();
            m_data.Init(define);
            m_view = new GraphNodeView();
            m_view.InitView(define);
        }

        public void SetPos(Vector2 pos)
        {
            m_data.pos = pos;
            m_view.SetPosition(new Rect(pos, Vector2.zero));
        }
    }
}