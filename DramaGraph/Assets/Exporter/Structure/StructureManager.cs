using System;
using System.Collections.Generic;

namespace Tool.Export.Structure
{
    public class StructureManager
    {
        public Dictionary<string, BaseStructureInfo> structureMap = new Dictionary<string, BaseStructureInfo>();
        private static Dictionary<int, BasicStructureInfo> m_basicStructureInfoMap = new Dictionary<int, BasicStructureInfo>();

        static StructureManager()
        {
            Dictionary<int, string> basicNameMap = new Dictionary<int, string>()
            {
                {(int)EBasicStructureType.Bool, "bool" },
                {(int)EBasicStructureType.Int8, "byte" },
                {(int)EBasicStructureType.Int16, "short" },
                {(int)EBasicStructureType.Int32, "int" },
                {(int)EBasicStructureType.Int64, "long" },
                {(int)EBasicStructureType.Single, "float" },
                {(int)EBasicStructureType.Double, "double" },
                {(int)EBasicStructureType.String, "string" },
                {(int)EBasicStructureType.Bytes, "bytes" },
            };
            foreach (var kv in basicNameMap)
            {
                EBasicStructureType basicType = (EBasicStructureType)kv.Key;
                m_basicStructureInfoMap.Add(kv.Key, new BasicStructureInfo(basicType.ToString(), basicType, kv.Value));
            }
        }

        public void Clear()
        {
            structureMap.Clear();
            foreach (var kv in m_basicStructureInfoMap)
            {
                AddStructureInfo(kv.Value);
            }
        }

        public BasicStructureInfo GetBasicStructureInfo(EBasicStructureType structureType)
        {
            BasicStructureInfo info;
            m_basicStructureInfoMap.TryGetValue((int)structureType, out info);
            return info;
        }

        public void AddStructureInfo(BaseStructureInfo structureInfo)
        {
            structureMap.Add(structureInfo.name, structureInfo);
        }

        public BaseStructureInfo GetStructureInfo(string structureName)
        {
            BaseStructureInfo structureInfo;
            if (structureMap.TryGetValue(structureName, out structureInfo))
            {
                return structureInfo;
            }
            return null;
        }
    }
}
