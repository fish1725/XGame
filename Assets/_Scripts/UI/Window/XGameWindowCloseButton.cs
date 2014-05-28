#region

using UnityEngine;

#endregion

namespace Assets._Scripts.UI.Window {
    public class XGameWindowCloseButton : MonoBehaviour {
        #region Fields

        public XGameWindowView target;

        #endregion

        #region Instance Methods

        protected void OnClick() {
            if (!target) {
                Transform temp = transform;
                while (temp) {
                    XGameWindowView gw = temp.GetComponent<XGameWindowView>();
                    if (gw) {
                        target = gw;
                        break;
                    }
                    temp = temp.parent;
                }
            }
            if (target)
                target.Close();
        }

        #endregion
    }
}