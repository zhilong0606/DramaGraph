using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class TreeNode<T>
    {
        public TreeNode<T> parent;
        public List<TreeNode<T>> childList = new List<TreeNode<T>>();

        public T value;

        public int childCount
        {
            get { return childList.Count; }
        }

        public bool isLeafNode
        {
            get { return childList.Count == 0; }
        }

        public TreeNode()
        {
        }

        public TreeNode(T value)
        {
            this.value = value;
        }

        public void AddNode(TreeNode<T> node)
        {
            childList.Add(node);
        }

        public TreeNode<T> GetChild(int index)
        {
            if(index >= 0 && index < childList.Count)
            {
                return childList[index];
            }
            return null;
        }

        public TreeNode<T> FindChildNodeByValue(T value)
        {
            int count = childList.Count;
            for (int i = 0; i < count; ++i)
            {
                if (childList[i].value.Equals(value))
                {
                    return childList[i];
                }
            }
            return null;
        }
    }
}
