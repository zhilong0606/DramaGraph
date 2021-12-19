using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DramaGraphSlotData
{
    [SerializeField]
    private int m_id;

    public DramaGraphSlotData(int id)
    {
        m_id = id;
    }
}
