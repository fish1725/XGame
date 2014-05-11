using UnityEngine;
using System.Collections;

public class XGameWindowController : XGameController {

    public XGameWindowModel CreateWindow() {
        XGameWindowModel window = new XGameWindowModel();
        return window;
    }

    public void AddWindowContent(XGameWindowModel window, XGameWindowContentItemModel item) {
        window.AddContent(item);
    }
}
