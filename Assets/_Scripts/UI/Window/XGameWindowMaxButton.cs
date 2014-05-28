using UnityEngine;

namespace Assets._Scripts.UI.Window {
    public class XGameWindowMaxButton : MonoBehaviour {
        public XGameWindowView target;

        protected void OnClick() {
            if (!target) {
                Transform temp = transform;
                while (temp) {
                    var gw = temp.GetComponent<XGameWindowView>();
                    if (gw) {
                        target = gw;
                        break;
                    }
                    temp = temp.parent;
                }
            }
            if (!target) return;
            target.Maximum(!target.isMax);
            target.Minimum(false);
        }
    }
}