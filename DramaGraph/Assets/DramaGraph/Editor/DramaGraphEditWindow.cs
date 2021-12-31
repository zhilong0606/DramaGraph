using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DramaScript;
using Google.Protobuf;
using Tool.Export;
using Tool.Export.Data;
using Tool.Export.Proto;
using Tool.Export.Structure;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GraphEditor.Drama
{
    public class DramaGraphEditWindow : EditorWindow
    {
        private string m_assetGuid;

        public string assetGuid
        {
            get { return m_assetGuid; }
        }

        public void Init(string assetGuid)
        {
            try
            {
                //if (m_assetGuid == assetGuid)
                //{
                //    return;
                //}
                Object asset = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(assetGuid));
                if (asset == null)
                {
                    return;
                }
                if (!EditorUtility.IsPersistent(asset))
                {
                    return;
                }
                string path = AssetDatabase.GetAssetPath(asset);
                string extension = Path.GetExtension(path);
                if (string.IsNullOrEmpty(extension))
                {
                    return;
                }
                m_assetGuid = assetGuid;
                string textGraph = File.ReadAllText(path, Encoding.UTF8);
                DramaGraphData graphData = JsonUtility.FromJson<DramaGraphData>(textGraph);

                GraphContext context = new GraphContext();
                context.window = this;
                context.nodeDefinePath = "Assets/DramaGraph/NodeDefine/NodeDefines.xml";
                Graph<DramaGraphData, DramaGraphView> graph = new Graph<DramaGraphData, DramaGraphView>();
                graph.Init(context);
                graph.actionOnSaveData = OnSaveGraphData;
                graph.actionOnExportData = OnExportGraphData;
                graph.SetData(graphData);

                rootVisualElement.Add(graph.view);
                titleContent = new GUIContent(asset.name.Split('/').Last());
                Repaint();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void OnSaveGraphData(DramaGraphData graphData)
        {
            var path = AssetDatabase.GUIDToAssetPath(m_assetGuid);
            if (string.IsNullOrEmpty(path))
                return;
            if (FileUtility.WriteToDisk(path, graphData))
                AssetDatabase.ImportAsset(path);
            //graphObject.isDirty = false;
        }

        private void OnExportGraphData(DramaGraphData graphData)
        {
            GraphNodeDefines defines = null;
            using (FileStream stream = File.Open("Assets/DramaGraph/NodeDefine/NodeDefines.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serialize = new XmlSerializer(typeof(GraphNodeDefines));
                //try
                {
                    defines = serialize.Deserialize(stream) as GraphNodeDefines;
                }
                //catch
                //{
                //}
            }
            DramaScriptGraphData scriptGraphData = new DramaScriptGraphData();
            foreach (var nodeData in graphData.nodeDataList)
            {
                DramaScriptNodeDataContainer container = new DramaScriptNodeDataContainer();
                container.TypeName = nodeData.defineName;
                GraphNodeDefine nodeDefine = defines.nodeList.Find(n => n.name == nodeData.defineName);
                switch (nodeData.defineName)
                {
                    case "PlayAnimation":
                    {
                        DramaScriptNodeDataPlayAnimation scriptNodeData = new DramaScriptNodeDataPlayAnimation();
                        scriptNodeData.Id = nodeData.id;
                        scriptNodeData.AnimationName = (nodeData.GetPortData(nodeDefine.portList.Find(p => p.name == "AnimationName").id) as GraphPortDataString).value;
                        scriptNodeData.Speed = (nodeData.GetPortData(nodeDefine.portList.Find(p => p.name == "Speed").id) as GraphPortDataFloat).value;
                        container.Buffers = GetBuffer(scriptNodeData);
                        break;
                    }
                    case "TimeEntry":
                    {
                        DramaScriptNodeDataTimeEntry scriptNodeData = new DramaScriptNodeDataTimeEntry();
                        scriptNodeData.Id = nodeData.id;
                        scriptNodeData.Time = (nodeData.GetPortData(nodeDefine.portList.Find(p => p.name == "Time").id) as GraphPortDataFloat).value;
                        container.Buffers = GetBuffer(scriptNodeData);
                            break;
                    }
                    case "Entry":
                    {
                        DramaScriptNodeDataEntry scriptNodeData = new DramaScriptNodeDataEntry();
                        scriptNodeData.Id = nodeData.id;
                        container.Buffers = GetBuffer(scriptNodeData);
                            break;
                    }
                    case "Exit":
                    {
                        DramaScriptNodeDataEntry scriptNodeData = new DramaScriptNodeDataEntry();
                        scriptNodeData.Id = nodeData.id;
                        container.Buffers = GetBuffer(scriptNodeData);
                            break;
                    }
                }
                scriptGraphData.NodeContainerList.Add(container);
            }
            foreach (var edgeData in graphData.edgeDataList)
            {
                DramaScriptEdgeData scriptEdgeData = new DramaScriptEdgeData()
                {
                    InputNodeId = edgeData.inputNodeId,
                    InputPortId = edgeData.inputPortId,
                    OutputNodeId = edgeData.outputNodeId,
                    OutputPortId = edgeData.outputPortId,
                };
                scriptGraphData.EdgeList.Add(scriptEdgeData);
            }
            string folderPath = "Assets/DramaGraph/NodeDatas/";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string outputPath = folderPath + "graphData.bytes";
            using (MemoryStream ms = new MemoryStream())
            {
                using (CodedOutputStream cos = new CodedOutputStream(ms))
                {
                    scriptGraphData.WriteTo(cos );
                    cos.Flush();
                    using (FileStream fs = File.Create(outputPath))
                    {
                        ms.Position = 0;
                        ms.WriteTo(fs);
                        fs.Close();
                    }
                }
            }
            //context.ilExportPath = "Assets/DramaGraph/NodeDefineIl/";
            //context.structureExportPath = "Assets/DramaGraph/NodeDefineData/";
            //context.namespaceStr = "DramaScript";
            //context.needExport = true;
            //context.extensionStr = "binary";
            //context.prefixStr = "DramaScriptData";
            //GraphNodeDefines defines = serialize.Deserialize(stream) as GraphNodeDefines;
            //foreach (var nodeDefine in defines.nodeList)
            //{
            //    ClassStructureInfo structureInfo = new ClassStructureInfo(nodeDefine.name);
            //    foreach (var portDefine in nodeDefine.portList)
            //    {
            //        if (!portDefine.isTrigger)
            //        {
            //            EBasicStructureType structureType = EBasicStructureType.Bool;
            //            switch (portDefine.valueType)
            //            {
            //                case EGraphPortValueType.Float:
            //                    structureType = EBasicStructureType.Single;
            //                    break;
            //                case EGraphPortValueType.String:
            //                    structureType = EBasicStructureType.String;
            //                    break;
            //            }
            //            BasicStructureInfo memberStructureInfo = context.structureManager.GetBasicStructureInfo(structureType);
            //            structureInfo.AddMember(memberStructureInfo, portDefine.name);
            //        }
            //    }
            //    context.structureManager.AddStructureInfo(structureInfo);
            //}
            //ProtoStructureExporter exporter = new ProtoStructureExporter();
            //exporter.Export(context);
        }

        private ByteString GetBuffer(IMessage obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (CodedOutputStream cos = new CodedOutputStream(ms))
                {
                    obj.WriteTo(cos);
                    cos.Flush();
                    ms.Position = 0;
                    return ByteString.FromStream(ms);
                }
            }
        }
    }
}