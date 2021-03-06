﻿using UnityEngine;

namespace Assets._Scripts.UI.Window {
    public class XGameWindowMinButton : MonoBehaviour {

        public XGameWindowView target;

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
            if (!target) return;
            target.Maximum(false);
            target.Minimum(!target.isMin);
        }
    }
}
