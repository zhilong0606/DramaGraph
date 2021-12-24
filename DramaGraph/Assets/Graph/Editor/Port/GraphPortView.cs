using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class GraphPortView : Port
    {
        private GraphPortInputViewContainer m_container;
        private GraphPortInputView m_inputView;

        public GraphPortDefine define;
        public Func<GraphPortInputView> funcOnCreateInputView;

        public GraphPortInputViewContainer container
        {
            get { return m_container; }
        }

        public GraphPortInputView inputView
        {
            get { return m_inputView; }
        }

        protected GraphPortView(Orientation portOrientation, Direction portDirection, Capacity portCapacity, Type type)
            : base(portOrientation, portDirection, portCapacity, type)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Styles/GraphPortView"));
        }

        public static GraphPortView Create(GraphPortDefine define, IEdgeConnectorListener connectorListener)
        {
            Direction direction = Direction.Input;
            Capacity capacity = Capacity.Single;
            switch (define.portType)
            {
                case EGraphPortType.Input:
                    direction = Direction.Input;
                    capacity = Capacity.Single;
                    break;
                case EGraphPortType.Output:
                    direction = Direction.Output;
                    capacity = Capacity.Multi;
                    break;
            }
            EdgeConnector<Edge> edgeConnector = new EdgeConnector<Edge>(connectorListener);
            GraphPortView port = new GraphPortView(Orientation.Horizontal, direction, capacity, null)
            {
                m_EdgeConnector = edgeConnector,
            };
            port.define = define;
            port.AddManipulator(edgeConnector);
            return port;
        }

        public void InitView()
        {
            portName = define.name;
            //visualClass = slot.concreteValueType.ToClassName();
            if (define.portType == EGraphPortType.Input && !define.isTrigger)
            {
                m_container = new GraphPortInputViewContainer() { style = { position = Position.Absolute } };
                if (funcOnCreateInputView != null)
                {
                    m_inputView = funcOnCreateInputView();
                    m_inputView.InitView();
                    m_container.SetInputView(m_inputView);
                }
                if (float.IsNaN(layout.width))
                {
                    RegisterCallback<GeometryChangedEvent>(UpdatePortInput);
                }
                else
                {
                    RefreshPortInputPosition();
                }
            }
        }

        private void UpdatePortInput(GeometryChangedEvent evt)
        {
            //GraphPortView port = (GraphPortView)evt.target;
            //var inputView = m_PortInputContainer.Children().OfType<PortInputView>().First(x => Equals(x.slot, port.slot));
            RefreshPortInputPosition();
            UnregisterCallback<GeometryChangedEvent>(UpdatePortInput);
        }

        private void RefreshPortInputPosition()
        {
            if (m_inputView != null)
            {
                m_inputView.style.top = layout.y;
                //inputView.parent.style.height = inputContainer.layout.height;
            }
        }
    }
}
