using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DramaGraphNodeData
{
    [SerializeField]
    private int m_id;

    public DramaGraphNodeData(int id)
    {
        m_id = id;
    }
}
