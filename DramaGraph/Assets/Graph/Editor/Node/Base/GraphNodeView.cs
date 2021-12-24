using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class GraphNodeView : Node
    {
        protected VisualElement m_portInputContainer;
        protected GraphNodeData m_data;

        protected GraphNodeDefine m_nodeDefine;
        protected Dictionary<int, Port> m_portMap = new Dictionary<int, Port>();

        public void InitView(GraphNodeDefine define)
        {
            m_nodeDefine = define;
            styleSheets.Add(Resources.Load<StyleSheet>("Styles/GraphNodeView"));
            AddToClassList("GraphNode");
            title = define.name;
            for (int i = 0; i < define.portList.Count; ++i)
            {
                GraphNodePortDefine portDefine = define.portList[i];
                Direction direction = Direction.Input;
                VisualElement container = null;
                switch (portDefine.portType)
                {
                    case EGraphNodePortType.Input:
                        direction = Direction.Input;
                        container = inputContainer;
                        break;
                    case EGraphNodePortType.Output:
                        direction = Direction.Output;
                        container = outputContainer;
                        break;
                }
                Port portView = Port.Create<Edge>(Orientation.Horizontal, direction, Port.Capacity.Multi, typeof(Port));
                container.Add(portView);
                m_portMap.Add(portDefine.id, portView);
            }
            m_portInputContainer = new VisualElement
            {
                name = "portInputContainer",
                cacheAsBitmap = true,
                //style = { overflow = Overflow.Hidden },
                pickingMode = PickingMode.Ignore
            };
            Add(m_portInputContainer);
            m_portInputContainer.SendToBack();
            UpdatePortInputs();
            RefreshExpandedState();
        }

        void UpdatePortInputs()
        {
            foreach (var kv in m_portMap)
            {
                Port port = kv.Value;
                var portInputView = new GraphPortInputViewContainer() { style = { position = Position.Absolute } };
                m_portInputContainer.Add(portInputView);
                if (float.IsNaN(port.layout.width))
                {
                    //port.RegisterCallback<GeometryChangedEvent>(UpdatePortInput);
                }
                else
                {
                    SetPortInputPosition(port, portInputView);
                }
            }
        }

        void SetPortInputPosition(Port port, GraphPortInputViewContainer inputViewContainer)
        {
            inputViewContainer.style.top = port.layout.y;
            inputViewContainer.parent.style.height = inputContainer.layout.height;
        }
    }
}
