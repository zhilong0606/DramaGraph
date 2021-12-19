using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DramaScriptMainBoardGraphView : GraphView
{
    private DramaScriptGraphData m_graphData;
    private DramaScriptMainBoardEdgeConnectorListener m_edgeConnectorListener;
    private DramaScriptMainBoardSearchWindowProvider m_searchWindowProvider;
    private EditorWindow m_editorWindow;

    public Action<DramaScriptGraphData> actionOnSaveGraphData;

    public DramaScriptMainBoardGraphView(EditorWindow editorWindow, DramaScriptGraphData graphData)
    {
        m_editorWindow = editorWindow;
        m_graphData = graphData;
        this.StretchToParentSize();
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        m_searchWindowProvider = ScriptableObject.CreateInstance<DramaScriptMainBoardSearchWindowProvider>();
        m_searchWindowProvider.funcOnSelectEntry = OnMenuWindowProviderSelectEntry;

        nodeCreationRequest += context => { m_searchWindowProvider.Open(context.screenMousePosition); };

        m_edgeConnectorListener = new DramaScriptMainBoardEdgeConnectorListener(m_searchWindowProvider);

        foreach (var nodeData in m_graphData.nodeDataList)
        {
            Type type = null;
            if (nodeData.GetType() == typeof(DramaGraphNodeDataFloat))
            {
                type = typeof(DramaScriptFloatNodeView);
            }
            CreateNode(type, nodeData.pos);
        }


        var toolbar = new IMGUIContainer(() =>
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            if (GUILayout.Button("Save Asset", EditorStyles.toolbarButton))
            {
                if (actionOnSaveGraphData != null)
                {
                    actionOnSaveGraphData(m_graphData);
                }
            }
            GUILayout.EndHorizontal();
        });
        Add(toolbar);
    }

    private bool OnMenuWindowProviderSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context, Port connectPort)
    {
        Type type = searchTreeEntry.userData as Type;
        var windowRoot = m_editorWindow.rootVisualElement;
        var windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent, context.screenMousePosition - m_editorWindow.position.position);

        DramaGraphNodeData node = null;
        if (type == typeof(DramaScriptFloatNodeView))
        {
            node = new DramaGraphNodeDataFloat();
        }
        node.pos = contentViewContainer.WorldToLocal(windowMousePosition);
        m_graphData.nodeDataList.Add(node);

        CreateNode(type, node.pos);
        return true;
    }

    private void CreateNode(Type type, Vector2 pos)
    {
        Node node = Activator.CreateInstance(type) as Node;
        node.SetPosition(new Rect(pos, Vector2.zero));
        for (int i = 0; i < node.inputContainer.childCount; ++i)
        {
            Port inputPort = node.inputContainer[i] as Port;
            if (inputPort != null)
            {
                EdgeConnector<Edge> connector = new EdgeConnector<Edge>(m_edgeConnectorListener);
                FieldInfo fi = typeof(Port).GetField("m_EdgeConnector", BindingFlags.NonPublic | BindingFlags.Instance);
                fi.SetValue(inputPort, connector);
                inputPort.AddManipulator(connector);
            }
        }
        for (int i = 0; i < node.outputContainer.childCount; ++i)
        {
            Port outputPort = node.outputContainer[i] as Port;
            if (outputPort != null)
            {
                EdgeConnector<Edge> connector = new EdgeConnector<Edge>(m_edgeConnectorListener);
                FieldInfo fi = typeof(Port).GetField("m_EdgeConnector", BindingFlags.NonPublic | BindingFlags.Instance);
                fi.SetValue(outputPort, connector);
                outputPort.AddManipulator(connector);
            }
        }
        this.AddElement(node);
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
        get { return !selection.OfType<DramaScriptFloatNodeView>().Any(); }
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        base.BuildContextualMenu(evt);
    }
}
