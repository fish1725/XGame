#region

using System.Collections.Generic;
using Assets._Scripts.UI.Window;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameTrigger;

#endregion

namespace Assets._Scripts.XGameEditor {
    public class XGameEditorModel : XGameModel {
        #region Instance Properties

        public List<XGameTriggerModel> triggers {
            get { return Get("triggers") as List<XGameTriggerModel>; }
            set { Set("triggers", value); }
        }

        public List<XGameWindowModel> windows {
            get { return Get("windows") as List<XGameWindowModel>; }
            set { Set("windows", value); }
        }

        #endregion

        #region Instance Methods

        public void AddTrigger(XGameTriggerModel trigger) {
            Add("triggers", trigger);
        }

        public void AddWindow(XGameWindowModel window) {
            Add("windows", window);
        }

        public void RemoveTrigger(XGameTriggerModel trigger) {
            Remove("triggers", trigger);
        }

        public void RemoveWindow(XGameWindowModel window) {
            Remove("windows", window);
        }

        #endregion
    }
}