using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class GraphPortInputViewFloat : GraphPortInputView
    {
        private Label m_nameLabel;
        private FloatField m_valueField;
        protected int m_undoGroup = -1;

        protected GraphPortDataFloat specificData
        {
            get { return m_data as GraphPortDataFloat; }
        }

        protected override void OnInitView()
        {
            base.OnInitView();
            styleSheets.Add(Resources.Load<StyleSheet>("Styles/GraphPortInputViewFloat"));

            VisualElement dummyElement = new VisualElement { name = "dummy" };
            m_nameLabel = new Label("T");
            dummyElement.Add(m_nameLabel);
            Add(dummyElement);
            m_valueField = new FloatField();// { userData = index, value = initialValue };
            FieldMouseDragger<float> dragger = new FieldMouseDragger<float>(m_valueField);
            dragger.SetDragZone(m_nameLabel);
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
                float value = specificData.value;
                if (Mathf.Abs(value - evt.newValue) > float.Epsilon)
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
