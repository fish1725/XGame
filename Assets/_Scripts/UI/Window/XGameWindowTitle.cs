using Assets._Scripts.XGameUtil;
using UnityEngine;

namespace Assets._Scripts.UI.Window {
    public class XGameWindowTitle : MonoBehaviour {
        public XGameWindowView target;

        protected void OnDragStart() {
            if (!target) {
                target = XGameObjectUtil.GetComponentInAncestors<XGameWindowView>(gameObject);
            }
            if (target) {
                target.Maximum(false);
            }
        }

        protected void OnDoubleClick() {
            if (!target) {
                target = XGameObjectUtil.GetComponentInAncestors<XGameWindowView>(gameObject);
            }
            if (!target) return;
            target.Maximum(!target.isMax);
            target.Minimum(false);
            var sp = target.GetComponent<SpringPosition>();
            if (sp)
                sp.enabled = false;
        }

        protected void OnPress() {
            if (!target) {
                target = XGameObjectUtil.GetComponentInAncestors<XGameWindowView>(gameObject);
            }
            if (target) {
                target.BringForward();
            }
        }
    }
}