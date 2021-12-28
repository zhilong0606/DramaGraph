using System;
using System.Collections.Generic;

namespace Tool.Export.Structure
{
    public class ListStructureInfo : CollectionStrucureInfo
    {
        protected BaseStructureInfo m_valueStructureInfo;

        public BaseStructureInfo valueStructureInfo { get { return m_valueStructureInfo; } }

        public sealed override EStructureType structureType { get { return EStructureType.List; } }

        public ListStructureInfo(BaseStructureInfo valueStructureInfo) : base(string.Format("List<{0}>", valueStructureInfo.name))
        {
            m_valueStructureInfo = valueStructureInfo;
        }
    }
}
