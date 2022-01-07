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

        protected GraphNodeDefine m_nodeDefine;
        protected GraphNode m_owner;

        public Action actionOnGeometryChanged;

        public GraphNode owner
        {
            get { return m_owner; }
        }

        public void InitView(GraphNode owner, GraphNodeDefine define)
        {
            m_owner = owner;
            m_nodeDefine = define;
            styleSheets.Add(Resources.Load<StyleSheet>("Styles/GraphNodeView"));
            AddToClassList("GraphNodeView");
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
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            RefreshExpandedState();
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            if (actionOnGeometryChanged != null)
            {
                actionOnGeometryChanged();
            }
        }

        public void AddPort(GraphPortView port, EGraphPortDirType dirType)
        {
            VisualElement container = GetPortContainer(dirType);
            container.Add(port);
            if (dirType == EGraphPortDirType.Input)
            {
                m_portInputContainerGroup.Add(port.container);
                RefreshPortInputContainerGroupHeight();
            }
        }

        public void RefreshPortInputContainerGroupHeight()
        {
            m_portInputContainerGroup.style.height = inputContainer.layout.height;
        }

        private VisualElement GetPortContainer(EGraphPortDirType dirType)
        {
            switch (dirType)
            {
                case EGraphPortDirType.Input:
                    return inputContainer;
                case EGraphPortDirType.Output:
                    return outputContainer;
            }
            return null;
        }
    }
}
