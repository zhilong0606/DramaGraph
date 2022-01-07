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

        public Action<GraphEdgeView> actionOnEdgeCreated;
        public Action<GraphEdgeView> actionOnEdgeRemoved;

        public EdgeConnectorListener(SearchWindowProvider searchWindowProvider)
        {
            m_searchWindowProvider = searchWindowProvider;
        }

        public void OnDropOutsidePort(Edge edge, Vector2 position)
        {
            GraphEdgeView edgeView = edge as GraphEdgeView;
            if (edge.input == null || edge.output == null)
            {
                GraphPortView portView = null;
                if (edge.input != null)
                {
                    portView = edge.input as GraphPortView;
                }
                if (edge.output != null)
                {
                    portView = edge.output as GraphPortView;
                }
                if (portView != null)
                {
                    m_searchWindowProvider.OpenAndConnectPort(GUIUtility.GUIToScreenPoint(Event.current.mousePosition), portView);
                }
            }
            if (edgeView != null && actionOnEdgeRemoved != null)
            {
                actionOnEdgeRemoved(edgeView);
            }
        }

        public void OnDrop(GraphView graphView, Edge edge)
        {
            GraphEdgeView edgeView = edge as GraphEdgeView;
            if(edgeView != null && actionOnEdgeCreated != null)
            {
                actionOnEdgeCreated(edgeView);
            }
            var leftSlot = edge.output;
            var rightSlot = edge.input;
            graphView.Add(edge);
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