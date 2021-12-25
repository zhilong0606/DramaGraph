using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphEditor
{
    public class GraphPortInputViewContainer : GraphElement, IDisposable
    {
        private GraphPortInputView m_inputView;
        private VisualElement m_container;
        private VisualElement m_container2;
        private EdgeControl m_edgeControl;

        public GraphPortInputViewContainer()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Styles/GraphPortInputViewContainer"));
            ClearClassList();
            pickingMode = PickingMode.Ignore;
            m_edgeControl = new EdgeControl
            {
                @from = new Vector2(232f - 21f, 11.5f),
                to = new Vector2(232f, 11.5f),
                edgeWidth = 2,
                pickingMode = PickingMode.Ignore
            };
            Add(m_edgeControl);
            m_container = new VisualElement { name = "container" };
            {
                m_container2 = new VisualElement {name = "container2"};
                m_container.Add(m_container2);
                VisualElement slotElement = new VisualElement { name = "slot" };
                {
                    slotElement.Add(new VisualElement { name = "dot" });
                }
                m_container.Add(slotElement);
            }
            Add(m_container);

            RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        public void SetInputView(GraphPortInputView inputView)
        {
            m_inputView = inputView;
            if (inputView != null)
            {
                m_container2.Add(inputView);
            }
            m_container.visible = m_edgeControl.visible = m_inputView != null;
        }

        //void Recreate()
        //{
        //    RemoveFromClassList("type" + m_SlotType);
        //    AddToClassList("type" + m_SlotType);
        //    if (m_Control != null)
        //    {
        //        var disposable = m_Control as IDisposable;
        //        if (disposable != null)
        //            disposable.Dispose();
        //        m_Container.Remove(m_Control);
        //    }
        //    m_Control = slot.InstantiateControl();
        //    if (m_Control != null)
        //        m_Container.Insert(0, m_Control);

        //    m_Container.visible = m_EdgeControl.visible = m_Control != null;
        //}

        public void Dispose()
        {
            IDisposable disposable = m_inputView as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
        {
            m_edgeControl.UpdateLayout();
        }
    }
}
