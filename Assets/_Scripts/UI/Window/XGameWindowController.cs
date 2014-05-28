#region

using Assets._Scripts.UI.Window.Items;
using Assets._Scripts.XGameMVC;

#endregion

namespace Assets._Scripts.UI.Window {
    public class XGameWindowController : XGameController {
        #region Instance Methods

        public void AddWindowContent(XGameWindowModel window, IXGameWindowContentItemModel item) {
            window.AddContent(item);
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