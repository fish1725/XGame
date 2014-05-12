using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class XGameModel : IXGameModel {
    private Hashtable _properties = new Hashtable();
    private XGameEventDispatcher _events = new XGameEventDispatcher();

    public XGameModel() {
        id = Guid.NewGuid();
    }

    protected void Set(string property, object value) {
        _properties[property] = value;
        _events.broadcast("change:" + property, new XGameEvent() { data = value });
    }

    protected object Get(string property) {
        return _properties[property];
    }

    protected void Add<T>(string property, T value) {
        if (_properties[property] == null) {
            _properties[property] = new List<T>();
        }
        (_properties[property] as List<T>).Add(value);
        _events.broadcast("add:" + property, new XGameEvent() { data = value });
    }

    protected void Remove<T>(string property, T value) {
        if (_properties[property] != null) {
            if ((_properties[property] as List<T>).Remove(value)) {
                _events.broadcast("remove:" + property, new XGameEvent() { data = value });
            }
        }
    }

    public void On(string eventName, XGameEventHandler func) {
        _events.addEventListener(eventName, func);
    }

    public void Off(string eventName, XGameEventHandler func) {
        _events.removeEventListener(eventName, func);
    }

    public Guid id {
        get { return (Guid)(Get("id")); }
        set { Set("id", value); }
    }

    public string name {
        get { return Get("name") as string; }
        set { Set("name", value); }
    }
}
