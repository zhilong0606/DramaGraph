using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
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
        private TreeNode<string> m_nodePathTree = new TreeNode<string>();
        private string m_assetGuid;

        public Action actionOnSaveData;
        public Action actionOnExportData;

        public GraphView<TData> view
        {
            get { return m_view; }
        }

        public GraphData data
        {
            get { return m_data; }
        }

        public void Init(GraphContext context)
        {
            m_context = context;
            InitNodeDefine();
            InitNodePathTree();
            InitPortHelper();
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
            m_view.actionOnSaveBtnClicked = OnSaveData;
            m_view.actionOnExportBtnClicked = OnExportData;
            m_view.funcOnCreateNode = OnCreateNode;
            m_view.actionOnDeleteSelection = OnDeleteSelection;
            m_view.searchWindowProvider.nodePathTree = m_nodePathTree;
            m_view.edgeConnectorListener.actionOnEdgeCreated = OnEdgeCreated;
            m_view.edgeConnectorListener.actionOnEdgeRemoved = OnEdgeRemoved;
            m_context.edgeConnectorListener = m_view.edgeConnectorListener;
        }

        private void InitNodeDefine()
        {
            m_nodeDefineList.Clear();
            using (FileStream stream = File.Open(m_context.nodeDefinePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serialize = new XmlSerializer(typeof(GraphNodeDefines));
                //try
                {
                    GraphNodeDefines defines = serialize.Deserialize(stream) as GraphNodeDefines;
                    m_nodeDefineList.AddRange(defines.nodeList);
                }
                //catch
                //{
                //}
            }
        }

        private void InitNodePathTree()
        {
            foreach (GraphNodeDefine nodeDefine in m_nodeDefineList)
            {
                string path = nodeDefine.path;
                string name = nodeDefine.name;
                TreeNode<string> curTree = m_nodePathTree;
                if (!string.IsNullOrEmpty(path))
                {
                    string[] splits = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string split in splits)
                    {
                        TreeNode<string> childNode = curTree.FindChildNodeByValue(path);
                        if (childNode == null)
                        {
                            childNode = new TreeNode<string>(split);
                            curTree.AddNode(childNode);
                        }
                        curTree = childNode;
                    }
                }
                TreeNode<string> leafNode = new TreeNode<string>(name);
                curTree.AddNode(leafNode);
            }
            m_context.nodePathTree = m_nodePathTree;
        }

        private void InitPortHelper()
        {
            foreach (Type type in GetType().Assembly.GetTypes())
            {
                object[] attrs = type.GetCustomAttributes(true);
                for (int i = 0; i < attrs.Length; ++i)
                {
                    GraphPortDataAttribute portDataAttr = attrs[i] as GraphPortDataAttribute;
                    if (portDataAttr != null)
                    {
                        GraphPortHelper helper = GetPortHelper(portDataAttr.type, true);
                        helper.dataType = type;
                        break;
                    }
                    GraphPortInputViewAttribute portInputViewAttr = attrs[i] as GraphPortInputViewAttribute;
                    if (portInputViewAttr != null)
                    {
                        GraphPortHelper helper = GetPortHelper(portInputViewAttr.type, true);
                        helper.inputViewType = type;
                        break;
                    }
                }
            }
            m_context.portHelperList = m_portHelperList;
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
            for (int i = 0; i < data.edgeDataList.Count; ++i)
            {
                CreateEdge(data.edgeDataList[i]);
            }
        }

        private void OnSaveData()
        {
            if (actionOnSaveData != null)
            {
                actionOnSaveData();
            }
        }

        private void OnExportData()
        {
            if (actionOnExportData != null)
            {
                actionOnExportData();
            }
        }

        private bool OnCreateNode(string nodeDefineName, Vector2 screenMousePosition, GraphPortView connectPortView)
        {
            VisualElement windowRoot = m_context.window.rootVisualElement;
            Vector2 windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent, screenMousePosition - m_context.window.position.position);
            Vector2 localPos = m_view.contentViewContainer.WorldToLocal(windowMousePosition);

            GraphNode node = CreateNode(nodeDefineName, localPos);
            if (node != null)
            {
                if (connectPortView != null)
                {
                    GraphPort connectablePort = node.FindConnectablePort(connectPortView.owner);
                    int nodeId1, portId1, nodeId2, portId2;
                    if (connectablePort != null
                        && TryGetPortInfo(connectablePort, out nodeId1, out portId1)
                        && TryGetPortInfo(connectPortView, out nodeId2, out portId2))
                    {
                        GraphEdgeData edgeData = new GraphEdgeData();
                        if (connectablePort.define.portType == EGraphPortType.Output)
                        {
                            edgeData.Init(nodeId1, portId1, nodeId2, portId2);
                        }
                        else if (connectablePort.define.portType == EGraphPortType.Input)
                        {
                            edgeData.Init(nodeId2, portId2, nodeId1, portId1);
                        }
                        m_data.AddEdge(edgeData);
                        CreateEdge(edgeData);
                    }
                }
                return true;
            }
            return false;
        }

        private GraphNode CreateNode(string nodeDefineName, Vector2 pos)
        {
            GraphNodeDefine nodeDefine = GetNodeDefine(nodeDefineName);
            if (nodeDefine != null)
            {
                GraphNodeData nodeData = new GraphNodeData();
                nodeData.id = GetUnusedNodeId();
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

        private int GetUnusedNodeId()
        {
            List<int> usedNodeIdList = new List<int>();
            for (int i = 0; i < m_nodeList.Count; ++i)
            {
                usedNodeIdList.Add(m_nodeList[i].data.id);
            }
            int id = 1;
            while (usedNodeIdList.Contains(id))
            {
                id++;
            }
            return id;
        }

        private void OnEdgeCreated(GraphEdgeView edgeView)
        {
            GraphEdge edge = CreateEdge(edgeView);
            if (edge != null)
            {
                AddEdge(edge);
            }
        }

        private void OnEdgeRemoved(GraphEdgeView edgeView)
        {
            RemoveEdge(edgeView);
        }

        private GraphEdge CreateEdge(GraphEdgeView edgeView)
        {
            GraphEdgeData edgeData = CreateEdgeData(edgeView);
            if (edgeData != null)
            {
                return CreateEdge(edgeData, edgeView);
            }
            return null;
        }

        private GraphEdge CreateEdge(GraphEdgeData edgeData)
        {
            GraphPort inputPort = GetPort(edgeData.inputNodeId, edgeData.inputPortId);
            GraphPort outputPort = GetPort(edgeData.outputNodeId, edgeData.outputPortId);
            GraphEdgeView edgeView = new GraphEdgeView()
            {
                output = outputPort.view,
                input = inputPort.view
            };
            return CreateEdge(edgeData, edgeView);
        }

        private GraphEdge CreateEdge(GraphEdgeData edgeData, GraphEdgeView edgeView)
        {
            GraphPort inputPort = GetPort(edgeData.inputNodeId, edgeData.inputPortId);
            GraphPort outputPort = GetPort(edgeData.outputNodeId, edgeData.outputPortId);
            GraphEdge edge = new GraphEdge();
            edge.Init(edgeData, edgeView);
            edge.inputPort = inputPort;
            edge.outputPort = outputPort;
            m_edgeList.Add(edge);
            edgeView.Init(edge);
            m_view.Add(edge.view);
            inputPort.AddEdge(edge);
            outputPort.AddEdge(edge);
            return edge;
        }

        private void AddEdge(GraphEdge edge)
        {
        }

        private void RemoveEdge(GraphEdgeView edgeView)
        {
            GraphEdge edge = edgeView.owner;
            if (edge == null)
            {
                return;
            }
            if (edge.inputPort != null)
            {
                edge.inputPort.RemoveEdge(edge);
                edge.inputPort = null;
            }
            if (edge.outputPort != null)
            {
                edge.outputPort.RemoveEdge(edge);
                edge.outputPort = null;
            }
            m_edgeList.Remove(edge);
            m_data.RemoveEdge(edge.data);
            //m_view.Remove(edge.view);
        }

        private GraphEdgeData CreateEdgeData(GraphEdgeView edgeView)
        {
            int inputNodeId, inputPortId, outputNodeId, outputPortId;
            if (TryGetPortInfo(edgeView.input as GraphPortView, out inputNodeId, out inputPortId)
                && TryGetPortInfo(edgeView.output as GraphPortView, out outputNodeId, out outputPortId))
            {
                if (inputNodeId != outputNodeId)
                {
                    GraphEdgeData edgeData = new GraphEdgeData();
                    edgeData.Init(outputNodeId, outputPortId, inputNodeId, inputPortId);
                    m_data.AddEdge(edgeData);
                    return edgeData;
                }
            }
            return null;
        }

        private void OnDeleteSelection(List<ISelectable> list)
        {
            foreach (var item in list)
            {
                GraphEdgeView edgeView = item as GraphEdgeView;
                if (edgeView != null)
                {
                    GraphEdge edge = edgeView.owner;
                    if(edge.inputPort != null)
                    {
                        edge.inputPort.RemoveEdge(edge);
                        edge.inputPort = null;
                    }
                    if (edge.outputPort != null)
                    {
                        edge.outputPort.RemoveEdge(edge);
                        edge.outputPort = null;
                    }
                    m_edgeList.Remove(edge);
                    m_data.RemoveEdge(edge.data);
                }
            }
            List<GraphElement> deleteEdgeList = new List<GraphElement>();
            foreach (var item in list)
            {
                GraphNodeView nodeView = item as GraphNodeView;
                if (nodeView != null)
                {
                    GraphNode node = nodeView.owner;
                    m_nodeList.Remove(node);
                    m_data.RemoveNode(node.data);
                    foreach (GraphPort port in node.portList)
                    {
                        List<GraphEdge> edgeList = new List<GraphEdge>(port.edgeList);
                        foreach (GraphEdge edge in edgeList)
                        {
                            if (m_edgeList.Contains(edge) && !deleteEdgeList.Contains(edge.view))
                            {
                                deleteEdgeList.Add(edge.view);
                                if (edge.inputPort != null)
                                {
                                    edge.inputPort.RemoveEdge(edge);
                                    edge.inputPort = null;
                                }
                                if (edge.outputPort != null)
                                {
                                    edge.outputPort.RemoveEdge(edge);
                                    edge.outputPort = null;
                                }
                                m_edgeList.Remove(edge);
                                m_data.RemoveEdge(edge.data);
                            }
                        }
                    }
                }
            }
            m_view.DeleteElements(deleteEdgeList);
        }

        private bool TryGetPortInfo(GraphPort port, out int nodeId, out int portId)
        {
            if (port != null && port.owner != null)
            {
                nodeId = port.owner.data.id;
                portId = port.data.id;
                return true;
            }
            nodeId = 0;
            portId = 0;
            return false;
        }

        private bool TryGetPortInfo(GraphPortView portView, out int nodeId, out int portId)
        {
            if (portView != null && portView.owner != null)
            {
                return TryGetPortInfo(portView.owner, out nodeId, out portId);
            }
            nodeId = 0;
            portId = 0;
            return false;
        }

        private GraphNode GetNode(int nodeId)
        {
            for (int i = 0; i < m_nodeList.Count; ++i)
            {
                if (m_nodeList[i].data.id == nodeId)
                {
                    return m_nodeList[i];
                }
            }
            return null;
        }


        public GraphPort GetPort(int nodeId, int portId)
        {
            GraphNode node = GetNode(nodeId);
            if (node != null)
            {
                return node.GetPort(portId);
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

        private GraphPortHelper GetPortHelper(EGraphPortValueType type, bool autoCreate)
        {
            for (int i = 0; i < m_portHelperList.Count; ++i)
            {
                if (m_portHelperList[i].valueType == type)
                {
                    return m_portHelperList[i];
                }
            }
            if (autoCreate)
            {
                GraphPortHelper helper = new GraphPortHelper();
                helper.valueType = type;
                m_portHelperList.Add(helper);
                return helper;
            }
            return null;
        }
    }
}