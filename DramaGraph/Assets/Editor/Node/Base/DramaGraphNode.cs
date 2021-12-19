using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class DramaGraphNode : ISerializationCallbackReceiver
{
    [SerializeField]
    List<JSONSerializedElement> m_serializableSlots = new List<JSONSerializedElement>();

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
    }
}
