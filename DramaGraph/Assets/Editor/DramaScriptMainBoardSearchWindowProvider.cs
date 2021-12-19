using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DramaScriptMainBoardSearchWindowProvider : ScriptableObject, ISearchWindowProvider
{
    public Func<SearchTreeEntry, SearchWindowContext, bool> funcOnSelectEntry;

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> entryList = new List<SearchTreeEntry>();
        entryList.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
        entryList.Add(new SearchTreeGroupEntry(new GUIContent("Example")) {level = 1});
        entryList.Add(new SearchTreeEntry(new GUIContent("float")) {level = 2, userData = typeof(DramaScriptFloatNodeView)});
        entryList.Add(new SearchTreeEntry(new GUIContent("binary_op")) { level = 2, userData = typeof(DramaScriptBinaryOpNodeView) });
        return entryList;
    }

    public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        if (funcOnSelectEntry != null)
        {
            return funcOnSelectEntry(searchTreeEntry, context);
        }
        return false;
    }

    public void Open(Vector3 screenPosition)
    {
        SearchWindow.Open(new SearchWindowContext(screenPosition), this);
    }
}
