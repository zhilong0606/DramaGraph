using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public struct SerializedElement
    {
        [SerializeField] public TypeInfo typeInfo;

        [SerializeField] public string JSONnodeData;
    }
}