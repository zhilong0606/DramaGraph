using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class Graph<TData, TView>
        where TData : GraphData
        where TView : GraphView<TData>
    {
        private TView m_view;
        private TData m_data;
        private GraphObject m_obj;
        private GraphContext m_context;

        private List<GraphNode> m_nodeList = new List<GraphNode>();
        private List<GraphEdge> m_edgeList = new List<GraphEdge>();
        private List<GraphNodeDefine> m_nodeDefineList = new List<GraphNodeDefine>();
        private List<GraphPortHelper> m_portHelperList = new List<GraphPortHelper>();
        private string m_assetGuid;

        public Action<TData> actionOnSaveData;

        public GraphView<TData> view
        {
            get { return m_view; }
        }

        public void Init(GraphContext context)
        {
            m_context = context;
            InitNodeDefine();
            InitPortHelper();
            m_context.portHelperList = m_portHelperList;
            InitObject();
            InitView();
        }

        private void InitObject()
        {
            m_obj = ScriptableObject.CreateInstance<GraphObject>();
            m_obj.hideFlags = HideFlags.HideAndDontSave;
            //m_data.messageManager = messageManager;
            //m_data.OnEnable();
            //m_data.ValidateGraph(););
        }

        private void InitView()
        {
            m_view = Activator.CreateInstance(typeof(TView)) as TView;
            m_view.Init();
            m_view.actionOnSaveData = OnSaveData;
            m_view.funcOnCreateNode = OnCreateNode;
            m_context.edgeConnectorListener = m_view.edgeConnectorListener;
        }

        private void InitNodeDefine()
        {
            m_nodeDefineList = new List<GraphNodeDefine>()
            {
                new GraphNodeDefine()
                {
                    name = "Float",
                    portList = new List<GraphPortDefine>()
                    {
                        new GraphPortDefine()
                        {
                            id = 0,
                            valueType = "Float",
                            portType = EGraphPortType.Input,
                            isTrigger = false,
                        },
                        new GraphPortDefine()
                        {
                            id = 1,
                            valueType = "Float",
                            portType = EGraphPortType.Output,
                            isTrigger = false,
                        },
                    },
                },
                new GraphNodeDefine()
                {
                    name = "BinaryOp",
                    portList = new List<GraphPortDefine>()
                    {
                        new GraphPortDefine()
                        {
                            id = 0,
                            valueType = "Float",
                            portType = EGraphPortType.Input,
                            isTrigger = false,
                        },
                        new GraphPortDefine()
                        {
                            id = 1,
                            valueType = "Float",
                            portType = EGraphPortType.Input,
                            isTrigger = false,
                        },
                        new GraphPortDefine()
                        {
                            id = 2,
                            valueType = "Float",
                            portType = EGraphPortType.Output,
                            isTrigger = false,
                        },
                    },
                },
            };
        }

        private void InitPortHelper()
        {
            m_portHelperList = new List<GraphPortHelper>()
            {
                new GraphPortHelper()
                {
                    name = "Float",
                    dataType = typeof(GraphPortDataFloat),
                    inputViewType = typeof(GraphPortInputViewFloat),
                }
            };
        }

        public void SetData(TData data)
        {
            m_data = data;
            m_obj.SetData(data);
            m_view.SetData(data);
            for (int i = 0; i < data.nodeDataList.Count; ++i)
            {
                CreateNode(data.nodeDataList[i]);
            }
        }

        private void OnSaveData(TData data)
        {
            if (actionOnSaveData != null)
            {
                actionOnSaveData(data);
            }
        }

        private bool OnCreateNode(string nodeDefineName, Vector2 screenMousePosition)
        {
            VisualElement windowRoot = m_context.window.rootVisualElement;
            Vector2 windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent, screenMousePosition - m_context.window.position.position);
            Vector2 localPos = m_view.contentViewContainer.WorldToLocal(windowMousePosition);

            GraphNode node = CreateNode(nodeDefineName, localPos);
            return node != null;
        }

        private GraphNode CreateNode(string nodeDefineName, Vector2 pos)
        {
            GraphNodeDefine nodeDefine = GetNodeDefine(nodeDefineName);
            if (nodeDefine != null)
            {
                GraphNodeData nodeData = new GraphNodeData();
                nodeData.Init(nodeDefine);
                nodeData.pos = pos;
                m_data.AddNode(nodeData);
                return CreateNode(nodeData);
            }
            return null;
        }

        private GraphNode CreateNode(GraphNodeData nodeData)
        {
            GraphNodeDefine nodeDefine = GetNodeDefine(nodeData.defineName);
            if (nodeDefine != null)
            {
                GraphNodeContext nodeContext = new GraphNodeContext(m_context);
                GraphNode node = new GraphNode();
                node.Init(nodeDefine, nodeContext);
                m_view.AddNode(node.view);
                node.SetData(nodeData);
                node.SetPos(nodeData.pos);
                m_nodeList.Add(node);
                return node;
            }
            return null;
        }

        private GraphNodeDefine GetNodeDefine(string nodeDefineName)
        {
            for (int i = 0; i < m_nodeDefineList.Count; ++i)
            {
                if (m_nodeDefineList[i].name == nodeDefineName)
                {
                    return m_nodeDefineList[i];
                }
            }
            return null;
        }
    }
}