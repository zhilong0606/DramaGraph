using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    [Serializable]
    public class GraphEdgeData
    {
        [SerializeField] private int m_outputNodeId;
        [SerializeField] private int m_outputSlotId;
        [SerializeField] private int m_inputNodeId;
        [SerializeField] private int m_inputSlotId;

        public int outputNodeId
        {
            get { return m_outputNodeId; }
        }

        public int outputSlotId
        {
            get { return m_outputSlotId; }
        }

        public int inputNodeId
        {
            get { return m_inputNodeId; }
        }

        public int inputSlotId
        {
            get { return m_inputSlotId; }
        }

        public GraphEdgeData(int outputNodeId, int outputSlotId, int inputNodeId, int inputSlotId)
        {
            m_outputNodeId = outputNodeId;
            m_outputSlotId = outputSlotId;
            m_inputNodeId = inputNodeId;
            m_inputSlotId = inputSlotId;
        }
    }
}