using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class GraphContext
    {
        public EdgeConnectorListener edgeConnectorListener;
        public EditorWindow window;
        public List<GraphPortHelper> portHelperList;
        public string nodeDefinePath;
    }
}
