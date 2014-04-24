using UnityEngine;
using System.Collections;

public class XGameUnitModel : XGameModel {
    public Vector3 position {
        get {
            return (Vector3)Get("position");
        }
        set {
            Set("position", value);
        }
    }

    public XGameUnitType type {
        get {
            return (XGameUnitType)Get("type");
        }
        set {
            Set("type", value);
        }
    }
}
