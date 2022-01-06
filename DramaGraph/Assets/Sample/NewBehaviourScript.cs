using System;
using System.Collections;
using System.Collections.Generic;
using DramaScript;
using UnityEditor;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    private DramaScript.DramaScript m_script;

    private void Start()
    {
        string folderPath = "Assets/DramaGraph/NodeDatas/";
        string outputPath = folderPath + "graphData.bytes";
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(outputPath);
        if (textAsset != null)
        {
            DramaScriptGraphData scriptGraphData = DramaScriptGraphData.Parser.ParseFrom(textAsset.bytes);
            m_script = new DramaScript.DramaScript();
            m_script.Load(scriptGraphData);
            m_script.actionOnEnd = OnEnd;
            m_script.Start();
        }
    }

    private void OnEnd()
    {
        Debug.LogError("OnEnd");
    }

    private void Update()
    {
        if (m_script != null)
        {
            m_script.Tick(new DramaScriptTime(Time.deltaTime));
        }
    }
}
