using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TypeSerializationInfo
{
    [SerializeField] public string fullName;

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(fullName);
    }
}