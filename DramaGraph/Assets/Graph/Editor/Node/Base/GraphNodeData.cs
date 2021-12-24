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
        public List<GraphPortData> portList = new List<GraphPortData>();

        public void Init(GraphNodeDefine define)
        {
            defineName = define.name;
        }

        public GraphPortData GetPortData(int id)
        {
            for (int i = 0; i < portList.Count; ++i)
            {
                if (portList[i].id == id)
                {
                    return portList[i];
                }
            }
            return null;
        }
    }
}