using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace GraphEditor.Drama
{
    public static class DramaGraphTools
    {
        private const string m_fileExtension = "dramagraph";

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
