using UnityEngine;
using System.Collections;

public class XGameWindowMinButton : MonoBehaviour {

    public XGameWindowView target;

    void OnClick() {
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
            target.Maximum(false);
            target.Minimum(!target.IsMin);
        }            
    }
}
