#region

using Assets._Scripts.UI.Window;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameTrigger;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameEditor {
    public class XGameEditorView : XGameView<XGameEditorModel> {
        #region Fields

        private UIAtlas _atlas;
        private UISprite _navMenu;
        private XGameWindowModel _window;

        #endregion

        #region Instance Methods

        public override void Init() {
            InitUIRoot();
            InitNavMenu();
        }

        public override void InitEvents() {
            base.InitEvents();
            Model.On("add:windows", AddWindow);
            Model.On("change:triggers", InitTriggersButton);
        }

        private void AddWindow(XGameEvent e) {
            XGameWindowView view = XGame.CreateView<XGameWindowView, XGameWindowModel>(e.data as XGameWindowModel,
                gameObject);
            view.BringForward();
        }

        private void InitNavMenu() {
            _atlas = XGame.Resolve<UIAtlas>();
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

        private void InitTriggersButton(XGameEvent e) {
            UITable table = _navMenu.GetComponentInChildren<UITable>();
            UISprite triggerButton = NGUITools.AddSprite(table.gameObject, _atlas, "Buttons_Info");
            NGUITools.AddWidgetCollider(triggerButton.gameObject);
            UIButton b = triggerButton.gameObject.AddComponent<UIButton>();
            UIButtonScale bs = triggerButton.gameObject.AddComponent<UIButtonScale>();
            bs.hover = Vector3.one;
            bs.pressed = new Vector3(0.9f, 0.9f, 0.9f);
            b.onClick.Add(new EventDelegate(
                () => {
                    if (_window == null) {
                        _window = XGame.Resolve<XGameEditorController>().CreateWindow(XGame.Resolve<XGameEditorModel>());
                        foreach (XGameTriggerModel trigger in Model.triggers) {
                            XGame.Resolve<XGameWindowController>().AddWindowContent(_window, trigger);
                        }
                    }
                    XGame.Resolve<XGameWindowController>().SetWindowActive(_window, true);
                    
                    //XGame.Resolve<XGameWindowController>().AddWindowContent(window, Model);
                }
                ));
        }

        private void InitUIRoot() {
            UIPanel panel = NGUITools.CreateUI(false, LayerMask.NameToLayer("UI"));
            panel.GetComponent<UIRoot>().scalingStyle = UIRoot.Scaling.FixedSize;
            panel.GetComponent<UIRoot>().manualHeight = 768;
            gameObject.layer = LayerMask.NameToLayer("UI");
            transform.parent = panel.GetComponentInChildren<UICamera>().transform;
        }

        #endregion
    }
}