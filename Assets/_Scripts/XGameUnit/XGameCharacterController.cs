#region

using Assets._Scripts.XGameMVC;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameUnit {
    public class XGameCharacterController : XGameController {
        #region Instance Methods

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

        #endregion
    }
}