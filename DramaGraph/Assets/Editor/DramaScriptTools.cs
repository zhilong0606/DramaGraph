using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public static class DramaScriptTools
{
    [MenuItem("Assets/Create/Drama Script/Skill Drama Graph", false, 208)]
    public static void CreateSkillDramaGraph()
    {
        var graphItem = ScriptableObject.CreateInstance<NewDramaGraphAction>();
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, graphItem, string.Format("New Drama Graph.{0}", DramaScriptDefine.dramaScriptGraphExtension), null, null);
    }
}


class NewDramaGraphAction : EndNameEditAction
{
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        var graph = new DramaScriptGraphData();
        FileUtility.WriteToDisk(pathName, graph);
        AssetDatabase.Refresh();

        UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<Shader>(pathName);
        Selection.activeObject = obj;
    }
}
