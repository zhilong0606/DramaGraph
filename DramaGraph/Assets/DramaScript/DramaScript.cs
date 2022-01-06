using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DramaScript
{
    public class DramaScript
    {
        private List<DramaScriptNode> m_nodeList = new List<DramaScriptNode>();
        private DramaScriptNode m_entryNode;
        private List<DramaScriptNode> m_entryNodeList = new List<DramaScriptNode>();

        public void Load(DramaScriptGraphData graphData)
        {
            foreach(var nodeContainer in graphData.NodeContainerList)
            {
                DramaScriptNode node = DramaScriptFactory.CreateNode(nodeContainer.TypeName);
                object data = DramaScriptFactory.ParseNodeData(nodeContainer.TypeName, nodeContainer.Buffers);
                node.Init(nodeContainer.Id, data);
                m_nodeList.Add(node);
                if (node is DramaScriptNodeTimeEntry || node is DramaScriptNodeEntry)
                {
                    m_entryNodeList.Add(node);
                }
            }
            foreach (var edgeData in graphData.EdgeList)
            {
                DramaScriptNode inputNode = GetNode(edgeData.InputNodeId);
                DramaScriptNode outputNode = GetNode(edgeData.OutputNodeId);
                if (inputNode != null && outputNode != null)
                {
                    switch (edgeData.TypeName)
                    {
                        case "Float":
                            inputNode.RegisterInputFunc(edgeData.InputPortId, outputNode.GetOutputFunc<float>(edgeData.OutputPortId));
                            break;
                        case "String":
                            inputNode.RegisterInputFunc(edgeData.InputPortId, outputNode.GetOutputFunc<string>(edgeData.OutputPortId));
                            break;
                        case "Trigger":
                            outputNode.RegisterOutputTrigger(edgeData.OutputPortId, inputNode.GetInputTriggerAction(edgeData.InputPortId));
                            break;
                    }
                }
            }
        }



        public void Tick(float deltaTime)
        {

        }

        private DramaScriptNode GetNode(int nodeId)
        {
            int count = m_nodeList.Count;
            for (int i = 0; i < count; ++i)
            {
                if (m_nodeList[i].id == nodeId)
                {
                    return m_nodeList[i];
                }
            }
            return null;
        }
    }
}
