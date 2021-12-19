using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DramaScriptFloatNodeView : Node
{
    private FloatField m_floatField;

    public Port outputPort;

    public float value
    {
        get { return m_floatField.value; }
        set { m_floatField.value = value; }
    }

    public DramaScriptFloatNodeView()
    {
        title = "Float";

        outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Port));
        outputContainer.Add(outputPort);

        m_floatField = new FloatField();
        m_floatField.RegisterValueChangedCallback(OnFloatValueChanged);
        contentContainer.Add(m_floatField);
        RefreshExpandedState();
    }

    private void OnFloatValueChanged(ChangeEvent<float> evt)
    {
        if(outputPort != null)
        {
            foreach (Edge connection in outputPort.connections)
            {
                if (connection.input.node == null)
                {
                    continue;
                }
                DramaScriptBinaryOpNodeView binaryOpNode = connection.input.node as DramaScriptBinaryOpNodeView;
                if (binaryOpNode != null)
                {
                    binaryOpNode.RefreshResultLabel();
                }
            }
        }
    }
}
