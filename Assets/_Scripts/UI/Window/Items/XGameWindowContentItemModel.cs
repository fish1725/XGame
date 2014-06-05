#region

using System;
using System.Collections.Generic;
using Assets._Scripts.XGameMVC;
using UnityEngine;

#endregion

namespace Assets._Scripts.UI.Window.Items {
    public class XGameWindowContentItemModel : XGameModel, IXGameWindowContentItemModel {
        #region IXGameWindowContentItemModel Members

        public List<IXGameWindowContentItemModel> children { get; set; }
        public string key { get; set; }
        public XGameModel parent { get; set; }
        public string spriteName { get; set; }
        public Type type { get; set; }
        public virtual object value { get; set; }

        #endregion

        public XGameWindowContentItemModel() {
            children = new List<IXGameWindowContentItemModel>();
        }

        public static XGameWindowContentItemModel Create(object item) {
            Type itemModelType = typeof(XGameWindowContentItemModel);
            string className = XGameUtil.XGameUtil.GetTypeName(item.GetType());
            Type itemType = Type.GetType(itemModelType.FullName + className);
            Debug.Log("className: " + itemModelType.FullName + className);
            if (itemType == null) {
                if (item is XGameModel) {
                    className = "XGameModel";
                }
                itemType = Type.GetType(itemModelType.FullName + className);
                Debug.Log("className: " + itemModelType.FullName + className);
            }
            if (itemType == null) return null;
            XGameWindowContentItemModel itemModel = (XGameWindowContentItemModel)Activator.CreateInstance(itemType);
            return itemModel;
        }
    }
}