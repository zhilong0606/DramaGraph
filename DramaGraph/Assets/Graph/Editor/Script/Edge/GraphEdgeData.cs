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
        [SerializeField] private int m_outputPortId;
        [SerializeField] private int m_inputNodeId;
        [SerializeField] private int m_inputPortId;

        public int outputNodeId
        {
            get { return m_outputNodeId; }
        }

        public int outputPortId
        {
            get { return m_outputPortId; }
        }

        public int inputNodeId
        {
            get { return m_inputNodeId; }
        }

        public int inputPortId
        {
            get { return m_inputPortId; }
        }

        public void Init(int outputNodeId, int outputSlotId, int inputNodeId, int inputSlotId)
        {
            m_outputNodeId = outputNodeId;
            m_outputPortId = outputSlotId;
            m_inputNodeId = inputNodeId;
            m_inputPortId = inputSlotId;
        }
    }
}