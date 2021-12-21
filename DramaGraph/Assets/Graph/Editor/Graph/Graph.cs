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

        private bool OnCreateNode(GraphNodeDefine nodeDefine, Vector2 pos)
        {
            GraphNode node = CreateNode(nodeDefine, pos);
            return node != null;
        }

        private GraphNode CreateNode(GraphNodeDefine nodeDefine, Vector2 pos)
        {
            GraphNode node = new GraphNode();
            node.Init(nodeDefine, pos);
            CreateNode(type, node.pos);
        }
    }
}