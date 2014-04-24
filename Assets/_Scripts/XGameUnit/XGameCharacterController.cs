using UnityEngine;
using System.Collections;

public class XGameCharacterController : XGameController {

    public XGameCharacterModel CreateCharacter() {
        XGameCharacterModel c = new XGameCharacterModel();
        return c;
    }

    public void SetCharacterPosition(XGameCharacterModel cha, Vector3 pos) {
        cha.position = pos;
    }

}
