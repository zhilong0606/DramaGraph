using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public abstract class GraphNodePortView
    {
        private VisualElement m_view;

        public VisualElement CreateView()
        {
            m_view = OnCreateView();
            return m_view;
        }

        public void LoadValue(GraphNodePortData data)
        {
            OnLoadValue(m_view, data);
        }

        public void SaveValue(GraphNodePortData data)
        {
            OnSaveValue(m_view, data);
        }

        protected abstract VisualElement OnCreateView();
        protected abstract void OnLoadValue(VisualElement view, GraphNodePortData data);
        protected abstract void OnSaveValue(VisualElement view, GraphNodePortData data);
    }
}
