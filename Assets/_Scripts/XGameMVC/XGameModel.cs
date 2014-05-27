using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class XGameModel : IXGameModel, IXGameWindowContentItemModel {
    private Hashtable _properties = new Hashtable();
    private XGameEventDispatcher _events = new XGameEventDispatcher();
    private String _key;

    public Hashtable properties {
        get { return _properties; }
        protected set { _properties = value; }
    }

    public XGameModel() {
        id = Guid.NewGuid();
        name = this.GetType().Name;
    }

    public void Set(string property, object value) {
        _properties[property] = value;
        _events.broadcast("change:" + property, new XGameEvent() { data = value });
    }

    public object Get(string property) {
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

    public void CopyFrom(XGameModel model) {
        foreach (String p in model.properties.Keys) {
            Set(p, model.properties[p]);
        }
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
            //Debug.Log("get items.");
            List<IXGameWindowContentItemModel> items = new List<IXGameWindowContentItemModel>();
            foreach (DictionaryEntry de in _properties) {
                Type t = de.Value.GetType();
                String className = "XGameWindowContentItemModel" + t.Name;
                //Debug.Log("prop: " + de.Key.ToString() + " : " + de.Value.ToString() + " className: " + className);
                IXGameWindowContentItemModel item = CreateWindowContentItem(className, de.Key, de.Value);
                if (item != null) {
                    items.Add(item);
                } else {
                    className = t.Name;
                    item = CreateWindowContentItem(className, de.Key, de.Value);
                    if (item != null) {
                        items.Add(item);
                    }
                }
            }
            return items;
        }
        set {
            throw new NotImplementedException();
        }
    }

    private IXGameWindowContentItemModel CreateWindowContentItem(String className, object key, object value) {
        Type itemType;
        if ((itemType = Type.GetType(className)) != null && typeof(IXGameWindowContentItemModel).IsAssignableFrom(itemType)) {
            //Debug.Log("type: " + itemType.ToString());
            IXGameWindowContentItemModel item = Activator.CreateInstance(itemType) as IXGameWindowContentItemModel;
            item.value = value;
            item.key = key.ToString();
            return item;
        } else return null;
    }


    public virtual object value {
        get {
            return this;
        }
        set {
            CopyFrom(value as XGameModel);
        }
    }

    public virtual Type type {
        get {
            return this.GetType();
        }
        set {
            throw new NotImplementedException();
        }
    }

    public virtual void Save(object value) {
        Debug.Log("Save model: " + value);
        this.value = value;
    }

    public override string ToString() {
        Debug.Log(this.GetType().Name + " props: ");
        foreach (String p in _properties.Keys) {
            Debug.Log(p + ": " + _properties[p]);
        }
        Debug.Log(this.GetType().Name + " props end. ");
        return base.ToString();
    }


    public virtual string key {
        get {
            return _key;
        }
        set {
            _key = value;
        }
    }
}
