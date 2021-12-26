using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
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
                graph.SetData(graphData);

                rootVisualElement.Add(graph.view);
                titleContent = new GUIContent(asset.name.Split('/').Last());
                Repaint();
            }
            catch (Exception)
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
    }
}