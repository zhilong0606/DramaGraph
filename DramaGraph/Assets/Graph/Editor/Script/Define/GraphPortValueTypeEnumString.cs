using System;
using System.Collections;
using System.Collections.Generic;
using EnumString;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public class GraphPortValueTypeEnumString : EnumStringAbstract<EGraphPortValueType>
    {
        public GraphPortValueTypeEnumString(EGraphPortValueType value) : base(value)
        {
        }

        public override bool CheckValueEquals(EGraphPortValueType other)
        {
            return value == other;
        }
    }
}