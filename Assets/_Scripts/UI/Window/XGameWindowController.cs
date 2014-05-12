using UnityEngine;
using System.Collections;

public class XGameWindowController : XGameController {

    public XGameWindowModel CreateWindow() {
        XGameWindowModel window = new XGameWindowModel();
        return window;
    }

    public void AddWindowContent(XGameWindowModel window, IXGameWindowContentItemModel item) {
        window.AddContent(item);
    }

    public void SetWindowActive(XGameWindowModel window, bool active) {
        window.active = active;
    }
}
