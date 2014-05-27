using UnityEngine;
using System.Collections;

public class XGameWindowContentItemModelString : XGameWindowContentItemModel {

    public override void Save(object value) {
        this.value = value.ToString();
    }

}
