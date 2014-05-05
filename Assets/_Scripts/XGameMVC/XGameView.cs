using UnityEngine;
using System.Collections;

public class XGameView<T> : MonoBehaviour where T : XGameModel {
    private T _model;

    public T Model {
        get { return _model; }
        set { _model = value; }
    }

    void Start() {

    }

    void Update() {

    }

    virtual public void InitEvents() {

    }
}
