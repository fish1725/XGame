using UnityEngine;
using System.Collections;

public class XGameWindowContentItemModelSingle : XGameWindowContentItemModel {

    public override void Save(object value) {
        float result = 0f;
        if (!float.TryParse(value.ToString(), out result)) {
            this.value = 0f;
        } else {
            this.value = result;
        }
    }
}
