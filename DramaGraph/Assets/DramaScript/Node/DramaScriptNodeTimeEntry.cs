using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DramaScript
{
    public partial class DramaScriptNodeTimeEntry : DramaScriptNode, IDramaScriptNodeEntry
    {
        private bool m_isStart;
        private float m_curTime;
        private float m_tarTime;

        public void Start()
        {
            m_isStart = true;
            m_curTime = 0f;
            m_tarTime = GetTime();
        }

        protected override void OnTick(DramaScriptTime deltaTime)
        {
            if (m_isStart)
            {
                m_curTime += deltaTime.floatSec;
                if (m_curTime > m_tarTime)
                {
                    m_isStart = false;
                    TriggerEnd();
                }
            }
        }
    }
}
