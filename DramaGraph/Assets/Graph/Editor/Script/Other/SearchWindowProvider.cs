using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GraphEditor
{
    public class SearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        public Func<SearchTreeEntry, SearchWindowContext, GraphPortView, bool> funcOnSelectEntry;
        public TreeNode<string> nodePathTree;

        private GraphPortView m_connectPortView;

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> entryList = new List<SearchTreeEntry>();
            int layerIndex = 0;
            AddEntry(nodePathTree, entryList, ref layerIndex);
            //entryList.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
            //entryList.Add(new SearchTreeGroupEntry(new GUIContent("Example")) {level = 1});
            //entryList.Add(new SearchTreeGroupEntry(new GUIContent("Example1")) { level = 1 });
            //entryList.Add(new SearchTreeEntry(new GUIContent("float")) {level = 2, userData = "Float"});
            //entryList.Add(new SearchTreeEntry(new GUIContent("binary_op")) {level = 2, userData = "BinaryOp"});
            //entryList.Add(new SearchTreeGroupEntry(new GUIContent("Example")) { level = 1 });
            //entryList.Add(new SearchTreeEntry(new GUIContent("float2")) { level = 2, userData = "Float2" });
            return entryList;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            if (funcOnSelectEntry != null)
            {
                return funcOnSelectEntry(searchTreeEntry, context, m_connectPortView);
            }
            return false;
        }

        public void Open(Vector3 screenPosition)
        {
            SearchWindow.Open(new SearchWindowContext(screenPosition), this);
        }

        public void OpenAndConnectPort(Vector3 screenPosition, GraphPortView portView)
        {
            m_connectPortView = portView;
            Open(screenPosition);
        }

        private void AddEntry(TreeNode<string> treeNode, List<SearchTreeEntry> entryList, ref int level)
        {
            if (treeNode.isLeafNode)
            {
                entryList.Add(new SearchTreeEntry(new GUIContent(treeNode.value)) { level = level, userData = treeNode.value });
            }
            else
            {
                entryList.Add(new SearchTreeGroupEntry(new GUIContent(treeNode.value)) { level = level });
                level++;
                for (int i = 0; i < treeNode.childCount; ++i)
                {
                    AddEntry(treeNode.GetChild(i), entryList, ref level);
                }
                level--;
            }
        }
    }
}