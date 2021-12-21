using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GraphEditor
{
    public class GraphObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private SerializedElement m_serializedGraphData;
        [SerializeField] private int m_serializedVersion;
        [SerializeField] private bool m_isDirty;

        private GraphData m_graphData;
        private int m_deserializedVersion;

        public GraphData graphData
        {
            get { return m_graphData; }
        }

        public bool isDirty
        {
            get { return m_isDirty; }
            set { m_isDirty = value; }
        }

        public bool wasUndoRedoPerformed
        {
            get { return m_deserializedVersion != m_serializedVersion; }
        }

        public void SetData(GraphData data)
        {
            if (m_graphData != null)
            {
                m_graphData.owner = null;
            }
            m_graphData = data;
            if (m_graphData != null)
            {
                m_graphData.owner = this;
            }
        }

        public void RegisterCompleteObjectUndo(string actionName)
        {
            Undo.RegisterCompleteObjectUndo(this, actionName);
            m_serializedVersion++;
            m_deserializedVersion++;
            m_isDirty = true;
        }

        public void HandleUndoRedo()
        {
            Debug.Assert(wasUndoRedoPerformed);
            var deserializedGraph = DeserializeGraph();
            //m_graphData.ReplaceWith(deserializedGraph);
        }

        public void OnBeforeSerialize()
        {
            if (m_graphData != null)
            {
                m_serializedGraphData = SerializationUtility.Serialize(m_graphData);
            }
        }

        public void OnAfterDeserialize()
        {
            if (m_graphData == null)
            {
                SetData(DeserializeGraph());
            }
        }

        public void Validate()
        {
            if (m_graphData != null)
            {
                //m_graphData.OnEnable();
                //m_graphData.ValidateGraph();
            }
        }

        private void OnEnable()
        {
            Validate();
        }

        private GraphData DeserializeGraph()
        {
            GraphData deserializedGraph = SerializationUtility.Deserialize<GraphData>(m_serializedGraphData);
            m_deserializedVersion = m_serializedVersion;
            m_serializedGraphData = default(SerializedElement);
            return deserializedGraph;
        }
    }
}