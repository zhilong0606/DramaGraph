using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


namespace GraphEditor
{
    [CustomEditor(typeof(GraphNodeDefineContainer))]
    public class GraphNodeDefineContainerInspector : Editor
    {
        private ReorderableList m_reorderableNodeDefineList;
        private ReorderableList m_reorderablePortDefineListLeft;
        private ReorderableList m_reorderablePortDefineListRight;

        private GraphNodeDefineContainer m_nodeDefineContainer;

        private GraphNodeDefine m_selectedNodeDefine;
        private GraphPortDefine m_selectedPortDefine;
        private List<GraphPortDefine> m_inputPortDefineList = new List<GraphPortDefine>();
        private List<GraphPortDefine> m_outputPortDefineList = new List<GraphPortDefine>();

        private static GraphNodeDefineContainer m_cachedNodeDefineContainer;
        private static GraphNodeDefine m_cachedSelectedNodeDefine;
        private static GraphPortDefine m_cachedSelectedPortDefine;

        public override void OnInspectorGUI()
        {
            m_nodeDefineContainer = target as GraphNodeDefineContainer;
            if (m_nodeDefineContainer != m_cachedNodeDefineContainer)
            {
                m_cachedNodeDefineContainer = m_nodeDefineContainer;
                m_cachedSelectedNodeDefine = null;
                m_cachedSelectedPortDefine = null;
            }
            EditorGUI.BeginChangeCheck();
            if (m_reorderableNodeDefineList == null)
            {
                m_reorderableNodeDefineList = new ReorderableList(m_nodeDefineContainer.nodeList, typeof(GraphNodeDefineContainer), true, true, true, true);
                m_reorderableNodeDefineList.drawHeaderCallback = OnDrawHeader_NodeDefine;
                m_reorderableNodeDefineList.drawElementCallback = OnDrawElement_NodeDefine;
                m_reorderableNodeDefineList.onReorderCallback = OnReorder_NodeDefine;
                m_reorderableNodeDefineList.onAddCallback = OnAdd_NodeDefine;
                m_reorderableNodeDefineList.onRemoveCallback = OnRemove_NodeDefine;
                m_reorderableNodeDefineList.onSelectCallback = OnSelect_NodeDefine;
                if (m_cachedSelectedNodeDefine != null)
                {
                    if (m_nodeDefineContainer.nodeList.Contains(m_cachedSelectedNodeDefine))
                    {
                        m_reorderableNodeDefineList.index = m_nodeDefineContainer.nodeList.IndexOf(m_cachedSelectedNodeDefine);
                        m_selectedNodeDefine = m_cachedSelectedNodeDefine;
                    }
                    else
                    {
                        m_cachedSelectedPortDefine = null;
                    }
                }
            }
            m_reorderableNodeDefineList.DoLayoutList();

            if (m_selectedNodeDefine != null)
            {
                SerializedProperty nodeProperty = serializedObject.FindProperty("nodeList").GetArrayElementAtIndex(m_reorderableNodeDefineList.index);
                DrawNodeDefine(nodeProperty, m_selectedNodeDefine);
            }
            else
            {
                SelectPortDefine(null);
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(m_nodeDefineContainer);
            }
        }

        private void OnDrawHeader_NodeDefine(Rect rect)
        {
            EditorGUI.LabelField(rect, "NodeList");
        }

        private void OnDrawElement_NodeDefine(Rect rect, int index, bool isactive, bool isfocused)
        {
            GraphNodeDefine nodeDefine = m_nodeDefineContainer.nodeList[index];
            GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
            style.richText = true;
            EditorGUI.LabelField(rect, string.Format("{0} {1}", nodeDefine.name, nodeDefine.path, style));
        }

        private void OnReorder_NodeDefine(ReorderableList list)
        {
            if (m_nodeDefineContainer != null)
            {
                EditorUtility.SetDirty(m_nodeDefineContainer);
            }
        }

        private void OnAdd_NodeDefine(ReorderableList list)
        {
            GraphNodeDefine nodeDefine = new GraphNodeDefine();
            if (m_selectedNodeDefine != null)
            {
                int selectedIndex = m_nodeDefineContainer.nodeList.IndexOf(m_selectedNodeDefine);
                m_nodeDefineContainer.nodeList.Insert(selectedIndex + 1, nodeDefine);
            }
            else
            {
                m_nodeDefineContainer.nodeList.Add(nodeDefine);
            }
            //list.index = m_nodeDefineContainer.nodeList.IndexOf(nodeDefine);
            //SelectNodeDefine(nodeDefine);
            EditorUtility.SetDirty(m_nodeDefineContainer);
        }

        private void OnRemove_NodeDefine(ReorderableList list)
        {
            if (list.index >= 0 && list.index < m_nodeDefineContainer.nodeList.Count)
            {
                if (m_selectedNodeDefine == m_nodeDefineContainer.nodeList[list.index])
                {
                    SelectNodeDefine(null);
                }
                m_nodeDefineContainer.nodeList.RemoveAt(list.index);
                if (m_nodeDefineContainer.nodeList.Count > 0)
                {
                    list.index = Mathf.Min(list.index, m_nodeDefineContainer.nodeList.Count - 1);
                    SelectNodeDefine(m_nodeDefineContainer.nodeList[list.index]);
                }
                EditorUtility.SetDirty(m_nodeDefineContainer);
            }
        }

        private void OnSelect_NodeDefine(ReorderableList list)
        {
            if (list.index >= 0 && list.index < m_nodeDefineContainer.nodeList.Count)
            {
                if (m_selectedNodeDefine != m_nodeDefineContainer.nodeList[list.index])
                {
                    SelectNodeDefine(m_nodeDefineContainer.nodeList[list.index]);
                    int maxId = 0;
                    for (int i = 0; i < m_selectedNodeDefine.portList.Count; ++i)
                    {
                        maxId = Mathf.Max(maxId, m_selectedNodeDefine.portList[i].id);
                    }
                    m_selectedNodeDefine.idCursor = maxId + 1;
                    m_reorderablePortDefineListLeft = null;
                    m_reorderablePortDefineListRight = null;
                    SelectPortDefine(null);
                }
            }
        }

        private void SelectNodeDefine(GraphNodeDefine nodeDefine)
        {
            m_selectedNodeDefine = nodeDefine;
            m_cachedSelectedNodeDefine = nodeDefine;
        }

        private void DrawNodeDefine(SerializedProperty nodeProperty, GraphNodeDefine nodeDefine)
        {
            GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
            style.fontSize = 18;
            EditorGUILayout.LabelField("NodeDefine", style, GUILayout.ExpandWidth(true), GUILayout.Height(24));

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(30);
                EditorGUILayout.BeginVertical("HelpBox");
                {
                    EditorGUILayout.PropertyField(nodeProperty.FindPropertyRelative("name"));
                    EditorGUILayout.PropertyField(nodeProperty.FindPropertyRelative("path"));
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.BeginVertical();
                        {
                            if (m_reorderablePortDefineListLeft == null)
                            {
                                m_inputPortDefineList.Clear();
                                foreach (var portDefine in nodeDefine.portList)
                                {
                                    if(portDefine.dirType == EGraphPortDirType.Input)
                                    {
                                        m_inputPortDefineList.Add(portDefine);
                                    }
                                }
                                m_inputPortDefineList.Sort((x, y) => x.sortId.CompareTo(y.sortId));
                                m_reorderablePortDefineListLeft = new ReorderableList(m_inputPortDefineList, typeof(GraphPortDefine), true, true, true, true);
                                m_reorderablePortDefineListLeft.drawHeaderCallback = OnDrawHeader_PortDefineInput;
                                m_reorderablePortDefineListLeft.drawElementCallback = OnDrawElement_PortDefineInput;
                                m_reorderablePortDefineListLeft.onAddCallback = OnAdd_PortDefineInput;
                                m_reorderablePortDefineListLeft.onRemoveCallback = OnRemove_PortDefineInput;
                                m_reorderablePortDefineListLeft.onSelectCallback = OnSelect_PortDefineInput;
                                m_reorderablePortDefineListLeft.onChangedCallback = OnChanged_PortDefineInput;
                                if (m_cachedSelectedPortDefine != null)
                                {
                                    if (m_inputPortDefineList.Contains(m_cachedSelectedPortDefine))
                                    {
                                        m_reorderablePortDefineListLeft.index = m_inputPortDefineList.IndexOf(m_cachedSelectedPortDefine);
                                        m_selectedPortDefine = m_cachedSelectedPortDefine;
                                    }
                                }
                            }
                            m_reorderablePortDefineListLeft.DoLayoutList();
                        }
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.BeginVertical();
                        {
                            if (m_reorderablePortDefineListRight == null)
                            {
                                m_outputPortDefineList.Clear();
                                foreach (var portDefine in nodeDefine.portList)
                                {
                                    if (portDefine.dirType == EGraphPortDirType.Output)
                                    {
                                        m_outputPortDefineList.Add(portDefine);
                                    }
                                }
                                m_outputPortDefineList.Sort((x, y) => x.sortId.CompareTo(y.sortId));
                                m_reorderablePortDefineListRight = new ReorderableList(m_outputPortDefineList, typeof(GraphPortDefine), true, true, true, true);
                                m_reorderablePortDefineListRight.drawHeaderCallback = OnDrawHeader_PortDefineOutput;
                                m_reorderablePortDefineListRight.drawElementCallback = OnDrawElement_PortDefineOutput;
                                m_reorderablePortDefineListRight.onAddCallback = OnAdd_PortDefineOutput;
                                m_reorderablePortDefineListRight.onRemoveCallback = OnRemove_PortDefineOutput;
                                m_reorderablePortDefineListRight.onSelectCallback = OnSelect_PortDefineOutput;
                                m_reorderablePortDefineListRight.onChangedCallback = OnChanged_PortDefineOutput;
                                if (m_cachedSelectedPortDefine != null)
                                {
                                    if (m_outputPortDefineList.Contains(m_cachedSelectedPortDefine))
                                    {
                                        m_reorderablePortDefineListRight.index = m_outputPortDefineList.IndexOf(m_cachedSelectedPortDefine);
                                        m_selectedPortDefine = m_cachedSelectedPortDefine;
                                    }
                                }
                            }
                            m_reorderablePortDefineListRight.DoLayoutList();
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndHorizontal();
                    if (m_selectedPortDefine != null)
                    {
                        SerializedProperty portProperty = null;
                        int index = nodeDefine.portList.IndexOf(m_selectedPortDefine);
                        if (index >= 0 && index < nodeProperty.FindPropertyRelative("portList").arraySize)
                        {
                            portProperty = nodeProperty.FindPropertyRelative("portList").GetArrayElementAtIndex(index);

                        }
                        DrawPortDefine(portProperty, m_selectedPortDefine);
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawPortDefine(SerializedProperty portProperty, GraphPortDefine portDefine)
        {
            GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
            style.fontSize = 16;
            EditorGUILayout.LabelField("PortDefine", style, GUILayout.ExpandWidth(true), GUILayout.Height(22));
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(30);
                EditorGUILayout.BeginVertical("HelpBox");
                {
                    EditorGUILayout.LabelField("id", portDefine.id.ToString());
                    EditorGUILayout.PropertyField(portProperty.FindPropertyRelative("name"));
                    EditorGUILayout.PropertyField(portProperty.FindPropertyRelative("valueType"));
                    if (portDefine.isTrigger)
                    {
                        portDefine.defaultValue = string.Empty;
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(portProperty.FindPropertyRelative("defaultValue"));
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void OnDrawHeader_PortDefineInput(Rect rect)
        {
            EditorGUI.LabelField(rect, "Input");
        }

        private void OnDrawElement_PortDefineInput(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index >= 0 && index < m_inputPortDefineList.Count)
            {
                GraphPortDefine portDefine = m_inputPortDefineList[index];
                GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
                style.richText = true;
                EditorGUI.LabelField(rect, portDefine.name, style);
            }
        }

        private void OnAdd_PortDefineInput(ReorderableList list)
        {
            GraphPortDefine portDefine = new GraphPortDefine();
            portDefine.id = m_selectedNodeDefine.idCursor++;
            portDefine.dirType.value = EGraphPortDirType.Input;
            if (m_selectedPortDefine != null)
            {
                int selectedIndex = m_inputPortDefineList.IndexOf(m_selectedPortDefine);
                m_inputPortDefineList.Insert(selectedIndex + 1, portDefine);
                selectedIndex = m_selectedNodeDefine.portList.IndexOf(m_selectedPortDefine);
                m_selectedNodeDefine.portList.Insert(selectedIndex + 1, portDefine);
            }
            else
            {
                m_inputPortDefineList.Add(portDefine);
                m_selectedNodeDefine.portList.Add(portDefine);
            }
            //list.index = m_inputPortDefineList.IndexOf(portDefine);
            //SelectPortDefine(portDefine);
            EditorUtility.SetDirty(m_nodeDefineContainer);
        }

        private void OnRemove_PortDefineInput(ReorderableList list)
        {
            if (list.index >= 0 && list.index < m_inputPortDefineList.Count)
            {
                if (m_selectedPortDefine == m_inputPortDefineList[list.index])
                {
                    m_inputPortDefineList.Remove(m_selectedPortDefine);
                    m_selectedNodeDefine.portList.Remove(m_selectedPortDefine);
                    SelectPortDefine(null);
                    if (m_inputPortDefineList.Count > 0)
                    {
                        list.index = Mathf.Min(list.index, m_inputPortDefineList.Count - 1);
                        SelectPortDefine(m_inputPortDefineList[list.index]);
                    }
                }
                EditorUtility.SetDirty(m_nodeDefineContainer);
            }
        }

        private void OnSelect_PortDefineInput(ReorderableList list)
        {
            if (list.index >= 0 && list.index < m_inputPortDefineList.Count)
            {
                if (m_selectedPortDefine != m_inputPortDefineList[list.index])
                {
                    SelectPortDefine(m_inputPortDefineList[list.index]);
                    m_reorderablePortDefineListRight.index = -1;
                }
            }
        }

        private void OnChanged_PortDefineInput(ReorderableList list)
        {
            for (int i = 0; i < list.count; ++i)
            {
                m_inputPortDefineList[i].sortId = i;
            }
        }

        private void OnDrawHeader_PortDefineOutput(Rect rect)
        {
            EditorGUI.LabelField(rect, "Output");
        }

        private void OnDrawElement_PortDefineOutput(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index >= 0 && index < m_outputPortDefineList.Count)
            {
                GraphPortDefine portDefine = m_outputPortDefineList[index];
                GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
                style.richText = true;
                EditorGUI.LabelField(rect, portDefine.name, style);
            }
        }

        private void OnAdd_PortDefineOutput(ReorderableList list)
        {
            GraphPortDefine portDefine = new GraphPortDefine();
            portDefine.id = m_selectedNodeDefine.idCursor++;
            portDefine.dirType.value = EGraphPortDirType.Output;
            if (m_selectedPortDefine != null)
            {
                int selectedIndex = m_outputPortDefineList.IndexOf(m_selectedPortDefine);
                m_outputPortDefineList.Insert(selectedIndex + 1, portDefine);
                selectedIndex = m_selectedNodeDefine.portList.IndexOf(m_selectedPortDefine);
                m_selectedNodeDefine.portList.Insert(selectedIndex + 1, portDefine);
            }
            else
            {
                m_outputPortDefineList.Add(portDefine);
                m_selectedNodeDefine.portList.Add(portDefine);
            }
            //list.index = m_outputPortDefineList.IndexOf(portDefine);
            //SelectPortDefine(portDefine);
            EditorUtility.SetDirty(m_nodeDefineContainer);
        }

        private void OnRemove_PortDefineOutput(ReorderableList list)
        {
            if (list.index >= 0 && list.index < m_outputPortDefineList.Count)
            {
                if (m_selectedPortDefine == m_outputPortDefineList[list.index])
                {
                    m_outputPortDefineList.Remove(m_selectedPortDefine);
                    m_selectedNodeDefine.portList.Remove(m_selectedPortDefine);
                    SelectPortDefine(null);
                    if (m_outputPortDefineList.Count > 0)
                    {
                        list.index = Mathf.Min(list.index, m_outputPortDefineList.Count - 1);
                        SelectPortDefine(m_outputPortDefineList[list.index]);
                    }
                }
                EditorUtility.SetDirty(m_nodeDefineContainer);
            }
        }

        private void OnSelect_PortDefineOutput(ReorderableList list)
        {
            if (list.index >= 0 && list.index < m_outputPortDefineList.Count)
            {
                if (m_selectedPortDefine != m_outputPortDefineList[list.index])
                {
                    SelectPortDefine(m_outputPortDefineList[list.index]);
                    m_reorderablePortDefineListLeft.index = -1;
                }
            }
        }

        private void OnChanged_PortDefineOutput(ReorderableList list)
        {
            for (int i = 0; i < list.count; ++i)
            {
                m_outputPortDefineList[i].sortId = i;
            }
        }

        private void SelectPortDefine(GraphPortDefine portDefine)
        {
            m_selectedPortDefine = portDefine;
            m_cachedSelectedPortDefine = portDefine;
        }
    }
}
