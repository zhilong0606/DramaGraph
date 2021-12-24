using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class GraphPortInputView : VisualElement
    {
        public void InitView()
        {
            OnInitView();
        }

        protected virtual void OnInitView() { }
    }
}
