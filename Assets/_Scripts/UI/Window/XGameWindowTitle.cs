﻿using UnityEngine;
using System.Collections;

public class XGameWindowTitle : MonoBehaviour {

    public XGameWindowView target;

    void OnDragStart() {
        if (!target) {
            target = XGameObjectUtil.GetComponentInAncestors<XGameWindowView>(gameObject);
        }
        if (target)
            target.Maximum(false);
    }

    void OnDoubleClick() {
        if (!target) {
            target = XGameObjectUtil.GetComponentInAncestors<XGameWindowView>(gameObject);
        }
        if (target) {
            target.Maximum(!target.IsMax);
            target.Minimum(false);
            target.GetComponent<SpringPosition>().enabled = false;
        }
    }
}
