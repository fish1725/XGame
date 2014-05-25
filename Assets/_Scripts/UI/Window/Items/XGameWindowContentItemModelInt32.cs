using UnityEngine;
using System.Collections;

public class XGameWindowContentItemModelInt32 : XGameWindowContentItemModel {

    public override void Save(object value) {
        int result = 0;
        if (!int.TryParse(value.ToString(), out result)) {
            this.value = 0;
        } else {
            this.value = result;
        }
    }
}
