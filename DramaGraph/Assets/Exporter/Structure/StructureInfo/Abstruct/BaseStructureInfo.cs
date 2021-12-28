using System;
using System.Collections.Generic;

namespace Tool.Export.Structure
{
    public abstract class BaseStructureInfo
    {
        protected string m_name;

        public string name { get { return m_name; } }

        public abstract bool isCollection { get; }
        public abstract EStructureType structureType { get; }

        protected BaseStructureInfo(string name)
        {
            m_name = name;
        }

        public void Rename(string name)
        {
            m_name = name;
        }
    }
}
