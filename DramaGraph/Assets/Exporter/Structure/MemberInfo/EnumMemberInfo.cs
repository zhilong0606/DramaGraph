using System;
using System.Collections.Generic;

namespace Tool.Export.Structure
{
    public class EnumMemberInfo : BaseMemberInfo
    {
        protected int m_index;

        public int index
        {
            get { return m_index; }
        }

        public EnumMemberInfo(string name, int index) : base (name)
        {
            m_index = index;
        }
    }
}
