#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Assets._Scripts.XGameTrigger.XGameEvent;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameMVC {
    public class XGameModel : IXGameModel {
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

        public void CopyFrom(XGameModel source, bool triggerEvent = false) {
            foreach (String p in source.properties.Keys) {
                if (triggerEvent)
                    Set(p, source.properties[p]);
                else
                    _properties[p] = source.properties[p];
            }
        }

        protected void Add<T>(string property, T data) {
            List<T> list = _properties[property] as List<T> ?? new List<T>();
            list.Add(data);
            _events.broadcast("add:" + property, new XGameEvent {data = data});
        }

        protected void Remove<T>(string property, T data) {
            var list = _properties[property] as List<T>;
            if (list == null) return;
            if (list.Remove(data)) {
                _events.broadcast("remove:" + property, new XGameEvent {data = data});
            }
        }

        #endregion

        #region IXGameModel Members

        public void Set(string property, object data) {
            _properties[property] = data;
            _events.broadcast("change:" + property, new XGameEvent {data = data});
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
            get { return (Guid) (Get("id")); }
            set { Set("id", value); }
        }

        public string name {
            get { return Get("name") as string; }
            set { Set("name", value); }
        }

        #endregion
    }
}