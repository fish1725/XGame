using UnityEngine;
using System.Collections;

public class XGame : MonoBehaviour {

    private static Hashtable _instances = new Hashtable();

    public static T CreateView<T>(XGameModel model) where T : XGameView {
        GameObject go = new GameObject();
        go.name = typeof(T).ToString();
        T view = go.AddComponent<T>();
        view.Model = model;
        view.InitEvents();
        return view;
    }

    public static T CreateController<T>() where T : XGameController, new() {
        T controller = new T();
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
