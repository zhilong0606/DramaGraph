using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class GraphNodeDefine
    {
        public string name;
        public List<GraphPortDefine> portList = new List<GraphPortDefine>();
    }

    public class GraphPortDefine
    {
        public int id;
        public string name;
        public string valueType;
        public string defaultValue;
        public EGraphPortType portType;
        public bool isTrigger;
    }

    public enum EGraphPortType
    {
        Input,
        Output,
    }

    public class GraphPortHelper
    {
        public string name;
        public Type dataType;
        public Type inputViewType;
    }
}
