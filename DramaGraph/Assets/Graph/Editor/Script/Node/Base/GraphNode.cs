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
        private List<GraphPort> m_portList = new List<GraphPort>();

        public GraphNodeData data
        {
            get { return m_data; }
        }

        public GraphNodeView view
        {
            get { return m_view; }
        }

        public List<GraphPort> portList
        {
            get { return m_portList; }
        }

        public void Init(GraphNodeDefine define, GraphNodeContext context)
        {
            m_define = define;
            m_context = context;
            m_view = new GraphNodeView();
            m_view.InitView(this, define);
            m_view.actionOnGeometryChanged = OnGeometryChanged;
        }

        public void SetData(GraphNodeData data)
        {
            m_data = data;
            for (int i = 0; i < m_define.portList.Count; ++i)
            {
                GraphPortDefine portDefine = m_define.portList[i];
                GraphPortData portData = m_data.GetPortData(portDefine.id);
                GraphPort port = null;
                if (portData == null)
                {
                    port = CreatePort(portDefine);
                    m_data.AddPort(port.data);
                }
                else
                {
                    port = CreatePort(portData, portDefine);
                }
                AddPort(port);
            }
            m_view.RefreshExpandedState();
        }

        public void SetPos(Vector2 pos)
        {
            m_data.pos = pos;
            m_view.SetPosition(new Rect(pos, Vector2.zero));
        }

        public GraphPort GetPort(int portId)
        {
            for (int i = 0; i < m_portList.Count; ++i)
            {
                if (m_portList[i].data.id == portId)
                {
                    return m_portList[i];
                }
            }
            return null;
        }

        public GraphPort FindConnectablePort(GraphPort targetPort)
        {
            if (targetPort != null)
            {
                for (int i = 0; i < m_portList.Count; ++i)
                {
                    GraphPort port = m_portList[i];
                    if (port.define.dirType != targetPort.define.dirType
                        && targetPort.define.valueType == port.define.valueType)
                    {
                        return port;
                    }
                }
            }
            return null;
        }

        private void OnGeometryChanged()
        {
            if (m_data != null)
            {
                m_data.pos = m_view.GetPosition().position;
            }
        }

        private GraphPort CreatePort(GraphPortDefine portDefine)
        {
            GraphPortHelper helper = m_context.graphContext.portHelperList.Find(h => h.valueType == portDefine.valueType);
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
            port.Init(this, portDefine, portContext);

            port.SetData(portData);
            return port;
        }

        private void AddPort(GraphPort port)
        {
            if (m_portList.Contains(port))
            {
                return;
            }
            m_portList.Add(port);
            m_view.AddPort(port.view, port.define.dirType);
        }

        private void OnPortViewGeometryChanged()
        {
            m_view.RefreshPortInputContainerGroupHeight();
        }
    }
}