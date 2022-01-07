using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GraphEditor.Drama
{
    public static class GraphExtension
    {
        public static GraphPortData GetPortId(this GraphData graphData, int nodeId, int portId)
        {
            if (graphData != null)
            {
                GraphNodeData nodeData = graphData.GetNodeData(nodeId);
                if (nodeData != null)
                {
                    return nodeData.GetPortData(portId);
                }
            }
            return null;
        }
        public static GraphNodeData GetNodeData(this GraphData graphData, int nodeId)
        {
            if (graphData != null)
            {
                int count = graphData.nodeDataList.Count;
                for (int i = 0; i < count; ++i)
                {
                    if (graphData.nodeDataList[i].id == nodeId)
                    {
                        return graphData.nodeDataList[i];
                    }
                }
            }
            return null;
        }

        public static GraphNodeDefine GetNode(this GraphNodeDefineContainer nodeDefineContainer, string nodeName)
        {
            if (nodeDefineContainer != null)
            {
                int count = nodeDefineContainer.nodeList.Count;
                for (int i = 0; i < count; ++i)
                {
                    if (nodeDefineContainer.nodeList[i].name == nodeName)
                    {
                        return nodeDefineContainer.nodeList[i];
                    }
                }
            }
            return null;
        }

        public static GraphPortDefine GetPort(this GraphNodeDefine nodeDefine, int portId)
        {
            if (nodeDefine != null)
            {
                int count = nodeDefine.portList.Count;
                for (int i = 0; i < count; ++i)
                {
                    if (nodeDefine.portList[i].id == portId)
                    {
                        return nodeDefine.portList[i];
                    }
                }
            }
            return null;
        }

        public static GraphPortDefine GetPort(this GraphNodeDefine nodeDefine, string portName)
        {
            if (nodeDefine != null)
            {
                int count = nodeDefine.portList.Count;
                for (int i = 0; i < count; ++i)
                {
                    if (nodeDefine.portList[i].name == portName)
                    {
                        return nodeDefine.portList[i];
                    }
                }
            }
            return null;
        }
    }
}