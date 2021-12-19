using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GraphEditor
{
    public class SearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        public Func<SearchTreeEntry, SearchWindowContext, Port, bool> funcOnSelectEntry;

        private Port m_connectPort;

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> entryList = new List<SearchTreeEntry>();
            entryList.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
            entryList.Add(new SearchTreeGroupEntry(new GUIContent("Example")) {level = 1});
            entryList.Add(new SearchTreeEntry(new GUIContent("float")) {level = 2, userData = typeof(GraphNodeViewFloat)});
            entryList.Add(new SearchTreeEntry(new GUIContent("binary_op")) {level = 2, userData = typeof(GraphNodeViewBinaryOp)});
            return entryList;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            if (funcOnSelectEntry != null)
            {
                return funcOnSelectEntry(searchTreeEntry, context, m_connectPort);
            }
            return false;
        }

        public void Open(Vector3 screenPosition)
        {
            SearchWindow.Open(new SearchWindowContext(screenPosition), this);
        }

        public void OpenAndConnectPort(Vector3 screenPosition, Port port)
        {
            m_connectPort = port;
            Open(screenPosition);
        }
    }
}