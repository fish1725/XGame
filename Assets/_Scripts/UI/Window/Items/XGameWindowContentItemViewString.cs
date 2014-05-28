#region

using Assets._Scripts.XGameUtil;
using UnityEngine;

#endregion

namespace Assets._Scripts.UI.Window.Items {
    public class XGameWindowContentItemViewString : XGameWindowContentItemView {
        #region Instance Properties

        public override object value {
            get { return _itemInput.GetComponent<UIInput>().value; }
            set { _itemInput.GetComponent<UIInput>().value = value.ToString(); }
        }

        #endregion

        #region Instance Methods

        protected override void InitInput() {
            _itemInput = XGameUIUtil.CreateInput(gameObject, Model.value.ToString());
            _itemInput.transform.localPosition = new Vector3(_leftX, 0f, 0f);
        }

        #endregion
    }
}