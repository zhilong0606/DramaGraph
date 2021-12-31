using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DramaScript
{
    public class DramaScriptNodeFuncInfo<T> : DramaScriptNodeFuncInfo
    {
        private Func<T> m_func;

        public override bool isValid
        {
            get { return m_func != null; }
        }

        public DramaScriptNodeFuncInfo(int id) : base(id)
        {
        }

        public void Register(Func<T> func)
        {
            m_func = func;
        }

        public void UnRegister(Func<T> func)
        {
            if(m_func == func)
            {
                m_func = null;
            }
        }

        public T Invoke()
        {
            if (m_func != null)
            {
                return m_func();
            }
            throw new Exception("Cannot Invoke: func is null");
        }
    }

    public abstract class DramaScriptNodeFuncInfo
    {
        public int id;

        public abstract bool isValid { get; }

        public DramaScriptNodeFuncInfo(int id)
        {
            this.id = id;
        }
    }
}
