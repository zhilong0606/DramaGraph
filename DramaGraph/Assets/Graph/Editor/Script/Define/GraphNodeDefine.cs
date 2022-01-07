using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using EnumString;
using UnityEditor;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public class GraphNodeDefine
    {
        public string name;
        public string path;
        public int idCursor;
        public List<GraphPortDefine> portList = new List<GraphPortDefine>();
    }

}
