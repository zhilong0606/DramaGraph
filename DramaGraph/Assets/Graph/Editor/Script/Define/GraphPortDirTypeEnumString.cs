using System;
using System.Collections;
using System.Collections.Generic;
using EnumString;
using UnityEngine;


namespace GraphEditor
{
    [Serializable]
    public class GraphPortDirTypeEnumString : EnumStringAbstract<EGraphPortDirType>
    {
        public GraphPortDirTypeEnumString(EGraphPortDirType value) : base(value)
        {
        }

        public override bool CheckValueEquals(EGraphPortDirType other)
        {
            return value == other;
        }
    }
}