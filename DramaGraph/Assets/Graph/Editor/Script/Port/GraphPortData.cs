using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public class GraphPortData
    {
        public int id;

        public void Init(GraphPortDefine define)
        {
            id = define.id;
        }
    }
}