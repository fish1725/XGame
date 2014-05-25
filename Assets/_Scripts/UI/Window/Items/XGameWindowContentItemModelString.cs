using UnityEngine;
using System.Collections;

public class XGameWindowContentItemModelString : XGameWindowContentItemModel {

    public override void Save(object value) {
        Debug.Log("Window Content Item String Save: " + value);
        this.value = value.ToString();
    }

}
