using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GraphEditor
{
    public class GraphPortDataAttribute : Attribute
    {
        public EGraphPortValueType type;

        public GraphPortDataAttribute(EGraphPortValueType type)
        {
            this.type = type;
        }
    }
}
