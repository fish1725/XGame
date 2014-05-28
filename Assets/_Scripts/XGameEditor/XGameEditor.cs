#region

using Assets._Scripts.UI.Window;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameTrigger;
using Assets._Scripts.XGameUnit;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameEditor {
    public class XGameEditor : XGame {
        #region Fields

        public Font font;

        #endregion

        #region Instance Methods

        public override void Setup() {
            base.Setup();

            UIAtlas atlas = Resources.Load<GameObject>("Mobile Cartoon GUI Rock Demo").GetComponent<UIAtlas>();
            RegisterInstance(atlas);

            if (font) {
                RegisterInstance(font);
            }

            InitControllers();

            XGameEditorModel editorModel = Resolve<XGameEditorController>().CreateEditor();
            CreateView<XGameEditorView, XGameEditorModel>(editorModel, gameObject);

            Resolve<XGameEditorController>().InitTriggers(editorModel);
            RegisterInstance(editorModel);
        }

        private void InitControllers() {
            CreateController<XGameEditorController>();
            CreateController<XGameWindowController>();
            CreateController<XGameTriggerController>();
            CreateController<XGameCharacterController>();
        }

        #endregion
    }
}