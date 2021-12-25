using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public class GraphNodeData : ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<SerializedElement> m_serializablePortDataList = new List<SerializedElement>();

        public int id;
        public Vector2 pos;
        public string defineName;

        private List<GraphPortData> m_portList = new List<GraphPortData>();

        public void Init(GraphNodeDefine define)
        {
            defineName = define.name;
        }

        public GraphPortData GetPortData(int id)
        {
            for (int i = 0; i < m_portList.Count; ++i)
            {
                if (m_portList[i] != null && m_portList[i].id == id)
                {
                    return m_portList[i];
                }
            }
            return null;
        }

        public void AddPort(GraphPortData portData)
        {
            m_portList.Add(portData);
        }

        public void OnBeforeSerialize()
        {
            m_serializablePortDataList = SerializationUtility.Serialize(m_portList);
        }

        public void OnAfterDeserialize()
        {
            m_portList.Clear();
            m_portList.AddRange(SerializationUtility.Deserialize<GraphPortData>(m_serializablePortDataList));
        }
    }
}