using Assets._Scripts.XGameMVC;

namespace Assets._Scripts.UI.Window.Items {
    public class XGameWindowContentItemModel : XGameModel, IXGameWindowContentItemModel {
        public override string spriteName {
            get {
                return "";
            }
            set {
                base.spriteName = value;
            }
        }

        public override System.Collections.Generic.List<IXGameWindowContentItemModel> windowContentItems {
            get {
                return null;
            }
            set {

            }
        }

        public override object value { get; set; }

        public override string key { get; set; }
    }
}
