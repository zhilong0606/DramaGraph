using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DramaScript
{
    public partial class DramaScriptNodeEntry : DramaScriptNode, IDramaScriptNodeEntry
    {
        public void Start()
        {
            TriggerStart();
        }
    }
}
