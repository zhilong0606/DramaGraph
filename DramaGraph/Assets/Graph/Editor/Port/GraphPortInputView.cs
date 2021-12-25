using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class GraphPortInputView : VisualElement
    {
        protected GraphPortData m_data;

        public void InitView()
        {
            OnInitView();
        }

        public void SetData(GraphPortData data)
        {
            m_data = data;
            OnRefreshData();
        }

        protected virtual void OnInitView() { }
        protected virtual void OnRefreshData() { }
    }
}
