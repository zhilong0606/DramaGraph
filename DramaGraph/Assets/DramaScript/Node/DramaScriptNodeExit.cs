using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DramaScript
{
    public partial class DramaScriptNodeExit : DramaScriptNode
    {
        public Action actionOnEnd;

        partial void OnEnd()
        {
            if (actionOnEnd != null)
            {
                actionOnEnd();
            }
        }
    }
}
