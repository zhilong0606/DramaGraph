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

        protected GraphPort m_owner;
        protected GraphPortData m_data;
        protected GraphPortDefine m_define;

        public Func<GraphPortInputView> funcOnCreateInputView;
        public Action actionOnGeometryChanged;

        public GraphPortInputViewContainer container
        {
            get { return m_container; }
        }

        public GraphPortInputView inputView
        {
            get { return m_inputView; }
        }

        public int id
        {
            get { return m_data.id; }
        }

        public GraphPort owner
        {
            get { return m_owner; }
        }

        protected GraphPortView(Orientation portOrientation, Direction portDirection, Capacity portCapacity, Type type)
            : base(portOrientation, portDirection, portCapacity, type)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Styles/GraphPortView"));
        }

        public static GraphPortView Create(GraphPort owner, GraphPortDefine define, IEdgeConnectorListener connectorListener)
        {
            Direction direction = Direction.Input;
            Capacity capacity = Capacity.Single;
            switch ((EGraphPortDirType)define.dirType)
            {
                case EGraphPortDirType.Input:
                    direction = Direction.Input;
                    capacity = Capacity.Single;
                    break;
                case EGraphPortDirType.Output:
                    direction = Direction.Output;
                    capacity = Capacity.Multi;
                    break;
            }
            EdgeConnector<GraphEdgeView> edgeConnector = new EdgeConnector<GraphEdgeView>(connectorListener);
            GraphPortView port = new GraphPortView(Orientation.Horizontal, direction, capacity, typeof(GraphPortView))
            {
                m_EdgeConnector = edgeConnector,
            };
            port.m_define = define;
            port.m_owner = owner;
            port.AddManipulator(edgeConnector);
            return port;
        }

        public void InitView()
        {
            portName = m_define.name;
            //visualClass = slot.concreteValueType.ToClassName();
            if (m_define.dirType == EGraphPortDirType.Input && !m_define.isTrigger)
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

        public void SetData(GraphPortData data)
        {
            m_data = data;
            if (m_inputView != null)
            {
                m_inputView.SetData(data);
            }
        }

        public void ShowInputView()
        {
            if (m_container != null)
            {
                m_container.ShowInputView();
            }
        }

        public void HideInputView()
        {
            if (m_container != null)
            {
                m_container.HideInputView();
            }
        }

        private void UpdatePortInput(GeometryChangedEvent evt)
        {
            //GraphPortView port = (GraphPortView)evt.target;
            //var inputView = m_PortInputContainer.Children().OfType<PortInputView>().First(x => Equals(x.slot, port.slot));
            RefreshPortInputPosition();
            if (actionOnGeometryChanged != null)
            {
                actionOnGeometryChanged();
            }
            UnregisterCallback<GeometryChangedEvent>(UpdatePortInput);
        }

        private void RefreshPortInputPosition()
        {
            if (m_container != null)
            {
                m_container.style.top = layout.y;
                //inputView.parent.style.height = inputContainer.layout.height;
            }
        }
    }
}
