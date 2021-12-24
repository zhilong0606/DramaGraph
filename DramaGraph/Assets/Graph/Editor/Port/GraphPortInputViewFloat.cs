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
        //protected Func<float> m_Get;
        //protected Action<float> m_Set;
        protected float value;
        protected int m_undoGroup = -1;


        protected override void OnInitView()
        {
            base.OnInitView();
            styleSheets.Add(Resources.Load<StyleSheet>("Styles/GraphPortInputViewFloat"));
            AddField(0f, 0, "A");
            AddField(0f, 1, "A");
        }

        public void InitView(string[] labels, Func<float> get, Action<float> set)
        {
            //m_Get = get;
            //m_Set = set;
            //float initialValue = get();
            //for (var i = 0; i < labels.Length; i++)
            //    AddField(initialValue, i, labels[i]);
        }

        private void AddField(float initialValue, int index, string subLabel)
        {
            var dummy = new VisualElement { name = "dummy" };
            var label = new Label(subLabel);
            dummy.Add(label);
            Add(dummy);
            var field = new FloatField { userData = index, value = initialValue };
            var dragger = new FieldMouseDragger<float>(field);
            dragger.SetDragZone(label);
            field.Q("unity-text-input").RegisterCallback<KeyDownEvent>(evt =>
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
            field.RegisterValueChangedCallback(evt =>
            {   
                if (m_undoGroup == -1)
                {
                    //m_Node.owner.owner.RegisterCompleteObjectUndo("Change " + m_Node.name);
                }
                //var value = m_Get();
                if (value != (float)evt.newValue)
                {
                    value = (float)evt.newValue;
                    //m_Set(value);
                    //m_Node.Dirty(ModificationScope.Node);
                }
            });
            field.Q("unity-text-input").RegisterCallback<FocusOutEvent>(evt =>
            {
                m_undoGroup = -1;
            });
            Add(field);
        }
    }
}
