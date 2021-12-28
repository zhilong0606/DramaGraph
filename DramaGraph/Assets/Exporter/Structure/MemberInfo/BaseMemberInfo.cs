using System;
using System.Collections.Generic;

namespace Tool.Export.Structure
{
    public abstract class BaseMemberInfo
    {
        protected string m_name;

        public string name
        {
            get { return m_name; }
        }

        protected BaseMemberInfo(string name)
        {
            m_name = name;
        }

        public void Rename(string name)
        {
            m_name = name;
        }
    }
}
