using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public class GraphPortDefine
    {
        public int id;
        public int sortId;
        public string name;
        public GraphPortDirTypeEnumString dirType = new GraphPortDirTypeEnumString(EGraphPortDirType.Input);
        public GraphPortValueTypeEnumString valueType = new GraphPortValueTypeEnumString(EGraphPortValueType.Trigger);
        public string defaultValue;

        public bool isTrigger
        {
            get { return valueType == EGraphPortValueType.Trigger; }
        }
    }
}
