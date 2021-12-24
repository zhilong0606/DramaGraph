using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class GraphPortContext
    {
        public GraphNodeContext nodeContext;

        public GraphPortContext(GraphNodeContext nodeContext)
        {
            this.nodeContext = nodeContext;
        }
    }
}
