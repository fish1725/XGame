using UnityEngine;
using System.Collections;

public class XGameWindowContentItemModel : XGameModel, IXGameWindowContentItemModel {

    private string _key;
    private object _value;


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
            return _value;
        }
        set {
            _value = value;
        }
    }

    public override string key {
        get {
            return _key;
        }
        set {
            _key = value;
        }
    }

}
