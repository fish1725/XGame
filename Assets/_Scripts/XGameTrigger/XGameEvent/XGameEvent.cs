#region

using System.Xml.Serialization;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameTrigger.XGameEvent {
    public class XGameEvent {
        #region Fields

        protected object _data;
        protected string _message;
        protected XGameEventType _type;

        #endregion

        #region C'tors

        public XGameEvent() {
            _type = XGameEventType.None;
            _message = "Message";
            _data = null;
        }

        public XGameEvent(XGameEventType type) {
            _type = type;
            _message = "Message";
            _data = null;
        }

        #endregion

        #region Instance Properties

        public object data {
            get { return _data; }
            set { _data = value; }
        }

        public string message {
            get { return _message; }
            set { _message = value; }
        }

        public string name {
            get { return _type.ToString(); }
        }

        [XmlAttribute(AttributeName = "type")]
        public XGameEventType type {
            get { return _type; }
            set { _type = value; }
        }

        #endregion

        #region Class Methods

        public static XGameEvent GameUnitEvent(XGameUnitEventType type) {
            XGameEventType et = GetGameEventType("Unit_", type.ToString());
            return new XGameEvent(et);
        }

        private static XGameEventType GetGameEventType(string prefix, string suffix) {
            XGameEventType get = XGameEventType.None;
            try {
                get = (XGameEventType) System.Enum.Parse(typeof (XGameEventType), prefix + suffix);
            }
            catch (System.Exception e) {
                Debug.Log(e.Message);
            }
            return get;
        }

        #endregion
    }
}