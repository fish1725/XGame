#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets._Scripts.UI.Window.Items;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameTrigger.XGameEvent;
using Assets._Scripts.XGameUtil;
using UnityEngine;

#endregion

namespace Assets._Scripts.UI.Window {
    public class XGameWindowView : XGameView<XGameWindowModel> {
        #region Readonly & Static Fields

        private readonly List<XGameWindowContentItemView> _items = new List<XGameWindowContentItemView>();

        #endregion

        #region Fields

        private GameObject _contentWrapper;
        private int _lastHeight = 480;
        private int _lastWidth = 600;
        private float _lastX;
        private float _lastY;
        private GameObject _title;

        #endregion

        #region Instance Properties

        public bool isMax { get; set; }

        public bool isMin { get; set; }

        #endregion

        #region Instance Methods

        public override void Init() {
            InitPanel();
            InitContentWrapper();
            InitTitle();
            InitButtons();
            InitResizer();
            InitListContent();
        }

        public override void InitEvents() {
            base.InitEvents();
            Model.On("add:content", OnAddWindowContent);
            Model.On("remove:content", OnRemoveWindowContent);
            Model.On("change:active", OnChangeActive);
        }

        public void BringForward() {
            NGUITools.BringForward(gameObject);
        }

        public void Close() {
            Save();
            XGame.Resolve<XGameWindowController>().SetWindowActive(Model, false);
        }

        public void Maximum(bool max, bool rePosition = true) {
            Save();
            if (isMax == max) return;
            isMax = max;
            var widget = GetComponent<UIWidget>();
            if (!isMax) {
                widget.SetAnchor(null, 0, 0, 0, 0);
                widget.width = _lastWidth;
                widget.height = _lastHeight;
                if (rePosition) {
                    Vector3 position = transform.localPosition;
                    position.x = _lastX;
                    position.y = _lastY;
                    transform.localPosition = position;
                }
                GetComponentInChildren<UIDragResize>().collider.enabled = true;
            } else {
                _lastWidth = widget.width;
                _lastHeight = widget.height;
                _lastX = transform.localPosition.x;
                _lastY = transform.localPosition.y;
                widget.SetAnchor(widget.root.gameObject, 0, 0, 0, 0);
                GetComponentInChildren<UIDragResize>().collider.enabled = false;
            }
        }

        public void Minimum(bool min) {
            if (isMin == min) return;
            isMin = min;
            if (_contentWrapper == null)
                _contentWrapper = transform.Find("ContentWrapper").gameObject;
            _contentWrapper.GetComponent<UIWidget>().alpha = isMin ? 0 : 1;
        }

        public void Save() {
            foreach (XGameWindowContentItemView view in _items) {
                view.Save();
            }
        }

        private void InitButtons() {
            InitCloseButton();
            InitMaxButton();
            InitMinButton();
        }

        private void InitCloseButton() {
            GameObject imageButton = XGameUIUtil.CreateImageButton(_title, "Buttons_Cancel");
            var sprite = imageButton.GetComponent<UISprite>();
            sprite.SetAnchor(_title, -90, 10, -10, -10);
            sprite.leftAnchor.relative = 1;
            imageButton.AddComponent<XGameWindowCloseButton>();
        }

        private void InitContentWrapper() {
            UISprite sprite = NGUITools.AddSprite(gameObject, XGame.Resolve<UIAtlas>(), "WindowBG");
            sprite.SetAnchor(gameObject);
            _contentWrapper = sprite.gameObject;
            _contentWrapper.name = "ContentWrapper";
        }

        private void InitListContent() {
            GameObject sv = XGameUIUtil.CreateScrollViewContent(_contentWrapper);
            sv.GetComponentInChildren<UIPanel>().depth = gameObject.GetComponent<UIPanel>().depth + 1;
        }

        private void InitMaxButton() {
            GameObject imageButton = XGameUIUtil.CreateImageButton(_title, "Buttons_LevelSelect");
            var sprite = imageButton.GetComponent<UISprite>();
            sprite.SetAnchor(_title, -170, 10, -90, -10);
            sprite.leftAnchor.relative = 1;
            sprite.gameObject.AddComponent<XGameWindowMaxButton>();
        }

        private void InitMinButton() {
            GameObject imageButton = XGameUIUtil.CreateImageButton(_title, "Buttons_Decrease");
            var sprite = imageButton.GetComponent<UISprite>();
            sprite.SetAnchor(_title, -250, 10, -170, -10);
            sprite.leftAnchor.relative = 1;
            sprite.gameObject.AddComponent<XGameWindowMinButton>();
        }

        private void InitPanel() {
            var widget = gameObject.AddComponent<UIWidget>();
            widget.width = _lastWidth;
            widget.height = _lastHeight;
            gameObject.AddComponent<UIPanel>();
            gameObject.layer = LayerMask.NameToLayer("UI");
        }

        private void InitResizer() {
            var resizer = NGUITools.AddWidget<UIWidget>(_contentWrapper);
            resizer.SetAnchor(gameObject, -80, 0, 0, 80);
            resizer.leftAnchor.relative = 1;
            resizer.topAnchor.relative = 0;
            NGUITools.AddWidgetCollider(resizer.gameObject);
            var dr = resizer.gameObject.AddComponent<UIDragResize>();
            dr.target = GetComponent<UIWidget>();
            dr.pivot = UIWidget.Pivot.BottomRight;
            dr.minHeight = 480;
            dr.minWidth = 600;
        }

        private void InitTitle() {
            UISprite sprite = NGUITools.AddSprite(gameObject, XGame.Resolve<UIAtlas>(), "TitleBG");
            sprite.SetAnchor(gameObject, 0, -100, 0, 0);
            sprite.bottomAnchor.relative = 1;
            var udo = sprite.gameObject.AddComponent<UIDragObject>();
            udo.target = transform;
            udo.restrictWithinPanel = true;
            udo.contentRect = sprite;
            NGUITools.AddWidgetCollider(sprite.gameObject);
            sprite.gameObject.AddComponent<XGameWindowTitle>();
            _title = sprite.gameObject;
        }

        private void OnAddWindowContent(XGameEvent e) {
            var table = GetComponentInChildren<UITable>();
            if (!table) return;
            var item = e.data as IXGameWindowContentItemModel;
            Type gameType = typeof(XGame);
            if (item != null) {
                Type viewBaseType = typeof(XGameWindowContentItemView);
                Type viewType = Type.GetType(viewBaseType.Namespace + ".XGameWindowContentItemView" + item.value.GetType().Name) ??
                                viewBaseType;
                MethodInfo methodDefine = gameType.GetMethod("CreateView", BindingFlags.Public | BindingFlags.Static);
                Type[] genericTypes = { viewType, typeof(IXGameWindowContentItemModel) };
                MethodInfo constructed = methodDefine.MakeGenericMethod(genericTypes);
                object[] args = { item, table.gameObject };
                object view = constructed.Invoke(null, args);

                _items.Add(view as XGameWindowContentItemView);
            }
            table.repositionNow = true;
            table.StartCoroutine(XGameObjectUtil.WaitAndDo(1,
                () => GetComponentInChildren<UIScrollView>().ResetPosition()));
        }

        private void OnChangeActive(XGameEvent e) {
            var data = (bool)e.data;
            gameObject.SetActive(data);
            if (data) {
                NGUITools.BringForward(gameObject);
            }
        }

        private void OnRemoveWindowContent(XGameEvent e) {
            var table = GetComponentInChildren<UITable>();
            if (!table) return;
            var item = e.data as IXGameWindowContentItemModel;
            XGameWindowContentItemView viewToRemove = null;
            foreach (XGameWindowContentItemView view in _items.Where(view => view.Model == item)) {
                viewToRemove = view;
            }
            if (viewToRemove != null)
                XGame.RemoveView<XGameWindowContentItemView, IXGameWindowContentItemModel>(viewToRemove);
            table.repositionNow = true;
            table.StartCoroutine(XGameObjectUtil.WaitAndDo(1,
                () => GetComponentInChildren<UIScrollView>().ResetPosition()));
        }

        #endregion
    }
}