using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Label = System.Reflection.Emit.Label;

namespace GraphEditor
{
    [GraphPortInputView(EGraphPortValueType.String)]
    public class GraphPortInputViewString : GraphPortInputView
    {
        private TextField m_valueField;
        protected int m_undoGroup = -1;

        protected GraphPortDataString specificData
        {
            get { return m_data as GraphPortDataString; }
        }

        protected override void OnInitView()
        {
            base.OnInitView();
            styleSheets.Add(Resources.Load<StyleSheet>("Styles/GraphPortInputViewString"));

            VisualElement dummyElement = new VisualElement { name = "dummy" };
            Add(dummyElement);
            m_valueField = new TextField();
            m_valueField.Q("unity-text-input").RegisterCallback<KeyDownEvent>(evt =>
            {
                if (m_undoGroup == -1)
                {
                    m_undoGroup = Undo.GetCurrentGroup();
                    //m_Node.owner.owner.RegisterCompleteObjectUndo("Change " + m_Node.name);
                }
                if (evt.keyCode == KeyCode.Escape && m_undoGroup > -1)
                {
                    Undo.RevertAllDownToGroup(m_undoGroup);
                    m_undoGroup = -1;
                    evt.StopPropagation();
                }
                m_undoGroup++;
                MarkDirtyRepaint();
            });
            m_valueField.RegisterValueChangedCallback(evt =>
            {
                if (m_undoGroup == -1)
                {
                    //m_Node.owner.owner.RegisterCompleteObjectUndo("Change " + m_Node.name);
                }
                var value = specificData.value;
                if (value != evt.newValue)
                {
                    specificData.value = evt.newValue;
                    //m_Set(value);
                    //m_Node.Dirty(ModificationScope.Node);
                }
            });
            m_valueField.Q("unity-text-input").RegisterCallback<FocusOutEvent>(evt =>
            {
                m_undoGroup = -1;
            });
            Add(m_valueField);
        }

        protected override void OnRefreshData()
        {
            base.OnRefreshData();
            m_valueField.value = specificData.value;
        }
    }
}
