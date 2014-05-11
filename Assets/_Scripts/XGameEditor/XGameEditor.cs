using UnityEngine;
using System.Collections;

public class XGameEditor : XGame {
    private XGameEditorController _editorController = null;

    public Font font;

    public override void Setup() {
        base.Setup();

        UIAtlas atlas = Resources.Load<GameObject>("Mobile Cartoon GUI Rock Demo").GetComponent<UIAtlas>();
        RegisterInstance<UIAtlas>(atlas);

        if (font) {
            RegisterInstance<Font>(font);
        }
        
        _editorController = CreateController<XGameEditorController>();

        XGameEditorModel editorModel = _editorController.CreateEditor();
        CreateView<XGameEditorView, XGameEditorModel>(editorModel, gameObject);

        _editorController.InitTriggers(editorModel);
        RegisterInstance<XGameEditorModel>(editorModel);
    }

}
