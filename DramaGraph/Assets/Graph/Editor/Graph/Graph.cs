using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class Graph<TData, TView>
        where TData : GraphData
        where TView : GraphView<TData>
    {
        private TView m_view;
        private TData m_data;
        private GraphObject m_obj;

        private List<GraphNode> m_nodeList = new List<GraphNode>();
        private List<GraphEdge> m_edgeList = new List<GraphEdge>();
        private List<GraphNodeDefine> m_nodeDefineList = new List<GraphNodeDefine>();
        private List<GraphNodePortHelper> m_portHelperList = new List<GraphNodePortHelper>();
        private string m_assetGuid;

        public Action<TData> actionOnSaveData;

        public GraphView<TData> view
        {
            get { return m_view; }
        }

        public Graph(TData data)
        {
        }

        public void Init()
        {
            InitObject();
            InitView();
            InitNodeDefine();
            InitPortHelper();
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
        }

        private void InitNodeDefine()
        {
            m_nodeDefineList = new List<GraphNodeDefine>()
            {
                new GraphNodeDefine()
                {
                    name = "Float",
                    portList = new List<GraphNodePortDefine>()
                    {
                        new GraphNodePortDefine()
                        {
                            id = 0,
                            valueType = "Float",
                            portType = EGraphNodePortType.Input,
                            needPrivateEditor = true,
                        },
                        new GraphNodePortDefine()
                        {
                            id = 1,
                            valueType = "Float",
                            portType = EGraphNodePortType.Output,
                            needPrivateEditor = false,
                        },
                    },
                },
                new GraphNodeDefine()
                {
                    name = "BinaryOp",
                    portList = new List<GraphNodePortDefine>()
                    {
                        new GraphNodePortDefine()
                        {
                            id = 0,
                            valueType = "Float",
                            portType = EGraphNodePortType.Input,
                            needPrivateEditor = true,
                        },
                        new GraphNodePortDefine()
                        {
                            id = 1,
                            valueType = "Float",
                            portType = EGraphNodePortType.Input,
                            needPrivateEditor = true,
                        },
                        new GraphNodePortDefine()
                        {
                            id = 2,
                            valueType = "Float",
                            portType = EGraphNodePortType.Output,
                            needPrivateEditor = false,
                        },
                    },
                },
            };
        }

        private void InitPortHelper()
        {
            m_portHelperList = new List<GraphNodePortHelper>()
            {
                new GraphNodePortHelper()
                {
                    name = "Float",
                    dataType = typeof(GraphNodePortDataFloat),
                    viewType = typeof(GraphNodePortViewFloat),
                }
            };
        }

        public void SetData(TData data)
        {
            m_data = data;
            m_obj.SetData(data);
            m_view.SetData(data);
        }

        private void OnSaveData(TData data)
        {
            if (actionOnSaveData != null)
            {
                actionOnSaveData(data);
            }
        }

        private bool OnCreateNode(GraphNodeData nodeData, string nodeDefineName, Vector2 pos)
        {
            GraphNode node = CreateNode(nodeData, nodeDefineName, pos);
            return node != null;
        }

        private GraphNode CreateNode(GraphNodeData nodeData, string nodeDefineName, Vector2 pos)
        {
            GraphNodeDefine nodeDefine = GetNodeDefine(nodeDefineName);
            if (nodeDefine != null)
            {
                GraphNode node = new GraphNode();
                node.Init(nodeData, nodeDefine, pos);
                m_view.AddNode(node.view);
                node.SetPos(pos);
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