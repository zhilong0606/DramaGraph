using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


namespace GraphEditor
{
    public class GraphNodePortViewFloat : GraphNodePortView
    {
        protected override VisualElement OnCreateView()
        {
            return new FloatField();
        }

        protected override void OnLoadValue(VisualElement view, GraphNodePortData data)
        {
            FloatField vv = view as FloatField;
            GraphNodePortDataFloat dd = data as GraphNodePortDataFloat;
            if (vv != null && dd != null)
            {
                vv.value = dd.value;
            }
        }

        protected override void OnSaveValue(VisualElement view, GraphNodePortData data)
        {
            FloatField vv = view as FloatField;
            GraphNodePortDataFloat dd = data as GraphNodePortDataFloat;
            if (vv != null && dd != null)
            {
                dd.value = vv.value;
            }
        }
    }
}
