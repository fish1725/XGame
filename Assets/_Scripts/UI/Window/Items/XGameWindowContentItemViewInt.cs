namespace Assets._Scripts.UI.Window.Items {
    public class XGameWindowContentItemViewInt : XGameWindowContentItemViewString {
        public override object value {
            get {
                int re;
                int.TryParse(_itemInput.GetComponent<UIInput>().value, out re);
                return re;
            }
            set { _itemInput.GetComponent<UIInput>().value = value.ToString(); }
        }
    }
}