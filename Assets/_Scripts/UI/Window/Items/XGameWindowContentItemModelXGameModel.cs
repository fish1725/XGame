using Assets._Scripts.XGameMVC;

namespace Assets._Scripts.UI.Window.Items {
    public class XGameWindowContentItemModelXGameModel : XGameWindowContentItemModel {
        public override object value {
            get {
                return base.value;
            }
            set {
                base.value = value;
                if (value is XGameModel) {
                    children.Clear();
                    foreach (string itemKey in (value as XGameModel).properties.Keys) {
                        object item = (value as XGameModel).properties[itemKey];
                        XGameWindowContentItemModel itemModel = Create(item);
                        if (itemModel == null) continue;
                        itemModel.key = itemKey;
                        itemModel.value = item;
                        itemModel.parent = (value as XGameModel);
                        children.Add(itemModel);
                    }
                }
            }
        }
    }
}