using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Tool.Export.Proto
{
    public class ProtoExporter
    {
        private string m_codeFolderPath;
        private string m_dataFolderPath;
        private string m_ilFolderPath;

        public void Export(string ilFolderPath, string codeFolderPath, string dataFolderPath)
        {
            m_ilFolderPath = ilFolderPath;
            m_codeFolderPath = codeFolderPath;
            m_dataFolderPath = dataFolderPath;
            CheckAndCreateFolderPath(ilFolderPath);
            CheckAndCreateFolderPath(codeFolderPath);
            CheckAndCreateFolderPath(dataFolderPath);
            ExportIl(ilFolderPath);
        }

        private void ExportIl(string ilFolderPath)
        {
            CheckAndCreateFolderPath(ilFolderPath);
        }

        private void CheckAndCreateFolderPath(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private void RunProtoc()
        {
            //string protoOutputFilePath = m_protoOutputPath + m_outputName + ".proto";
            //string csOutputFilePath = m_csOutputPath + m_outputName + ".cs";
            string argStr = null;//string.Format("--csharp_out={0} {1}", m_csOutputPath, protoOutputFilePath);
            string errorMsg;
            //if (!Utilities.ExportUtility.RunExe("protoc", argStr, out errorMsg))
            //{
            //    throw new Exception(errorMsg);
            //}
        }
    }
}
