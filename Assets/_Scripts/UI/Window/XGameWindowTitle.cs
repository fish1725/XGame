using UnityEngine;
using System.Collections;

public class XGameWindowTitle : MonoBehaviour {

    public XGameWindowView target;

    void OnDragStart() {
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
            target.Maximum(false);
    }

    void OnDoubleClick() {
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
        if (target) {
            target.Maximum(!target.IsMax);
            target.Minimum(false);
            target.GetComponent<SpringPosition>().enabled = false;
        }
    }
}
