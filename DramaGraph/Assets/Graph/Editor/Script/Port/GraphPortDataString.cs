using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    [GraphPortData(EGraphPortValueType.String)]
    public class GraphPortDataString : GraphPortData
    {
        public string value;
    }
}
