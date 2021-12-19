using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class GraphNodeViewBinaryOp : Node
    {
        private EnumField m_opTypeEnumField;
        private FloatField m_resultField;
        private float m_result;

        public Port leftPort;
        public Port rightPort;

        public Port outputPort;

        public BinaryOpType opType
        {
            get { return (BinaryOpType)m_opTypeEnumField.value; }
            set { m_opTypeEnumField.value = value; }
        }

        public GraphNodeViewBinaryOp()
        {
            this.title = "BinaryOp";

            outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
            outputContainer.Add(outputPort);

            leftPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
            inputContainer.Add(leftPort);

            rightPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
            inputContainer.Add(rightPort);

            m_opTypeEnumField = new EnumField();
            m_opTypeEnumField.Init(BinaryOpType.Add);
            m_opTypeEnumField.RegisterValueChangedCallback(OnOpTypeValueChanged);
            contentContainer.Add(m_opTypeEnumField);

            m_resultField = new FloatField();
            m_resultField.isReadOnly = true;
            contentContainer.Add(m_resultField);
        }

        private void OnOpTypeValueChanged(ChangeEvent<Enum> evt)
        {
            RefreshResultLabel();
        }

        public void RefreshResultLabel()
        {
            float leftValue = GetInputValue(leftPort);
            float rightValue = GetInputValue(rightPort);
            float result = 0f;
            switch (opType)
            {
                case BinaryOpType.Add:
                    result = leftValue + rightValue;
                    break;
                case BinaryOpType.Sub:
                    result = leftValue - rightValue;
                    break;
                case BinaryOpType.Mul:
                    result = leftValue * rightValue;
                    break;
                case BinaryOpType.Div:
                    if (Mathf.Abs(rightValue) < 1e-4f)
                        result = 0f;
                    else
                        result = leftValue / rightValue;
                    break;
            }
            m_result = result;
            m_resultField.value = result;
        }

        private float GetInputValue(Port port)
        {
            if (port.node != null)
            {
                foreach (Edge connection in port.connections)
                {
                    if (connection != null && connection.output != null)
                    {
                        GraphNodeViewFloat floatNode = connection.output.node as GraphNodeViewFloat;
                        if (floatNode != null)
                        {
                            return floatNode.value;
                        }
                    }
                    break;
                }
            }
            return 0f;
        }
    }

    public enum BinaryOpType
    {
        Add,
        Sub,
        Div,
        Mul,
    }
}