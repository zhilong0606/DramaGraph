using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using DramaScript;
using Tool;
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

        [MenuItem("Tools/Drama Script/Read Codes", false)]
        public static void ReadCodes()
        {
            string folderPath = "Assets/DramaGraph/NodeDatas/";
            string outputPath = folderPath + "graphData.bytes";

            TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(outputPath);
            if (textAsset != null)
            {
                DramaScriptGraphData scriptGraphData = DramaScriptGraphData.Parser.ParseFrom(textAsset.bytes);
                foreach (var container in scriptGraphData.NodeContainerList)
                {
                    switch (container.TypeName)
                    {
                        case "PlayAnimation":
                        {
                            DramaScriptNodeDataPlayAnimation scriptNodeData = DramaScriptNodeDataPlayAnimation.Parser.ParseFrom(container.Buffers.ToByteArray());
                            int a = 0;
                                break;
                        }
                    }
                }
            }
        }

        [MenuItem("Tools/Drama Script/Generate Codes", false)]
        public static void GenerateCodes()
        {
            string codeAutoGenFolderPath = "Assets/DramaGraph/NodeDefineStructure/";
            using (FileStream stream = File.Open("Assets/DramaGraph/NodeDefine/NodeDefines.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serialize = new XmlSerializer(typeof(GraphNodeDefineContainer));
                //try
                {
                    GraphNodeDefineContainer nodeDefineContainer = serialize.Deserialize(stream) as GraphNodeDefineContainer;
                    GenerateStructureCode(codeAutoGenFolderPath, nodeDefineContainer);
                    GenerateScriptNodeCode(nodeDefineContainer);
                    GenerateDramaGraphEditorFactoryCode(codeAutoGenFolderPath, nodeDefineContainer);
                    GenerateDramaScriptFactoryCode(codeAutoGenFolderPath, nodeDefineContainer);
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

        private static void GenerateStructureCode(string codeAutoGenFolderPath, GraphNodeDefineContainer nodeDefineContainer)
        {
            ExportContext context = new ExportContext();
            context.ilExportPath = "Assets/DramaGraph/NodeDefineIl/";
            context.structureExportPath = codeAutoGenFolderPath;
            context.namespaceStr = "DramaScript";
            context.needExport = true;
            context.extensionStr = "binary";
            context.prefixStr = "";

            ClassStructureInfo nodeContainerStructureInfo = new ClassStructureInfo("DramaScriptNodeDataContainer");
            nodeContainerStructureInfo.AddMember(context.structureManager.GetBasicStructureInfo(EBasicStructureType.Int32), "id");
            nodeContainerStructureInfo.AddMember(context.structureManager.GetBasicStructureInfo(EBasicStructureType.String), "typeName");
            nodeContainerStructureInfo.AddMember(context.structureManager.GetBasicStructureInfo(EBasicStructureType.Bytes), "buffers");
            context.structureManager.AddStructureInfo(nodeContainerStructureInfo);

            ClassStructureInfo edgeStructureInfo = new ClassStructureInfo("DramaScriptEdgeData");
            edgeStructureInfo.AddMember(context.structureManager.GetBasicStructureInfo(EBasicStructureType.String), "typeName");
            edgeStructureInfo.AddMember(context.structureManager.GetBasicStructureInfo(EBasicStructureType.Int32), "inputNodeId");
            edgeStructureInfo.AddMember(context.structureManager.GetBasicStructureInfo(EBasicStructureType.Int32), "inputPortId");
            edgeStructureInfo.AddMember(context.structureManager.GetBasicStructureInfo(EBasicStructureType.Int32), "outputNodeId");
            edgeStructureInfo.AddMember(context.structureManager.GetBasicStructureInfo(EBasicStructureType.Int32), "outputPortId");
            context.structureManager.AddStructureInfo(edgeStructureInfo);

            ClassStructureInfo graphStructureInfo = new ClassStructureInfo("DramaScriptGraphData");
            graphStructureInfo.AddMember(new ListStructureInfo(nodeContainerStructureInfo), "nodeContainerList");
            graphStructureInfo.AddMember(new ListStructureInfo(edgeStructureInfo), "edgeList");
            context.structureManager.AddStructureInfo(graphStructureInfo);

            foreach (var nodeDefine in nodeDefineContainer.nodeList)
            {
                ClassStructureInfo structureInfo = new ClassStructureInfo("DramaScriptNodeData" + nodeDefine.name);
                foreach (var portDefine in nodeDefine.portList)
                {
                    if (!portDefine.isTrigger)
                    {
                        EBasicStructureType structureType = EBasicStructureType.Bool;
                        switch ((EGraphPortValueType)portDefine.valueType)
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

        private static void GenerateScriptNodeCode(GraphNodeDefineContainer nodeDefineContainer)
        {
            foreach (var nodeDefine in nodeDefineContainer.nodeList)
            {
                FileWriter writer = new FileWriter();
                writer.AppendLine("using UnityEngine;");
                writer.AppendLine();
                writer.AppendLine("namespace DramaScript");
                writer.StartCodeBlock();
                {
                    writer.AppendLine("public partial class DramaScriptNode{0}", nodeDefine.name);
                    writer.StartCodeBlock();
                    {
                        writer.AppendLine("protected DramaScriptNodeData{0} specificData", nodeDefine.name);
                        writer.StartCodeBlock();
                        {
                            writer.AppendLine("get {{ return m_data as DramaScriptNodeData{0}; }}", nodeDefine.name);
                        }
                        writer.EndCodeBlock();

                        List<GraphPortDefine> inputTriggerPortDefineList = new List<GraphPortDefine>();
                        foreach (var portDefine in nodeDefine.portList)
                        {
                            writer.AppendLine();
                            if (portDefine.isTrigger)
                            {
                                if (portDefine.dirType != EGraphPortDirType.Input)
                                {
                                    writer.AppendLine("private void Trigger{0}()", portDefine.name);
                                    writer.StartCodeBlock();
                                    {
                                        writer.AppendLine("InvokeOutputTrigger({0});", portDefine.id);
                                    }
                                    writer.EndCodeBlock();
                                }
                                else
                                {
                                    inputTriggerPortDefineList.Add(portDefine);
                                }
                            }
                            else
                            {
                                writer.AppendLine("private {0} Get{1}()", GraphUtility.GetPortValueTypeName(portDefine.valueType), portDefine.name);
                                writer.StartCodeBlock();
                                {
                                    writer.AppendLine("return GetInputValue({0}, specificData.{1});", portDefine.id, portDefine.name);
                                }
                                writer.EndCodeBlock();
                            }
                        }

                        if (inputTriggerPortDefineList.Count > 0)
                        {
                            writer.AppendLine();
                            writer.AppendLine("protected override void OnInitInputTrigger()");
                            writer.StartCodeBlock();
                            {
                                foreach (var portDefine in inputTriggerPortDefineList)
                                {
                                    writer.AppendLine("RegisterInputTrigger({0}, On{1});", portDefine.id, portDefine.name);
                                }
                            }
                            writer.EndCodeBlock();

                            writer.AppendLine();
                            foreach (var portDefine in inputTriggerPortDefineList)
                            {
                                writer.AppendLine("partial void On{0}();", portDefine.name);
                            }
                        }
                    }
                    writer.EndCodeBlock();
                }
                writer.EndCodeBlock();
                writer.WriteFile(string.Format("Assets/DramaScript/Node/AutoGen/DramaScriptNode{0}_AutoGen.cs", nodeDefine.name));
            }
        }

        private static void GenerateDramaGraphEditorFactoryCode(string codeAutoGenFolderPath, GraphNodeDefineContainer nodeDefineContainer)
        {
            FileWriter writer = new FileWriter();
            writer.AppendLine("using DramaScript;");
            writer.AppendLine("using Google.Protobuf;");
            writer.AppendLine();
            writer.AppendLine("namespace GraphEditor.Drama");
            writer.StartCodeBlock();
            {
                writer.AppendLine("public static partial class DramaGraphEditorFactory");
                writer.StartCodeBlock();
                {
                    writer.AppendLine("public static ByteString CreateNodeDataBuffer(GraphNodeData nodeData, GraphNodeDefine nodeDefine)");
                    writer.StartCodeBlock();
                    {
                        writer.AppendLine("switch (nodeData.defineName)");
                        writer.StartCodeBlock();
                        {
                            foreach (GraphNodeDefine nodeDefine in nodeDefineContainer.nodeList)
                            {
                                writer.AppendLine("case \"{0}\":", nodeDefine.name);
                                writer.StartCodeBlock();
                                {
                                    writer.AppendLine("DramaScriptNodeData{0} scriptNodeData = new DramaScriptNodeData{0}();", nodeDefine.name);
                                    foreach(GraphPortDefine portDefine in nodeDefine.portList)
                                    {
                                        if (portDefine.dirType == EGraphPortDirType.Input && !portDefine.isTrigger)
                                        {
                                            writer.AppendLine("scriptNodeData.{0} = GetPortData{1}(nodeData, nodeDefine, \"{0}\");", portDefine.name, portDefine.valueType);
                                        }
                                    }
                                    writer.AppendLine("return GetBuffer(scriptNodeData);");
                                }
                                writer.EndCodeBlock();
                            }
                        }
                        writer.EndCodeBlock();
                        writer.AppendLine("return null;");
                    }
                    writer.EndCodeBlock();
                }
                writer.EndCodeBlock();
            }
            writer.EndCodeBlock();
            writer.WriteFile(codeAutoGenFolderPath + "Editor/DramaGraphEditorFactory_AutoGen.cs");
        }

        private static void GenerateDramaScriptFactoryCode(string codeAutoGenFolderPath, GraphNodeDefineContainer nodeDefineContainer)
        {
            FileWriter writer = new FileWriter();
            writer.AppendLine("using Google.Protobuf;");
            writer.AppendLine();
            writer.AppendLine("namespace DramaScript");
            writer.StartCodeBlock();
            {
                writer.AppendLine("public static partial class DramaScriptFactory");
                writer.StartCodeBlock();
                {
                    writer.AppendLine("public static DramaScriptNode CreateNode(string typeName)");
                    writer.StartCodeBlock();
                    {
                        writer.AppendLine("switch (typeName)");
                        writer.StartCodeBlock();
                        {
                            foreach (GraphNodeDefine nodeDefine in nodeDefineContainer.nodeList)
                            {
                                writer.AppendLine("case \"{0}\":", nodeDefine.name);
                                writer.AppendLine("return new DramaScriptNode{0}();", nodeDefine.name);
                            }
                        }
                        writer.EndCodeBlock();
                        writer.AppendLine("return null;");
                    }
                    writer.EndCodeBlock();
                    writer.AppendLine();

                    writer.AppendLine("public static object ParseNodeData(string typeName, ByteString  buffer)");
                    writer.StartCodeBlock();
                    {
                        writer.AppendLine("switch (typeName)");
                        writer.StartCodeBlock();
                        {
                            foreach (GraphNodeDefine nodeDefine in nodeDefineContainer.nodeList)
                            {
                                writer.AppendLine("case \"{0}\":", nodeDefine.name);
                                writer.AppendLine("return DramaScriptNodeData{0}.Parser.ParseFrom(buffer.ToByteArray());", nodeDefine.name);
                            }
                        }
                        writer.EndCodeBlock();
                        writer.AppendLine("return null;");
                    }
                    writer.EndCodeBlock();
                }
                writer.EndCodeBlock();
            }
            writer.EndCodeBlock();
            writer.WriteFile(codeAutoGenFolderPath + "DramaScriptFactory.cs");
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
