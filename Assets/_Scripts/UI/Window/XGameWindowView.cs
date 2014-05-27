using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class XGameWindowView : XGameView<XGameWindowModel> {

    private bool _isMax = false;
    private bool _isMin = false;
    private int _lastWidth = 600;
    private int _lastHeight = 480;
    private float _lastX = 0;
    private float _lastY = 0;
    private GameObject _contentWrapper;
    private GameObject _title;

    private List<XGameWindowContentItemView> _items = new List<XGameWindowContentItemView>();

    public bool IsMax {
        get { return _isMax; }
        set { _isMax = value; }
    }
    public bool IsMin {
        get { return _isMin; }
        set { _isMin = value; }
    }

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

    void OnAddWindowContent(XGameEvent e) {
        UITable table = GetComponentInChildren<UITable>();
        if (table) {
            IXGameWindowContentItemModel item = e.data as IXGameWindowContentItemModel;
            Type gameType = typeof(XGame);
            Type viewType = Type.GetType("XGameWindowContentItemView" + item.value.GetType().Name);
            if (viewType == null) {
                viewType = Type.GetType("XGameWindowContentItemView");
            }
            MethodInfo methodDefine = gameType.GetMethod("CreateView", BindingFlags.Public | BindingFlags.Static);
            Type[] genericTypes = { viewType, typeof(IXGameWindowContentItemModel) };
            MethodInfo constructed = methodDefine.MakeGenericMethod(genericTypes);
            object[] args = { item, table.gameObject };
            object view = constructed.Invoke(null, args);

            _items.Add(view as XGameWindowContentItemView);
            table.repositionNow = true;
            table.StartCoroutine(XGameObjectUtil.WaitAndDo(1, () => { GetComponentInChildren<UIScrollView>().ResetPosition(); }));
        }
    }

    void OnRemoveWindowContent(XGameEvent e) {
        UITable table = GetComponentInChildren<UITable>();
        if (table) {
            IXGameWindowContentItemModel item = e.data as IXGameWindowContentItemModel;
            XGameWindowContentItemView viewToRemove = null;
            foreach (XGameWindowContentItemView view in _items) {
                if (view.Model == item)
                    viewToRemove = view;
            }
            if (viewToRemove != null)
                XGame.RemoveView<XGameWindowContentItemView, IXGameWindowContentItemModel>(viewToRemove);
            table.repositionNow = true;
            table.StartCoroutine(XGameObjectUtil.WaitAndDo(1, () => { GetComponentInChildren<UIScrollView>().ResetPosition(); }));
        }
    }

    void OnChangeActive(XGameEvent e) {
        bool active = (bool)e.data;
        gameObject.SetActive(active);
        if (active) {
            NGUITools.BringForward(gameObject);
        }
    }

    void InitPanel() {
        UIWidget widget = gameObject.AddComponent<UIWidget>();
        widget.width = _lastWidth;
        widget.height = _lastHeight;
        gameObject.AddComponent<UIPanel>();
        gameObject.layer = LayerMask.NameToLayer("UI");
    }

    void InitTitle() {
        UISprite sprite = NGUITools.AddSprite(gameObject, XGameEditor.Resolve<UIAtlas>(), "TitleBG");
        sprite.SetAnchor(gameObject, 0, -100, 0, 0);
        sprite.bottomAnchor.relative = 1;
        UIDragObject udo = sprite.gameObject.AddComponent<UIDragObject>();
        udo.target = transform;
        udo.restrictWithinPanel = true;
        udo.contentRect = sprite;
        NGUITools.AddWidgetCollider(sprite.gameObject);
        sprite.gameObject.AddComponent<XGameWindowTitle>();
        _title = sprite.gameObject;
    }

    void InitButtons() {
        InitCloseButton();
        InitMaxButton();
        InitMinButton();
    }

    void InitCloseButton() {
        GameObject imageButton = XGameUIUtil.CreateImageButton(_title, "Buttons_Cancel");
        UISprite sprite = imageButton.GetComponent<UISprite>();
        sprite.SetAnchor(_title, -90, 10, -10, -10);
        sprite.leftAnchor.relative = 1;
        imageButton.AddComponent<XGameWindowCloseButton>();
    }

    void InitMaxButton() {
        GameObject imageButton = XGameUIUtil.CreateImageButton(_title, "Buttons_LevelSelect");
        UISprite sprite = imageButton.GetComponent<UISprite>();
        sprite.SetAnchor(_title, -170, 10, -90, -10);
        sprite.leftAnchor.relative = 1;
        sprite.gameObject.AddComponent<XGameWindowMaxButton>();
    }

    void InitMinButton() {
        GameObject imageButton = XGameUIUtil.CreateImageButton(_title, "Buttons_Decrease");
        UISprite sprite = imageButton.GetComponent<UISprite>();
        sprite.SetAnchor(_title, -250, 10, -170, -10);
        sprite.leftAnchor.relative = 1;
        sprite.gameObject.AddComponent<XGameWindowMinButton>();
    }

    void InitContentWrapper() {
        UISprite sprite = NGUITools.AddSprite(gameObject, XGameEditor.Resolve<UIAtlas>(), "WindowBG");
        sprite.SetAnchor(gameObject);
        _contentWrapper = sprite.gameObject;
        _contentWrapper.name = "ContentWrapper";
    }

    void InitResizer() {
        UIWidget resizer = NGUITools.AddWidget<UIWidget>(_contentWrapper);
        resizer.SetAnchor(gameObject, -80, 0, 0, 80);
        resizer.leftAnchor.relative = 1;
        resizer.topAnchor.relative = 0;
        NGUITools.AddWidgetCollider(resizer.gameObject);
        UIDragResize dr = resizer.gameObject.AddComponent<UIDragResize>();
        dr.target = GetComponent<UIWidget>();
        dr.pivot = UIWidget.Pivot.BottomRight;
        dr.minHeight = 480;
        dr.minWidth = 600;
    }

    void InitListContent() {
        GameObject sv = XGameUIUtil.CreateScrollViewContent(_contentWrapper);
        sv.GetComponentInChildren<UIPanel>().depth = gameObject.GetComponent<UIPanel>().depth + 1;
    }

    public void Close() {
        Save();
        XGame.Resolve<XGameWindowController>().SetWindowActive(Model, false);
    }

    public void Maximum(bool max, bool rePosition = true) {
        if (_isMax != max) {
            _isMax = max;
            UIWidget widget = GetComponent<UIWidget>();
            if (!_isMax) {
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
    }

    public void Minimum(bool min) {
        if (_isMin != min) {
            _isMin = min;
            if (_contentWrapper == null)
                _contentWrapper = transform.Find("ContentWrapper").gameObject;
            _contentWrapper.GetComponent<UIWidget>().alpha = _isMin ? 0 : 1;
        }
    }

    public void BringForward() {
        NGUITools.BringForward(gameObject);
    }

    public void Save() {
        foreach (XGameWindowContentItemView view in _items) {
            view.Save();
        }
    }
}
