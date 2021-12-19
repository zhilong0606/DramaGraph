using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public struct TypeInfo
    {
        [SerializeField] public string fullName;

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(fullName);
        }
    }
}