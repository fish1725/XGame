using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class XGameModel : IXGameModel, IXGameWindowContentItemModel {
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

    public virtual string name {
        get { return Get("name") as string; }
        set { Set("name", value); }
    }

    public virtual string spriteName {
        get {
            return "Buttons_RightArrow";
        }
        set {
            name = value;
        }
    }

    [System.Xml.Serialization.XmlIgnore]
    public virtual List<IXGameWindowContentItemModel> windowContentItems {
        get {
            Debug.Log("get items.");
            List<IXGameWindowContentItemModel> items = new List<IXGameWindowContentItemModel>();
            foreach (DictionaryEntry de in _properties) {
                Type t = de.Value.GetType();
                String className = "XGameWindowContentItemModel" + t.Name;
                Debug.Log("prop: " + de.Key.ToString() + " : " + de.Value.ToString() + " className: " + className);
                Type itemType;
                if ((itemType = Type.GetType(className)) != null) {
                    Debug.Log("type: " + itemType.ToString());
                    IXGameWindowContentItemModel item = itemType.Assembly.CreateInstance(className) as IXGameWindowContentItemModel;
                    item.value = de.Value;
                    item.name = de.Key.ToString();
                    items.Add(item);
                } else {

                }
            }
            return items;
        }
        set {
            throw new NotImplementedException();
        }
    }


    public virtual object value {
        get {
            return this;
        }
        set {
            throw new NotImplementedException();
        }
    }

    public virtual Type type {
        get {
            throw new NotImplementedException();
        }
        set {
            throw new NotImplementedException();
        }
    }



    public virtual void Save(object value) {
        
    }
}
