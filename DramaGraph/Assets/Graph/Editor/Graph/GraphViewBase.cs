﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class GraphView<TData> : GraphView where TData : GraphData
    {
        private TData m_data;
        private EdgeConnectorListener m_edgeConnectorListener;
        private SearchWindowProvider m_searchWindowProvider;

        public Action actionOnSaveBtnClicked;
        public Action actionOnExportBtnClicked;
        public Action<List<ISelectable>> actionOnDeleteSelection;
        public Func<string, Vector2, GraphPortView, bool> funcOnCreateNode;

        public EdgeConnectorListener edgeConnectorListener
        {
            get { return m_edgeConnectorListener; }
        }

        public SearchWindowProvider searchWindowProvider
        {
            get { return m_searchWindowProvider; }
        }

        public void Init()
        {
            this.StretchToParentSize();
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            m_searchWindowProvider = ScriptableObject.CreateInstance<SearchWindowProvider>();
            m_searchWindowProvider.funcOnSelectEntry = OnMenuWindowProviderSelectEntry;

            nodeCreationRequest += context => { m_searchWindowProvider.Open(context.screenMousePosition); };
            serializeGraphElements += OnSerializeGraphElements;
            unserializeAndPaste += OnUnserializeAndPaste;

            m_edgeConnectorListener = new EdgeConnectorListener(m_searchWindowProvider);

            IMGUIContainer toolbar = new IMGUIContainer(() =>
            {
                GUILayout.BeginHorizontal(EditorStyles.toolbar);
                if (GUILayout.Button("Save", EditorStyles.toolbarButton))
                {
                    if (actionOnSaveBtnClicked != null)
                    {
                        actionOnSaveBtnClicked();
                    }
                }
                if (GUILayout.Button("Export", EditorStyles.toolbarButton))
                {
                    if (actionOnExportBtnClicked != null)
                    {
                        actionOnExportBtnClicked();
                    }
                }
                GUILayout.EndHorizontal();
            });
            Add(toolbar);
        }

        private string OnSerializeGraphElements(IEnumerable<GraphElement> elements)
        {
            throw new NotImplementedException();
        }

        private void OnUnserializeAndPaste(string operationname, string data)
        {
            throw new NotImplementedException();
        }

        public override EventPropagation DeleteSelection()
        {
            if (actionOnDeleteSelection != null)
            {
                actionOnDeleteSelection(selection);
            }
            return base.DeleteSelection();
        }

        private bool OnMenuWindowProviderSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context, GraphPortView connectPortView)
        {
            string nodeDefineName = searchTreeEntry.userData as string;
            Vector2 screenMousePosition = context.screenMousePosition;
            if (funcOnCreateNode != null)
            {
                return funcOnCreateNode(nodeDefineName, screenMousePosition, connectPortView);
            }
            return false;
        }

        public void SetData(TData data)
        {
            m_data = data;
        }

        public void AddNode(GraphNodeView nodeView)
        {
            AddElement(nodeView);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> portList = new List<Port>();
            foreach (Port port in ports.ToList())
            {
                if (startPort.node != port.node
                    && startPort.direction != port.direction
                    && startPort.portType == port.portType)
                {
                    portList.Add(port);
                }
            }
            return portList;
        }

        protected override bool canDeleteSelection
        {
            get { return true; }
        }

        protected override bool canCopySelection
        {
            get { return true; }
        }

        protected override bool canPaste
        {
            get { return true; }
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
        }
    }
}