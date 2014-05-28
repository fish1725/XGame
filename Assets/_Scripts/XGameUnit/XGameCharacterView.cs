#region

using UnityEngine;

#endregion

namespace Assets._Scripts.XGameUnit {
    public class XGameCharacterView : XGameUnitView<XGameCharacterModel> {
        #region Fields

        private GameObject _shadow;

        #endregion

        #region Instance Methods

        public override void Init() {
            InitModel();
            InitShadow();
            InitLayer();
        }

        public override void InitEvents() {
        }

        private void InitLayer() {
            foreach (Transform child in GetComponentsInChildren<Transform>()) {
                child.gameObject.layer = LayerMask.NameToLayer("Unit");
            }
        }

        private void InitModel() {
            GameObject character = Instantiate(Resources.Load<GameObject>("King")) as GameObject;
            if (character != null) character.transform.parent = transform;
            transform.position = Model.position;
            gameObject.tag = "Player";
        }

        private void InitShadow() {
            _shadow = Instantiate(Resources.Load<GameObject>("Blob Shadow Projector")) as GameObject;
            if (_shadow != null) {
                _shadow.transform.parent = transform;
                _shadow.transform.localPosition = new Vector3(0, 1.5f, 0);
                _shadow.GetComponent<Projector>().ignoreLayers |= 1 << LayerMask.NameToLayer("Unit");
            }
        }

        protected void Start() {
            XGameWorldEventDispatcher.instance.broadcast(XGameEventType.Character_Created);
        }

        protected void Update() {
        }

        #endregion
    }
}