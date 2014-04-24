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
}
