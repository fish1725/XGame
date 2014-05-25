using UnityEngine;
using System.Collections;

public class XGameWindowContentItemModel : XGameModel, IXGameWindowContentItemModel {

    public override string spriteName {
        get {
            return "";
        }
        set {
            base.spriteName = value;
        }
    }

    public override System.Collections.Generic.List<IXGameWindowContentItemModel> windowContentItems {
        get {
            return null;
        }
        set {

        }
    }

    public override object value {
        get {
            return Get("value");
        }
        set {
            Set("value", value);
        }
    }

}
