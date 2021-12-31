using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DramaScript
{
    public class DramaScriptNodeTriggerInfo
    {
        public int id;
        private List<Action> m_actionList = new List<Action>();

        public DramaScriptNodeTriggerInfo(int id)
        {
            this.id = id;
        }

        public void Register(Action action)
        {
            if (!m_actionList.Contains(action))
            {
                m_actionList.Add(action);
            }
        }

        public void UnRegister(Action action)
        {
            m_actionList.Remove(action);
        }

        public void Invoke()
        {
            int count = m_actionList.Count;
            for (int i = 0; i < count; ++i)
            {
                try
                {
                    m_actionList[i].Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message + "\n" + e.StackTrace);
                }
            }
        }
    }
}
