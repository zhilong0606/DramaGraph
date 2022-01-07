using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    [GraphPortData(EGraphPortValueType.Float)]
    public class GraphPortDataFloat : GraphPortData
    {
        public float value;
    }
}
