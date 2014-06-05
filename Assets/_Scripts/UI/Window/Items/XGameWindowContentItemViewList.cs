#region

using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.XGameEditor;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameUtil;
using UnityEngine;

#endregion

namespace Assets._Scripts.UI.Window.Items {
    public class XGameWindowContentItemViewList : XGameWindowContentItemView {
        #region Instance Properties

        public override object value {
            get {
                List<object> re = new List<object>();
                for (int i = 0; i < _itemInput.transform.childCount; i++) {
                    re.Add(_itemInput.transform.GetChild(i).GetComponent<XGameWindowContentItemView>().value);
                }
                return re;
            }
            set {
                IList vl = (value as IList);
                if (vl == null) return;
                resetInputs(vl);
            }
        }

        #endregion

        private readonly Dictionary<object, XGameWindowModel> _windows = new Dictionary<object, XGameWindowModel>();

        #region Instance Methods

        protected override void InitInput() {
            _itemInput = XGameUIUtil.CreateTable(gameObject);
            _itemInput.transform.localPosition = new Vector3(_leftX, 0f, 0f);
        }

        protected void resetInputs(IList list) {
            XGameObjectUtil.RemoveAllChildren(_itemInput);
            if (list == null) return;
            foreach (object item in list) {
                object o = item;
                IXGameWindowContentItemModel windowContentItemModel = XGameWindowContentItemModel.Create(o);
                if (windowContentItemModel == null) continue;
                GameObject itemInput = XGameUIUtil.CreateTextButton(_itemInput, windowContentItemModel.name);
                XGameWindowContentItemView itemView = itemInput.AddComponent<XGameWindowContentItemView>();
                itemView.value = windowContentItemModel;
                itemInput.transform.localPosition = new Vector3(_leftX, 0f, 0f);
                UIButton itemButton = itemInput.GetComponent<UIButton>();
                itemButton.onClick.Add(new EventDelegate(
                    () => {
                        if (!_windows.ContainsKey(o)) {
                            _windows[o] = XGame.Resolve<XGameEditorController>().CreateWindow(XGame.Resolve<XGameEditorModel>());
                            XGame.Resolve<XGameWindowController>().ClearWindowContent(_windows[o]);
                            foreach (IXGameWindowContentItemModel itemModel in windowContentItemModel.children) {
                                XGame.Resolve<XGameWindowController>().AddWindowContent(_windows[o], itemModel);
                            }
                        }
                        XGame.Resolve<XGameWindowController>().SetWindowActive(_windows[o], true);
                    }
                    ));
            }
            _itemInput.GetComponent<UITable>().repositionNow = true;
        }

        #endregion
    }
}