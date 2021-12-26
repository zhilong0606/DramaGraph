﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public class GraphData : ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<SerializedElement> m_serializableNodeDataList = new List<SerializedElement>();
        [SerializeField]
        private List<GraphEdgeData> m_edgeDataList = new List<GraphEdgeData>();

        private List<GraphNodeData> m_nodeDataList = new List<GraphNodeData>();

        public GraphObject owner;

        public List<GraphNodeData> nodeDataList
        {
            get { return m_nodeDataList; }
        }

        public List<GraphEdgeData> edgeDataList
        {
            get { return m_edgeDataList; }
        }

        public void AddNode(GraphNodeData node)
        {
            m_nodeDataList.Add(node);
        }

        public void RemoveNode(GraphNodeData node)
        {
            m_nodeDataList.Remove(node);
        }

        public void AddEdge(GraphEdgeData edgeData)
        {
            m_edgeDataList.Add(edgeData);
        }

        public void RemoveEdge(GraphEdgeData edgeData)
        {
            m_edgeDataList.Remove(edgeData);
        }

        public void OnBeforeSerialize()
        {
            m_serializableNodeDataList = SerializationUtility.Serialize(m_nodeDataList);
        }

        public void OnAfterDeserialize()
        {
            m_nodeDataList.Clear();
            m_nodeDataList.AddRange(SerializationUtility.Deserialize<GraphNodeData>(m_serializableNodeDataList));
        }
    }
}