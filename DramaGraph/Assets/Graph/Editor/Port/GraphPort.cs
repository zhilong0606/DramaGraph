using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class GraphPort
    {
        private GraphPortData m_data;
        private GraphPortView m_view;
        private GraphPortDefine m_define;
        private GraphPortContext m_context;

        public Action actionOnPortViewGeometryChanged;

        public GraphPortDefine define
        {
            get { return m_define; }
        }

        public GraphPortData data
        {
            get { return m_data; }
        }

        public GraphPortView view
        {
            get { return m_view; }
        }

        public void Init(GraphPortDefine define, GraphPortContext context)
        {
            m_define = define;
            m_context = context;
            m_view = GraphPortView.Create(define, m_context.nodeContext.graphContext.edgeConnectorListener);
            m_view.funcOnCreateInputView = OnCreateInputView;
            m_view.InitView();
            m_view.actionOnGeometryChanged = OnPortViewGeometryChanged;
        }

        public void SetData(GraphPortData data)
        {
            m_data = data;
            m_view.SetData(data);
        }

        private GraphPortInputView OnCreateInputView()
        {
            GraphPortHelper helper = GetHelper();
            if (helper != null && helper.inputViewType != null)
            {
                return Activator.CreateInstance(helper.inputViewType) as GraphPortInputView;
            }
            return null;
        }

        private void OnPortViewGeometryChanged()
        {
            if (actionOnPortViewGeometryChanged != null)
            {
                actionOnPortViewGeometryChanged();
            }
        }

        private GraphPortHelper GetHelper()
        {
            foreach (GraphPortHelper helper in m_context.nodeContext.graphContext.portHelperList)
            {
                if (helper.name == define.valueType)
                {
                    return helper;
                }
            }
            return null;
        }
    }
}
