using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class GraphView<TData> : GraphView where TData : GraphData
    {
        private TData m_data;
        private EdgeConnectorListener m_edgeConnectorListener;
        private SearchWindowProvider m_searchWindowProvider;

        public Action<TData> actionOnSaveData;
        public Func<GraphNodeData, string, Vector2, bool> funcOnCreateNode;

        public void Init()
        {
            this.StretchToParentSize();
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            m_searchWindowProvider = ScriptableObject.CreateInstance<SearchWindowProvider>();
            m_searchWindowProvider.funcOnSelectEntry = OnMenuWindowProviderSelectEntry;

            nodeCreationRequest += context => { m_searchWindowProvider.Open(context.screenMousePosition); };

            m_edgeConnectorListener = new EdgeConnectorListener(m_searchWindowProvider);

            
            IMGUIContainer toolbar = new IMGUIContainer(() =>
            {
                GUILayout.BeginHorizontal(EditorStyles.toolbar);
                if (GUILayout.Button("Save Asset", EditorStyles.toolbarButton))
                {
                    if (actionOnSaveData != null)
                    {
                        actionOnSaveData(m_data);
                    }
                }
                GUILayout.EndHorizontal();
            });
            Add(toolbar);
        }

        private bool OnMenuWindowProviderSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context, Port connectPort)
        {
            string nodeDefineName = searchTreeEntry.userData as string;
            Vector2 windowMousePosition = context.screenMousePosition;
            if (funcOnCreateNode != null)
            {
                return funcOnCreateNode(null, nodeDefineName, windowMousePosition);
            }
            return false;
        }

        public void SetData(TData data)
        {
            m_data = data;
            foreach (GraphNodeData nodeData in m_data.nodeDataList)
            {
                if (funcOnCreateNode != null)
                {
                    funcOnCreateNode(nodeData, nodeData.defineName, nodeData.pos);
                }
            }
        }

        public void AddNode(GraphNodeView nodeView)
        {
            for (int i = 0; i < nodeView.inputContainer.childCount; ++i)
            {
                Port inputPort = nodeView.inputContainer[i] as Port;
                if (inputPort != null)
                {
                    EdgeConnector<Edge> connector = new EdgeConnector<Edge>(m_edgeConnectorListener);
                    FieldInfo fi = typeof(Port).GetField("m_EdgeConnector", BindingFlags.NonPublic | BindingFlags.Instance);
                    fi.SetValue(inputPort, connector);
                    inputPort.AddManipulator(connector);
                }
            }
            for (int i = 0; i < nodeView.outputContainer.childCount; ++i)
            {
                Port outputPort = nodeView.outputContainer[i] as Port;
                if (outputPort != null)
                {
                    EdgeConnector<Edge> connector = new EdgeConnector<Edge>(m_edgeConnectorListener);
                    FieldInfo fi = typeof(Port).GetField("m_EdgeConnector", BindingFlags.NonPublic | BindingFlags.Instance);
                    fi.SetValue(outputPort, connector);
                    outputPort.AddManipulator(connector);
                }
            }
            AddElement(nodeView);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> portList = new List<Port>();
            foreach (Port port in ports.ToList())
            {
                if (startPort.node != port.node
                    && startPort.direction != port.direction
                    && startPort.portType == port.portType)
                {
                    portList.Add(port);
                }
            }
            return portList;
        }

        protected override bool canDeleteSelection
        {
            get { return true; }
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
        }
    }
}