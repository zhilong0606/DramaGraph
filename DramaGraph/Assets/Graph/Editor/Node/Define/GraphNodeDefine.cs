using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class GraphNodeDefine
    {
        public string name;
        public List<GraphNodePortDefine> portList = new List<GraphNodePortDefine>();
    }

    public class GraphNodePortDefine
    {
        public int id;
        public string name;
        public string valueType;
        public string defaultValue;
        public EGraphNodePortType portType;
        public bool needPrivateEditor;
    }

    public enum EGraphNodePortType
    {
        Input,
        Output,
    }

    public class GraphNodePortHelper
    {
        public string name;
        public Type dataType;
        public Type viewType;
    }
}
