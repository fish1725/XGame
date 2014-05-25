using UnityEngine;
using System.Collections;

public class XGameView<T> : MonoBehaviour where T : IXGameModel {
    private T _model;

    public T Model {
        get { return _model; }
        set { _model = value; }
    }

    public virtual void InitEvents() {

    }

    public virtual void Init() {

    }

    public virtual void dispose() {

    }
}
