using UnityEngine;
using System.Collections;

public class XGameCharacterView : XGameUnitView<XGameCharacterModel> {
    private GameObject _shadow;

    void Start() {
        XGameWorldEventDispatcher.instance.broadcast(XGameEventType.Character_Created);
    }

    void Update() {

    }

    public override void Init() {
        InitModel();
        InitShadow();
        InitLayer();
    }

    public override void InitEvents() {
    }

    void InitModel() {
        GameObject character = GameObject.Instantiate(Resources.Load<GameObject>("King")) as GameObject;
        character.transform.parent = transform;
        transform.position = Model.position;
        gameObject.tag = "Player";
    }

    void InitShadow() {
        _shadow = GameObject.Instantiate(Resources.Load<GameObject>("Blob Shadow Projector")) as GameObject;
        _shadow.transform.parent = transform;
        _shadow.transform.localPosition = new Vector3(0, 1.5f, 0);
        _shadow.GetComponent<Projector>().ignoreLayers |= 1 << LayerMask.NameToLayer("Unit");
    }

    void InitLayer() {
        foreach (Transform child in GetComponentsInChildren<Transform>()) {
            child.gameObject.layer = LayerMask.NameToLayer("Unit");
        }
    }
}
