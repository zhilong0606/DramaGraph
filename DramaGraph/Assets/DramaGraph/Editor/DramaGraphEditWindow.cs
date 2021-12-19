using System;
using System.IO;
using System.Linq;
using System.Text;
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
        private DramaGraphView m_view;
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
                GraphObject graphObject = CreateInstance<GraphObject>();
                graphObject.hideFlags = HideFlags.HideAndDontSave;
                graphObject.SetGraphData(JsonUtility.FromJson<GraphData>(textGraph));
                //graphObject.graphData.messageManager = messageManager;
                //graphObject.graphData.OnEnable();
                //graphObject.graphData.ValidateGraph(););

                m_view = new DramaGraphView(this, graphObject.graphData);
                m_view.actionOnSaveGraphData = OnSaveGraphData;
                rootVisualElement.Add(m_view);
                titleContent = new GUIContent(asset.name.Split('/').Last());
                Repaint();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void OnSaveGraphData(GraphData graphData)
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