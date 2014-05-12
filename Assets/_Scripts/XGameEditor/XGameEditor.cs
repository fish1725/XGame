using UnityEngine;
using System.Collections;

public class XGameEditor : XGame {

    public Font font;

    public override void Setup() {
        base.Setup();

        UIAtlas atlas = Resources.Load<GameObject>("Mobile Cartoon GUI Rock Demo").GetComponent<UIAtlas>();
        RegisterInstance<UIAtlas>(atlas);

        if (font) {
            RegisterInstance<Font>(font);
        }

        InitControllers();

        XGameEditorModel editorModel = Resolve<XGameEditorController>().CreateEditor();
        CreateView<XGameEditorView, XGameEditorModel>(editorModel, gameObject);

        Resolve<XGameEditorController>().InitTriggers(editorModel);
        RegisterInstance<XGameEditorModel>(editorModel);
    }

    void InitControllers() {
        CreateController<XGameEditorController>();
        CreateController<XGameWindowController>();
        CreateController<XGameTriggerController>();
        CreateController<XGameCharacterController>();
    }

}
