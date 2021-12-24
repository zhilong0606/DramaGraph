using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public class GraphNodeData
    {
        public int id;
        public Vector2 pos;
        public string defineName;
        public List<GraphNodePortData> portList = new List<GraphNodePortData>();

        public void Init(GraphNodeDefine define)
        {
            for (int i = 0; i < define.portList.Count; ++i)
            {
                GraphNodePortDefine portDefine = define.portList[i];
                GraphNodePortData portData = null;
                if (portDefine.valueType == "Float")
                {
                    portData = new GraphNodePortDataFloat();
                }
                if (portData == null)
                {
                    continue;
                }
                portData.id = portDefine.id;
                portList.Add(portData);
            }
        }
    }
}