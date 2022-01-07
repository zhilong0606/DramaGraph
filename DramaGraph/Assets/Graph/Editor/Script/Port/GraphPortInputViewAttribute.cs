using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GraphEditor
{
    public class GraphPortInputViewAttribute : Attribute
    {
        public EGraphPortValueType type;

        public GraphPortInputViewAttribute(EGraphPortValueType type)
        {
            this.type = type;
        }
    }
}
