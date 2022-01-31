using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DramaScript;
using Google.Protobuf;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GraphEditor.Drama
{
    public class DramaGraphEditWindow : EditorWindow
    {
        private string m_assetGuid;
        private Graph<DramaGraphData, DramaGraphView> m_graph;

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
                context.nodeDefineContainer = DramaGraphTools.GetGraphNodeDefineContainer();
                m_graph = new Graph<DramaGraphData, DramaGraphView>();
                m_graph.Init(context);
                m_graph.actionOnSaveData = OnSaveGraphData;
                m_graph.actionOnExportData = OnExportGraphData;
                m_graph.SetData(graphData);

                rootVisualElement.Add(m_graph.view);
                titleContent = new GUIContent(asset.name.Split('/').Last());
                Repaint();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void OnSaveGraphData()
        {
            var path = AssetDatabase.GUIDToAssetPath(m_assetGuid);
            if (string.IsNullOrEmpty(path))
                return;
            if (FileUtility.WriteToDisk(path, m_graph.data))
                AssetDatabase.ImportAsset(path);
            //graphObject.isDirty = false;
        }

        private void OnExportGraphData()
        {
            GraphNodeDefineContainer nodeDefineContainer = null;
            using (FileStream stream = File.Open("Assets/DramaGraph/NodeDefine/NodeDefines.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serialize = new XmlSerializer(typeof(GraphNodeDefineContainer));
                //try
                {
                    nodeDefineContainer = serialize.Deserialize(stream) as GraphNodeDefineContainer;
                }
                //catch
                //{
                //}
            }
            DramaScriptGraphData scriptGraphData = new DramaScriptGraphData();
            foreach (var nodeData in m_graph.data.nodeDataList)
            {
                DramaScriptNodeDataContainer container = new DramaScriptNodeDataContainer();
                container.Id = nodeData.id;
                container.TypeName = nodeData.defineName;
                GraphNodeDefine nodeDefine = nodeDefineContainer.GetNode(nodeData.defineName);
                container.Buffers = DramaGraphEditorFactory.CreateNodeDataBuffer(nodeData, nodeDefine);
                scriptGraphData.NodeContainerList.Add(container);
            }
            foreach (var edgeData in m_graph.data.edgeDataList)
            {
                GraphPort inputPort = m_graph.GetPort(edgeData.inputNodeId, edgeData.inputPortId);
                GraphPort outputPort = m_graph.GetPort(edgeData.outputNodeId, edgeData.outputPortId);
                if (inputPort != null && outputPort != null && inputPort.define.valueType == outputPort.define.valueType)
                {
                    DramaScriptEdgeData scriptEdgeData = new DramaScriptEdgeData()
                    {
                        TypeName = inputPort.define.valueType.ToString(),
                        InputNodeId = edgeData.inputNodeId,
                        InputPortId = edgeData.inputPortId,
                        OutputNodeId = edgeData.outputNodeId,
                        OutputPortId = edgeData.outputPortId,
                    };
                    scriptGraphData.EdgeList.Add(scriptEdgeData);
                }
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
    }
}