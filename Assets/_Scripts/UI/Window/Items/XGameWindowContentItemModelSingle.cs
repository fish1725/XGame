namespace Assets._Scripts.UI.Window.Items {
    public class XGameWindowContentItemModelSingle : XGameWindowContentItemModel {

        public override void Save(object data) {
            float result;
            base.Save(!float.TryParse(data.ToString(), out result) ? 0f : result);
        }
    }
}
