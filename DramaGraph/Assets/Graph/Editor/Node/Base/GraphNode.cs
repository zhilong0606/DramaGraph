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

        public void Init(GraphNodeDefine define, GraphNodeContext context)
        {
            m_define = define;
            m_context = context;
            m_view = new GraphNodeView();
            m_view.InitView(define);
        }

        public void SetData(GraphNodeData data)
        {
            m_data = data;
            for (int i = 0; i < m_define.portList.Count; ++i)
            {
                GraphPortDefine portDefine = m_define.portList[i];
                GraphPortData portData = m_data.GetPortData(portDefine.id);
                if (portData == null)
                {
                    GraphPort port = CreatePort(portDefine);
                    m_data.AddPort(port.data);
                }
                else
                {
                    CreatePort(portData, portDefine);
                }
            }
            m_view.RefreshExpandedState();
        }

        public void SetPos(Vector2 pos)
        {
            m_data.pos = pos;
            m_view.SetPosition(new Rect(pos, Vector2.zero));
        }

        private GraphPort CreatePort(GraphPortDefine portDefine)
        {
            GraphPortHelper helper = m_context.graphContext.portHelperList.Find(h => h.name == portDefine.valueType);
            if (helper != null)
            {
                GraphPortData data = Activator.CreateInstance(helper.dataType) as GraphPortData;
                if(data != null)
                {
                    data.Init(portDefine);
                    return CreatePort(data, portDefine);
                }
            }
            return null;
        }

        private GraphPort CreatePort(GraphPortData portData, GraphPortDefine portDefine)
        {
            GraphPort port = new GraphPort();
            port.actionOnPortViewGeometryChanged = OnPortViewGeometryChanged;
            GraphPortContext portContext = new GraphPortContext(m_context);
            port.Init(portDefine, portContext);
            m_view.AddPort(port.view, portDefine.portType);

            port.SetData(portData);
            return port;
        }

        private void OnPortViewGeometryChanged()
        {
            m_view.RefreshPortInputContainerGroupHeight();
        }
    }
}