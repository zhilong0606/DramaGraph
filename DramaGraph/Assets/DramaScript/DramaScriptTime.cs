using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DramaScript
{
    public struct DramaScriptTime
    {
        public float floatSec;
        public int milliSec;

        public DramaScriptTime(float floatSec)
        {
            this.floatSec = floatSec;
            this.milliSec = (int) (floatSec * 1000);
        }

        public DramaScriptTime(int milliSec)
        {
            this.floatSec = milliSec / 1000f;
            this.milliSec = milliSec;
        }
    }
}

