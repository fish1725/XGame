using UnityEngine;
using System.Collections;

public class XGameWindowView : XGameView<XGameWindowModel> {

    private bool _isMax = false;
    private bool _isMin = false;
    private int _lastWidth = 600;
    private int _lastHeight = 480;
    private float _lastX = 0;
    private float _lastY = 0;
    private GameObject _contentWrapper;
    private GameObject _title;

    public bool IsMax {
        get { return _isMax; }
        set { _isMax = value; }
    }
    public bool IsMin {
        get { return _isMin; }
        set { _isMin = value; }
    }

    void Awake() {
        InitPanel();
        InitContentWrapper();
        InitTitle();
        InitButtons();
        InitResizer();
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
        UISprite sprite = NGUITools.AddSprite(_title, XGameEditor.Resolve<UIAtlas>(), "Buttons_Cancel");
        sprite.SetAnchor(_title, -90, 10, -10, -10);
        sprite.leftAnchor.relative = 1;
        sprite.gameObject.AddComponent<UIButton>();
        UIButtonScale bs = sprite.gameObject.AddComponent<UIButtonScale>();
        bs.hover = Vector3.one;
        bs.pressed = new Vector3(0.9f, 0.9f, 0.9f);
        sprite.gameObject.AddComponent<XGameWindowCloseButton>();
        NGUITools.AddWidgetCollider(sprite.gameObject);
    }

    void InitMaxButton() {
        UISprite sprite = NGUITools.AddSprite(_title, XGameEditor.Resolve<UIAtlas>(), "Buttons_LevelSelect");
        sprite.SetAnchor(_title, -170, 10, -90, -10);
        sprite.leftAnchor.relative = 1;
        sprite.gameObject.AddComponent<UIButton>();
        UIButtonScale bs = sprite.gameObject.AddComponent<UIButtonScale>();
        bs.hover = Vector3.one;
        bs.pressed = new Vector3(0.9f, 0.9f, 0.9f);
        sprite.gameObject.AddComponent<XGameWindowMaxButton>();
        NGUITools.AddWidgetCollider(sprite.gameObject);
    }

    void InitMinButton() {
        UISprite sprite = NGUITools.AddSprite(_title, XGameEditor.Resolve<UIAtlas>(), "Buttons_Decrease");
        sprite.SetAnchor(_title, -250, 10, -170, -10);
        sprite.leftAnchor.relative = 1;
        sprite.gameObject.AddComponent<UIButton>();
        UIButtonScale bs = sprite.gameObject.AddComponent<UIButtonScale>();
        bs.hover = Vector3.one;
        bs.pressed = new Vector3(0.9f, 0.9f, 0.9f);
        sprite.gameObject.AddComponent<XGameWindowMinButton>();
        NGUITools.AddWidgetCollider(sprite.gameObject);
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
        resizer.topAnchor.relative = 1;
        NGUITools.AddWidgetCollider(resizer.gameObject);
        UIDragResize dr = resizer.gameObject.AddComponent<UIDragResize>();
        dr.target = GetComponent<UIWidget>();
        dr.pivot = UIWidget.Pivot.BottomRight;
        dr.minHeight = 480;
        dr.minWidth = 600;
    }

    public void Close() {
        GameObject.Destroy(gameObject);
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
            } else {
                _lastWidth = widget.width;
                _lastHeight = widget.height;
                _lastX = transform.localPosition.x;
                _lastY = transform.localPosition.y;
                widget.SetAnchor(widget.root.gameObject, 0, 0, 0, 0);
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
}
