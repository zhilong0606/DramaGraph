using System;
using System.Collections.Generic;

namespace Tool.Export.Structure
{
    public class DictionaryStructureInfo : CollectionStrucureInfo
    {
        protected BaseStructureInfo m_keyStructureInfo;
        protected BaseStructureInfo m_valueStructureInfo;

        public BaseStructureInfo keyStructureInfo { get { return m_keyStructureInfo; } }
        public BaseStructureInfo valueStructureInfo { get { return m_valueStructureInfo; } }

        public sealed override EStructureType structureType { get { return EStructureType.Dictionary; } }

        public DictionaryStructureInfo(BaseStructureInfo keyStructureInfo, BaseStructureInfo valueStructureInfo) : base(string.Format("Dictionary<{0},{1}>", keyStructureInfo.name, valueStructureInfo.name))
        {
            m_keyStructureInfo = keyStructureInfo;
            m_valueStructureInfo = valueStructureInfo;
        }
    }
}
