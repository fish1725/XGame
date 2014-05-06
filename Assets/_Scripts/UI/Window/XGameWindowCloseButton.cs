using UnityEngine;
using System.Collections;

public class XGameWindowCloseButton : MonoBehaviour {

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
        if (target)
            target.Close();
    }
}
