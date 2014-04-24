using UnityEngine;
using System.Collections;

public class XGameView : MonoBehaviour{
    private XGameModel _model;

    virtual public XGameModel Model {
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
