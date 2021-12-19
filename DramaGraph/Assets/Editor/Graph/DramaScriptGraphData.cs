using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DramaScriptGraphData : ISerializationCallbackReceiver
{
    [SerializeField]
    private List<JSONSerializedElement> m_serializableNodeDataList = new List<JSONSerializedElement>();
    [SerializeField]
    private List<DramaGraphEdgeData> m_edgeDataList = new List<DramaGraphEdgeData>();

    private List<DramaGraphNodeData> m_nodeDataList = new List<DramaGraphNodeData>();

    public DramaGraphObject owner;

    public List<DramaGraphNodeData> nodeDataList
    {
        get { return m_nodeDataList; }
    }

    public void OnBeforeSerialize()
    {
        m_serializableNodeDataList = SerializationUtility.Serialize(m_nodeDataList);
    }

    public void OnAfterDeserialize()
    {
        m_nodeDataList.Clear();
        m_nodeDataList.AddRange(SerializationUtility.Deserialize<DramaGraphNodeData>(m_serializableNodeDataList));
    }
}
