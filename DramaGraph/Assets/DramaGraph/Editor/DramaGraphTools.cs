using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Tool.Export;
using Tool.Export.Proto;
using Tool.Export.Structure;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace GraphEditor.Drama
{
    public static class DramaGraphTools
    {
        private const string m_fileExtension = "dramagraph";

        [MenuItem("Tools/Drama Script/Generate Codes", false)]
        public static void GenerateCodes()
        {
            using (FileStream stream = File.Open("Assets/DramaGraph/NodeDefine/NodeDefines.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serialize = new XmlSerializer(typeof(GraphNodeDefines));
                //try
                {
                    ExportContext context = new ExportContext();
                    context.ilExportPath = "Assets/DramaGraph/NodeDefineIl/";
                    context.structureExportPath = "Assets/DramaGraph/NodeDefineStructure/";
                    context.namespaceStr = "DramaScript";
                    context.needExport = true;
                    context.extensionStr = "binary";
                    context.prefixStr = "DramaScriptData";
                    GraphNodeDefines defines = serialize.Deserialize(stream) as GraphNodeDefines;
                    foreach (var nodeDefine in defines.nodeList)
                    {
                        ClassStructureInfo structureInfo = new ClassStructureInfo(nodeDefine.name);
                        foreach (var portDefine in nodeDefine.portList)
                        {
                            if (!portDefine.isTrigger)
                            {
                                EBasicStructureType structureType = EBasicStructureType.Bool;
                                switch (portDefine.valueType)
                                {
                                    case EGraphPortValueType.Float:
                                        structureType = EBasicStructureType.Single;
                                        break;
                                    case EGraphPortValueType.String:
                                        structureType = EBasicStructureType.String;
                                        break;
                                }
                                BasicStructureInfo memberStructureInfo = context.structureManager.GetBasicStructureInfo(structureType);
                                structureInfo.AddMember(memberStructureInfo, portDefine.name);
                            }
                        }
                        context.structureManager.AddStructureInfo(structureInfo);
                    }
                    ProtoStructureExporter exporter = new ProtoStructureExporter();
                    exporter.Export(context);
                }
                //catch
                //{
                //}
            }
        }

        [MenuItem("Assets/Create/Drama Script/Skill Drama Graph", false, 208)]
        public static void CreateSkillDramaGraph()
        {
            var graphItem = ScriptableObject.CreateInstance<NewFileAction>();
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, graphItem, string.Format("NewDramaGraph.{0}", m_fileExtension), null, null);
        }

        public static bool ShowGraphEditWindow(string path)
        {
            var guid = AssetDatabase.AssetPathToGUID(path);
            var extension = Path.GetExtension(path);
            if (string.IsNullOrEmpty(extension))
            {
                return false;
            }
            extension = extension.Substring(1).ToLowerInvariant();
            if (extension != m_fileExtension)
            {
                return false;
            }
            var foundWindow = false;
            //foreach (var w in Resources.FindObjectsOfTypeAll<DramaScriptEditWindow>())
            //{
            //    if (w.assetGuid == guid)
            //    {
            //        foundWindow = true;
            //        w.Focus();
            //    }
            //}

            if (!foundWindow)
            {
                DramaGraphEditWindow window = new DramaGraphEditWindow();
                window.Init(guid);
                window.Show();
            }
            return true;
        }

        [OnOpenAsset(0)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            var path = AssetDatabase.GetAssetPath(instanceID);
            return ShowGraphEditWindow(path);
        }


        public class NewFileAction : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var graph = new GraphData();
                FileUtility.WriteToDisk(pathName, graph);
                AssetDatabase.Refresh();
            }
        }
    }
}
