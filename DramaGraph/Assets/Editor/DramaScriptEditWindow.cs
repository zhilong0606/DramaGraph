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


public class DramaScriptEditWindow : EditorWindow
{
    //[MenuItem("Window/UIElements/DramaScriptEditWindow #q")]
    //public static void ShowGraphEditWindow(string path)
    //{
    //    DramaScriptEditWindow wnd = GetWindow<DramaScriptEditWindow>();
    //    wnd.Init();
    //    wnd.titleContent = new GUIContent("DramaScriptEditWindow");
    //}
    public static bool ShowGraphEditWindow(string path)
    {
        var guid = AssetDatabase.AssetPathToGUID(path);
        var extension = Path.GetExtension(path);
        if (string.IsNullOrEmpty(extension))
            return false;
        // Path.GetExtension returns the extension prefixed with ".", so we remove it. We force lower case such that
        // the comparison will be case-insensitive.
        extension = extension.Substring(1).ToLowerInvariant();
        if (extension != DramaScriptDefine.dramaScriptGraphExtension && extension != DramaScriptDefine.dramaScriptGraphExtension)
            return false;

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
            var window = CreateInstance<DramaScriptEditWindow>();
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

    private DramaScriptMainBoardGraphView m_view;
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
            DramaGraphObject graphObject = CreateInstance<DramaGraphObject>();
            graphObject.hideFlags = HideFlags.HideAndDontSave;
            graphObject.SetGraphData(JsonUtility.FromJson<DramaScriptGraphData>(textGraph));
            //graphObject.graphData.messageManager = messageManager;
            //graphObject.graphData.OnEnable();
            //graphObject.graphData.ValidateGraph(););

            m_view = new DramaScriptMainBoardGraphView(this, graphObject.graphData);
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

    private void OnSaveGraphData(DramaScriptGraphData graphData)
    {
        var path = AssetDatabase.GUIDToAssetPath(m_assetGuid);
        if (string.IsNullOrEmpty(path))
            return;
        if (FileUtility.WriteToDisk(path, graphData))
            AssetDatabase.ImportAsset(path);
        //graphObject.isDirty = false;
    }
}