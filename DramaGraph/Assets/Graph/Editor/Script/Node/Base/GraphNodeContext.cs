using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class GraphNodeContext
    {
        public GraphContext graphContext;

        public GraphNodeContext(GraphContext graphContext)
        {
            this.graphContext = graphContext;
        }
    }
}
