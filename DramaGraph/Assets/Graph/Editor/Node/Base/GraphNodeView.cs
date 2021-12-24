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
        protected VisualElement m_portInputContainerGroup;
        protected GraphNodeData m_data;

        protected GraphNodeDefine m_nodeDefine;

        public void InitView(GraphNodeDefine define)
        {
            m_nodeDefine = define;
            styleSheets.Add(Resources.Load<StyleSheet>("Styles/GraphNodeView"));
            AddToClassList("GraphNode");
            title = define.name;
            
            m_portInputContainerGroup = new VisualElement
            {
                name = "portInputContainer",
                cacheAsBitmap = true,
                //style = { overflow = Overflow.Hidden },
                pickingMode = PickingMode.Ignore
            };
            Add(m_portInputContainerGroup);
            m_portInputContainerGroup.SendToBack();
            RefreshExpandedState();
        }

        public void AddPort(GraphPortView port, EGraphPortType portType)
        {
            VisualElement container = GetPortContainer(portType);
            container.Add(port);
            m_portInputContainerGroup.Add(port.container);
        }

        private VisualElement GetPortContainer(EGraphPortType portType)
        {
            switch (portType)
            {
                case EGraphPortType.Input:
                    return inputContainer;
                case EGraphPortType.Output:
                    return outputContainer;
            }
            return null;
        }
    }
}
