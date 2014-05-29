#region

using System.Collections.Generic;
using Assets._Scripts.XGameEditor;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameTrigger.XGameEvent;
using Assets._Scripts.XGameUtil;
using UnityEngine;

#endregion

namespace Assets._Scripts.UI.Window.Items {
    public class XGameWindowContentItemView : XGameView<IXGameWindowContentItemModel> {
        #region Fields

        protected GameObject _itemInput;
        protected UILabel _itemName;
        protected UISprite _itemSprite;
        protected float _leftX = 0f;
        private List<IXGameWindowContentItemModel> _items;
        private XGameWindowModel _window;

        #endregion

        #region Instance Properties

        public virtual object value {
            get { return Model; }
            set { Model = value as IXGameWindowContentItemModel; }
        }

        public List<IXGameWindowContentItemModel> items {
            get { return _items ?? (_items = Model.windowContentItems); }
        }

        #endregion

        #region Instance Methods

        public override void Init() {
            InitSprite();
            InitName();
            InitInput();
        }

        public override void InitEvents() {
            base.InitEvents();
            Model.On("change:spriteName", OnChangeSpriteName);
            Model.On("change:name", OnChangeName);
            if (Model.model != null)
                Model.model.On("change:" + Model.key, OnChangeKeyValue);
        }

        public void Save() {
            Model.Save(value);
        }

        protected virtual void InitInput() {
            if (_itemInput != null) return;
            _itemInput = XGameUIUtil.CreateTextButton(gameObject, Model.name);
            _itemInput.transform.localPosition = new Vector3(_leftX, 0f, 0f);
            UIButton itemButton = _itemInput.GetComponent<UIButton>();
            itemButton.onClick.Add(new EventDelegate(
                () => {
                    if (_window == null) {
                        _window = XGame.Resolve<XGameEditorController>().CreateWindow(XGame.Resolve<XGameEditorModel>());
                        XGame.Resolve<XGameWindowController>().ClearWindowContent(_window);
                        Debug.Log("model: " + Model);
                        foreach (IXGameWindowContentItemModel item in items) {
                            XGame.Resolve<XGameWindowController>().AddWindowContent(_window, item);
                        }
                    }
                    XGame.Resolve<XGameWindowController>().SetWindowActive(_window, true);
                }
                ));
        }

        private void InitName() {
            if (_itemName != null) return;
            GameObject nameLabel = XGameUIUtil.CreateLabel(gameObject, Model.key);
            nameLabel.transform.localPosition = new Vector3(_leftX, 0f, 0f);
            _itemName = nameLabel.GetComponent<UILabel>();
            _leftX += _itemName.width + 10;
        }

        private void InitSprite() {
            if (string.IsNullOrEmpty(Model.spriteName) || _itemSprite != null) return;
            GameObject imageButton = XGameUIUtil.CreateImageButton(gameObject, Model.spriteName);
            _itemSprite = imageButton.GetComponent<UISprite>();

            _leftX += _itemSprite.width + 10;
        }

        private void OnChangeKeyValue(XGameEvent e) {
            value = e.data;
        }

        private void OnChangeName(XGameEvent e) {
            Debug.Log(Model.name + " : " + _itemInput);
            _itemInput.GetComponentInChildren<UILabel>().text = e.data as string;
        }

        private void OnChangeSpriteName(XGameEvent e) {
            _itemSprite.spriteName = e.data as string;
        }

        #endregion
    }
}