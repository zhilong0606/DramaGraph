using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tool.Export.Data;
using Tool.Export.Structure;

namespace Tool.Export
{
    public class ExportContext
    {
        public string name;
        public string usingNamespaceStr;
        public string namespaceStr;
        public string extensionStr;
        public string prefixStr;
        public string postfixStr;
        public string ilExportPath;
        public string structureExportPath;
        public string dataExportPath;
        public string exporterPath;
        public bool needExport;
        public Assembly assembly;
        public List<string> tagNameList = new List<string>();
        public StructureManager structureManager = new StructureManager();
        public DataObjectManager dataObjManager = new DataObjectManager();
    }
}
