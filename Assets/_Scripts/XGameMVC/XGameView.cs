using UnityEngine;
using System.Collections;

public class XGameView<T> : MonoBehaviour where T : IXGameModel {
    private T _model;

    public T Model {
        get { return _model; }
        set { _model = value; }
    }

    virtual public void InitEvents() {

    }

    virtual public void Init() {

    }
}
