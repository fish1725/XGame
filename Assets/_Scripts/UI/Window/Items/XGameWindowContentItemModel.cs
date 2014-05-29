#region

using Assets._Scripts.XGameMVC;

#endregion

namespace Assets._Scripts.UI.Window.Items {
    public class XGameWindowContentItemModel : XGameModel {
        #region Instance Properties

        public override string key { get; set; }

        public override string spriteName {
            get { return ""; }
            set { base.spriteName = value; }
        }

        public override object value { get; set; }

        public override System.Collections.Generic.List<IXGameWindowContentItemModel> windowContentItems {
            get { return null; }
            set { }
        }

        #endregion
    }
}