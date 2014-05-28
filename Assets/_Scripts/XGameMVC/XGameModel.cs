#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Assets._Scripts.UI.Window.Items;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameMVC {
    public class XGameModel : IXGameWindowContentItemModel {
        #region Readonly & Static Fields

        private readonly XGameEventDispatcher _events = new XGameEventDispatcher();

        #endregion

        #region Fields

        private Hashtable _properties = new Hashtable();

        #endregion

        #region C'tors

        public XGameModel() {
            id = Guid.NewGuid();
            name = GetType().Name;
        }

        #endregion

        #region Instance Properties

        [XmlIgnore]
        public Hashtable properties {
            get { return _properties; }
            protected set { _properties = value; }
        }

        #endregion

        #region Instance Methods

        public override string ToString() {
            Debug.Log(GetType().Name + " props: =============");
            foreach (String p in _properties.Keys) {
                Debug.Log(p + ": " + _properties[p]);
            }
            Debug.Log(GetType().Name + " props end. ");
            return base.ToString();
        }

        public void CopyFrom(XGameModel model) {
            foreach (String p in model.properties.Keys) {
                Set(p, model.properties[p]);
            }
        }

        protected void Add<T>(string property, T data) {
            List<T> list = _properties[property] as List<T> ?? new List<T>();
            list.Add(data);
            _events.broadcast("add:" + property, new XGameEvent { data = data });
        }

        protected void Remove<T>(string property, T data) {
            var list = _properties[property] as List<T>;
            if (list == null) return;
            if (list.Remove(data)) {
                _events.broadcast("remove:" + property, new XGameEvent { data = data });
            }
        }

        private IXGameWindowContentItemModel CreateWindowContentItem(Type classType, object itemKey, object itemValue) {
            if (!typeof(IXGameWindowContentItemModel).IsAssignableFrom(classType)) return null;
            var item = Activator.CreateInstance(classType) as IXGameWindowContentItemModel;
            if (item == null) return null;
            item.value = itemValue;
            item.key = itemKey.ToString();
            item.model = this;
            return item;
        }

        #endregion

        #region IXGameWindowContentItemModel Members

        public void Set(string property, object data) {
            _properties[property] = data;
            _events.broadcast("change:" + property, new XGameEvent { data = data });
        }

        public object Get(string property) {
            return _properties[property];
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

        public IXGameWindowContentItemModel model { get; set; }

        public virtual string spriteName {
            get { return "Buttons_RightArrow"; }
            set { name = value; }
        }

        [XmlIgnore]
        public virtual List<IXGameWindowContentItemModel> windowContentItems {
            get {
                //Debug.Log("get items.");
                var items = new List<IXGameWindowContentItemModel>();
                foreach (DictionaryEntry de in _properties) {
                    Type t = de.Value.GetType();
                    Type itemModelBaseType = typeof(XGameWindowContentItemModel);
                    String className = "XGameWindowContentItemModel" + t.Name;
                    Type itemModelType = Type.GetType(itemModelBaseType.Namespace + "." + className);
                    IXGameWindowContentItemModel item = CreateWindowContentItem(itemModelType, de.Key, de.Value);
                    if (item != null) {
                        items.Add(item);
                    } else {
                        item = CreateWindowContentItem(t, de.Key, de.Value);
                        if (item != null) {
                            items.Add(item);
                        }
                    }
                }
                return items;
            }
            set { throw new NotImplementedException(); }
        }


        public virtual object value {
            get { return this; }
            set { CopyFrom(value as XGameModel); }
        }

        public virtual Type type {
            get { return GetType(); }
            set { throw new NotImplementedException(); }
        }



        public virtual void Save(object data) {
            if (model == null) return;
            Debug.Log("save key: " + key + " data: " + data);
            model.Set(key, data);
        }


        public virtual string key { get; set; }

        #endregion
    }
}