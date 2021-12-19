using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public class GraphSlotData
    {
        [SerializeField] private int m_id;

        public GraphSlotData(int id)
        {
            m_id = id;
        }
    }
}