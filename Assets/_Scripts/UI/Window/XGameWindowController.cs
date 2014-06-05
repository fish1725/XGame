#region

using System;
using Assets._Scripts.UI.Window.Items;
using Assets._Scripts.XGameMVC;
using UnityEngine;

#endregion

namespace Assets._Scripts.UI.Window {
    public class XGameWindowController : XGameController {
        #region Instance Methods

        public void AddWindowContent(XGameWindowModel window, IXGameWindowContentItemModel item) {
            window.AddContent(item);
        }

        public void AddWindowContent(XGameWindowModel window, object item) {
            Type itemModelType = typeof(XGameWindowContentItemModel);
            string className = XGameUtil.XGameUtil.GetTypeName(item.GetType());
            Type itemType = Type.GetType(itemModelType.FullName + className);
            Debug.Log("className: " + itemModelType.FullName + className);
            if (itemType == null) {
                className = XGameUtil.XGameUtil.GetTypeName(item.GetType().BaseType);
                itemType = Type.GetType(itemModelType.FullName + className);
                Debug.Log("className: " + itemModelType.FullName + className);
            }
            if (itemType == null) return;
            XGameWindowContentItemModel itemModel = (XGameWindowContentItemModel)Activator.CreateInstance(itemType);
            itemModel.value = item;
            AddWindowContent(window, itemModel);
        }

        public void ClearWindowContent(XGameWindowModel window) {
            window.RemoveAllContent();
        }

        public XGameWindowModel CreateWindow() {
            XGameWindowModel window = new XGameWindowModel();
            return window;
        }

        public void SetWindowActive(XGameWindowModel window, bool active) {
            window.active = active;
        }

        #endregion
    }
}