using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DramaScript
{
    public class DramaScriptNode
    {
        private List<DramaScriptNodeFuncInfo> m_inputFuncInfoList = new List<DramaScriptNodeFuncInfo>();
        private List<DramaScriptNodeTriggerInfo> m_outputTriggerInfoList = new List<DramaScriptNodeTriggerInfo>();
        private List<DramaScriptNodeTriggerInfo> m_outputTriggerInfoList = new List<DramaScriptNodeTriggerInfo>();

        public void RegisterInputFunc<T>(int id, Func<T> func)
        {
            DramaScriptNodeFuncInfo<T> info = GetInputFuncInfo<T>(id, true);
            info.Register(func);
        }

        public void UnRegisterInputFunc<T>(int id, Func<T> func)
        {
            DramaScriptNodeFuncInfo<T> info = GetInputFuncInfo<T>(id, false);
            if (info != null)
            {
                info.UnRegister(func);
            }
        }

        public void GetInputValue<T>(int id)
        {

        }

        public void RegisterOutputTrigger(int id, Action action)
        {
            DramaScriptNodeTriggerInfo info = GetOutputTriggerInfo(id, true);
            info.Register(action);
        }

        public void UnRegisterOutputTrigger(int id, Action action)
        {
            DramaScriptNodeTriggerInfo info = GetOutputTriggerInfo(id, false);
            if (info != null)
            {
                info.UnRegister(action);
            }
        }

        private DramaScriptNodeFuncInfo<T> GetInputFuncInfo<T>(int id, bool autoCreate)
        {
            int count = m_inputFuncInfoList.Count;
            for (int i = 0; i < count; ++i)
            {
                if (m_inputFuncInfoList[i].id == id)
                {
                    return m_inputFuncInfoList[i] as DramaScriptNodeFuncInfo<T>;
                }
            }
            if (autoCreate)
            {
                DramaScriptNodeFuncInfo<T> info = new DramaScriptNodeFuncInfo<T>(id);
                m_inputFuncInfoList.Add(info);
                return info;
            }
            return null;
        }

        private DramaScriptNodeTriggerInfo GetOutputTriggerInfo(int id, bool autoCreate)
        {
            int count = m_outputTriggerInfoList.Count;
            for (int i = 0; i < count; ++i)
            {
                if (m_outputTriggerInfoList[i].id == id)
                {
                    return m_outputTriggerInfoList[i];
                }
            }
            if (autoCreate)
            {
                DramaScriptNodeTriggerInfo info = new DramaScriptNodeTriggerInfo(id);
                m_outputTriggerInfoList.Add(info);
                return info;
            }
            return null;
        }
    }
}
