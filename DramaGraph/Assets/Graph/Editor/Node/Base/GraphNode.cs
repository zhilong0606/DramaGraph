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
        private GraphNodeContext m_context;

        public GraphNodeData data
        {
            get { return m_data; }
        }

        public GraphNodeView view
        {
            get { return m_view; }
        }

        public void Init(GraphNodeData data, GraphNodeDefine define, GraphNodeContext context)
        {
            m_define = define;
            m_context = context;
            if (data == null)
            {
                data = new GraphNodeData();
                data.Init(define);
            }
            m_data = data;
            m_view = new GraphNodeView();
            m_view.InitView(define);


            for (int i = 0; i < define.portList.Count; ++i)
            {
                GraphPortDefine portDefine = define.portList[i];
                GraphPort port = new GraphPort();
                GraphPortContext portContext = new GraphPortContext(m_context);
                port.Init(m_data.GetPortData(portDefine.id), portDefine, portContext);
                m_view.AddPort(port.view, portDefine.portType);
            }
            m_view.RefreshExpandedState();
        }

        public void SetPos(Vector2 pos)
        {
            m_data.pos = pos;
            m_view.SetPosition(new Rect(pos, Vector2.zero));
        }
    }
}