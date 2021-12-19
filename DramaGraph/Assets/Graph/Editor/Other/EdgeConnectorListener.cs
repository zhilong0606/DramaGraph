using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GraphEditor
{
    public class EdgeConnectorListener : IEdgeConnectorListener
    {
        private SearchWindowProvider m_searchWindowProvider;

        public EdgeConnectorListener(SearchWindowProvider searchWindowProvider)
        {
            m_searchWindowProvider = searchWindowProvider;
        }

        public void OnDropOutsidePort(Edge edge, Vector2 position)
        {
            Port draggedPort = null;
            if (edge.output != null)
            {
                draggedPort = edge.output.edgeConnector.edgeDragHelper.draggedPort;
                if (draggedPort == null && edge.input != null)
                {
                    draggedPort = edge.input.edgeConnector.edgeDragHelper.draggedPort;
                }
            }
            m_searchWindowProvider.OpenAndConnectPort(GUIUtility.GUIToScreenPoint(Event.current.mousePosition), draggedPort);
        }

        public void OnDrop(GraphView graphView, Edge edge)
        {
            //var leftSlot = edge.output.no;
            //var rightSlot = edge.input.GetSlot();
            //if (leftSlot != null && rightSlot != null)
            //{
            //    m_Graph.owner.RegisterCompleteObjectUndo("Connect Edge");
            //    m_Graph.Connect(leftSlot.slotReference, rightSlot.slotReference);
            //}
        }
    }
}