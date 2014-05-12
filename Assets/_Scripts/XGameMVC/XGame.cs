using UnityEngine;
using System.Collections;

public class XGame : MonoBehaviour {

    private static Hashtable _instances = new Hashtable();

    public static T CreateView<T, TT>(TT model, GameObject parent = null)
        where T : XGameView<TT>
        where TT : IXGameModel {
        GameObject go = new GameObject();
        go.name = typeof(T).ToString();
        if (parent != null) {
            go.transform.parent = parent.transform;
            go.layer = parent.layer;
        }
        go.transform.localScale = Vector3.one;
        T view = go.AddComponent<T>();
        view.Model = model;
        view.InitEvents();
        view.Init();
        return view;
    }

    public static T CreateController<T>() where T : XGameController, new() {
        T controller = new T();
        RegisterInstance<T>(controller);
        return controller;
    }

    public static T RegisterInstance<T>(T instance) {
        string name = typeof(T).ToString();
        _instances[name] = instance;
        return instance;
    }

    public static T Resolve<T>() {
        return (T)(_instances[typeof(T).ToString()]);
    }

    virtual public void Setup() {

    }

    void Start() {
        Setup();
    }
}
