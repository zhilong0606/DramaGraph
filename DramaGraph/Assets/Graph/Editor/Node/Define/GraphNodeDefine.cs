using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace GraphEditor
{
    [XmlRoot("NodeDefines")]
    [Serializable]
    public class GraphNodeDefines
    {
        public List<GraphNodeDefine> nodeList = new List<GraphNodeDefine>();
    }

    [Serializable]
    public class GraphNodeDefine
    {
        [XmlElement]
        public string name;
        [XmlElement]
        public List<GraphPortDefine> portList = new List<GraphPortDefine>();
    }

    [Serializable]
    public class GraphPortDefine
    {
        [XmlElement]
        public int id;
        [XmlElement]
        public string name;
        [XmlElement]
        public EGraphPortType portType;
        [XmlElement]
        public EGraphPortValueType valueType;
        [XmlElement]
        public string defaultValue;
        [XmlElement]
        public bool isTrigger;
    }

    public enum EGraphPortType
    {
        Input,
        Output,
    }

    public enum EGraphPortValueType
    {
        Bool,
        Int,
        Int2,
        Int3,
        Float,
        Float2,
        Float3,
        String,
    }

    public class GraphPortHelper
    {
        public EGraphPortValueType valueType;
        public Type dataType;
        public Type inputViewType;
    }
}
