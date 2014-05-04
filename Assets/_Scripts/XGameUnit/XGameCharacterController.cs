using UnityEngine;

public class XGameCharacterController : XGameController {

    public XGameCharacterModel CreateCharacter() {
        XGameCharacterModel c = new XGameCharacterModel();
        return c;
    }

    public void SetCharacterPosition(XGameCharacterModel cha, Vector3 pos) {
        cha.position = pos;
    }

    public void SetTest(int a) {
        Debug.Log("SetTest: " + a);
    }
}
