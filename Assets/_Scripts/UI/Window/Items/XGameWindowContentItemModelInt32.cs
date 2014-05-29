namespace Assets._Scripts.UI.Window.Items {
    public class XGameWindowContentItemModelInt32 : XGameWindowContentItemModel {
        #region Instance Methods

        public override void Save(object data) {
            int result;
            base.Save(!int.TryParse(data.ToString(), out result) ? 0 : result);
        }

        #endregion
    }
}