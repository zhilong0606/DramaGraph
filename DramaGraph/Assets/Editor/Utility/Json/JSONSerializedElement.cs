using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct JSONSerializedElement
{
    [SerializeField] public TypeSerializationInfo typeInfo;

    [SerializeField] public string JSONnodeData;
}
