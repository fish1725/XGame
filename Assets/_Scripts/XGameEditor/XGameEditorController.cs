using System.Collections.Generic;

public class XGameEditorController : XGameController {

    public XGameEditorModel CreateEditor() {
        XGameEditorModel editor = new XGameEditorModel();
        return editor;
    }

    public void InitTriggers(XGameEditorModel editor) {
        editor.triggers = new List<XGameTrigger>();
    }

    public XGameWindowModel CreateWindow(XGameEditorModel editor) {
        XGameWindowModel window = new XGameWindowModel();
        editor.AddWindow(window);
        return window;
    }

}
