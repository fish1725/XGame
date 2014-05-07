using UnityEngine;
using System.Collections;

public class XGameEditorView : XGameView<XGameEditorModel> {

    private UIAtlas _atlas = null;
    private UISprite _navMenu = null;
    private int _topDepth = 100;

    public override void InitEvents() {
        base.InitEvents();
        Model.On("add:windows", AddWindow);
        Model.On("change:triggers", InitTriggersButton);
    }

    void Awake() {
        InitUIRoot();
        InitNavMenu();
    }

    void InitUIRoot() {
        UIPanel panel = NGUITools.CreateUI(false, LayerMask.NameToLayer("UI"));
        panel.GetComponent<UIRoot>().scalingStyle = UIRoot.Scaling.FixedSize;
        panel.GetComponent<UIRoot>().manualHeight = 768;
        gameObject.layer = LayerMask.NameToLayer("UI");
        transform.parent = panel.GetComponentInChildren<UICamera>().transform;
    }

    void InitNavMenu() {
        _atlas = XGameEditor.Resolve<UIAtlas>();
        _navMenu = NGUITools.AddSprite(gameObject, _atlas, "PopupBG");
        _navMenu.SetAnchor(_navMenu.root.gameObject, 10, 10, 150, -10);
        _navMenu.rightAnchor.relative = 0;
        UIWidget widget = NGUITools.AddWidget<UIWidget>(_navMenu.gameObject);
        widget.SetAnchor(_navMenu.gameObject, 20, 20, -20, -20);
        widget.pivot = UIWidget.Pivot.TopLeft;
        UITable table = widget.gameObject.AddComponent<UITable>();
        table.columns = 1;
        table.padding.y = 5;
        table.keepWithinPanel = true;
    }

    void InitTriggersButton(XGameEvent e) {
        UITable table = _navMenu.GetComponentInChildren<UITable>();
        UISprite triggerButton = NGUITools.AddSprite(table.gameObject, _atlas, "Buttons_Info");
        NGUITools.AddWidgetCollider(triggerButton.gameObject);
        UIButton b = triggerButton.gameObject.AddComponent<UIButton>();
        UIButtonScale bs = triggerButton.gameObject.AddComponent<UIButtonScale>();
        bs.hover = Vector3.one;
        bs.pressed = new Vector3(0.9f, 0.9f, 0.9f);
        b.onClick.Add(new EventDelegate(
            () => { XGame.Resolve<XGameEditorController>().CreateWindow(XGame.Resolve<XGameEditorModel>()); }
            ));
    }

    void AddWindow(XGameEvent e) {
        XGameWindowView view = XGame.CreateView<XGameWindowView, XGameWindowModel>(e.data as XGameWindowModel);
        view.transform.parent = transform;
        view.transform.localScale = Vector3.one;
        view.GetComponent<UIPanel>().depth = _topDepth++;
    }


}
