using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DramaScript
{
    public abstract class DramaScriptNode
    {
        protected int m_id;
        protected object m_data;

        private List<DramaScriptNodeFuncInfo> m_inputFuncInfoList = new List<DramaScriptNodeFuncInfo>();
        private List<DramaScriptNodeFuncInfo> m_outputFuncInfoList = new List<DramaScriptNodeFuncInfo>();
        private List<DramaScriptNodeTriggerInfo> m_inputTriggerInfoList = new List<DramaScriptNodeTriggerInfo>();
        private List<DramaScriptNodeTriggerInfo> m_outputTriggerInfoList = new List<DramaScriptNodeTriggerInfo>();

        public int id
        {
            get { return m_id; }
        }

        public void Init(int id, object data)
        {
            m_id = id;
            m_data = data;
            OnInit();
            OnInitInputTrigger();
        }

        public void UnInit()
        {
            OnUnInit();
        }

        public void Tick(DramaScriptTime deltaTime)
        {
            OnTick(deltaTime);
        }

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

        public T GetInputValue<T>(int id, T bindValue)
        {
            DramaScriptNodeFuncInfo<T> info = GetInputFuncInfo<T>(id, false);
            if (info != null && info.isValid)
            {
                return info.Invoke();
            }
            return bindValue;
        }

        public void RegisterOutputFunc<T>(int id, Func<T> func)
        {
            DramaScriptNodeFuncInfo<T> info = GetOutputFuncInfo<T>(id, true);
            info.Register(func);
        }

        public void UnRegisterOutputFunc<T>(int id, Func<T> func)
        {
            DramaScriptNodeFuncInfo<T> info = GetOutputFuncInfo<T>(id, false);
            if (info != null)
            {
                info.UnRegister(func);
            }
        }

        public Func<T> GetOutputFunc<T>(int id)
        {
            DramaScriptNodeFuncInfo<T> info = GetInputFuncInfo<T>(id, false);
            if (info != null && info.isValid)
            {
                return info.Invoke;
            }
            return null;
        }

        public void RegisterInputTrigger(int id, Action action)
        {
            DramaScriptNodeTriggerInfo info = GetInputTriggerInfo(id, true);
            info.Register(action);
        }

        public void UnRegisterInputTrigger(int id, Action action)
        {
            DramaScriptNodeTriggerInfo info = GetInputTriggerInfo(id, false);
            if (info != null)
            {
                info.UnRegister(action);
            }
        }

        public Action GetInputTriggerAction(int id)
        {
            DramaScriptNodeTriggerInfo info = GetInputTriggerInfo(id, false);
            if (info != null)
            {
                return info.Invoke;
            }
            return null;
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

        public bool InvokeOutputTrigger(int id)
        {
            DramaScriptNodeTriggerInfo info = GetOutputTriggerInfo(id, false);
            if (info != null)
            {
                info.Invoke();
                return true;
            }
            return false;
        }

        protected virtual void OnInit() { }

        protected virtual void OnInitInputTrigger() { }

        protected virtual void OnUnInit() { }

        protected virtual void OnTick(DramaScriptTime deltaTime) { }

        private DramaScriptNodeFuncInfo<T> GetInputFuncInfo<T>(int id, bool autoCreate)
        {
            return GetInputFuncInfo<T>(m_inputFuncInfoList, id, autoCreate);
        }

        private DramaScriptNodeFuncInfo<T> GetOutputFuncInfo<T>(int id, bool autoCreate)
        {
            return GetInputFuncInfo<T>(m_outputFuncInfoList, id, autoCreate);
        }

        private DramaScriptNodeTriggerInfo GetInputTriggerInfo(int id, bool autoCreate)
        {
            return GetTriggerInfo(m_inputTriggerInfoList, id, autoCreate);
        }

        private DramaScriptNodeTriggerInfo GetOutputTriggerInfo(int id, bool autoCreate)
        {
            return GetTriggerInfo(m_outputTriggerInfoList, id, autoCreate);
        }

        private DramaScriptNodeFuncInfo<T> GetInputFuncInfo<T>(List<DramaScriptNodeFuncInfo> list, int id, bool autoCreate)
        {
            int count = list.Count;
            for (int i = 0; i < count; ++i)
            {
                if (list[i].id == id)
                {
                    return list[i] as DramaScriptNodeFuncInfo<T>;
                }
            }
            if (autoCreate)
            {
                DramaScriptNodeFuncInfo<T> info = new DramaScriptNodeFuncInfo<T>(id);
                list.Add(info);
                return info;
            }
            return null;
        }

        private DramaScriptNodeTriggerInfo GetTriggerInfo(List<DramaScriptNodeTriggerInfo> list, int id, bool autoCreate)
        {
            int count = list.Count;
            for (int i = 0; i < count; ++i)
            {
                if (list[i].id == id)
                {
                    return list[i];
                }
            }
            if (autoCreate)
            {
                DramaScriptNodeTriggerInfo info = new DramaScriptNodeTriggerInfo(id);
                list.Add(info);
                return info;
            }
            return null;
        }
    }
}
