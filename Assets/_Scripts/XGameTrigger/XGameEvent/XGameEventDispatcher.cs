#region

using System;
using System.Collections.Generic;

#endregion

namespace Assets._Scripts.XGameTrigger.XGameEvent {
    public delegate void XGameEventHandler(XGameEvent e);

    public class XGameEventDispatcher {
        #region Readonly & Static Fields

        private readonly Dictionary<string, Delegate> _eventTable;

        #endregion

        #region C'tors

        public XGameEventDispatcher() {
            _eventTable = new Dictionary<string, Delegate>();
        }

        #endregion

        #region Instance Methods

        public void addEventListener(string eventType, XGameEventHandler func) {
            if (!_eventTable.ContainsKey(eventType)) {
                _eventTable.Add(eventType, null);
            }
            _eventTable[eventType] = ((XGameEventHandler) _eventTable[eventType]) + func;
        }

        public void addEventListener(XGameEventType eventType, XGameEventHandler func) {
            addEventListener(eventType.ToString(), func);
        }

        public void broadcast(string eventType, XGameEvent e = null) {
            if (_eventTable.ContainsKey(eventType) && _eventTable[eventType] != null) {
                ((XGameEventHandler) _eventTable[eventType])(e);
            }
        }

        public void broadcast(XGameEventType eventType, XGameEvent e = null) {
            broadcast(eventType.ToString(), e);
        }

        public void broadcast(XGameEvent e) {
            broadcast(e.name, e);
        }

        public void removeEventListener(string eventType, XGameEventHandler func) {
            if (_eventTable.ContainsKey(eventType)) {
                _eventTable[eventType] = ((XGameEventHandler) _eventTable[eventType]) - func;
            }
        }

        public void removeEventListener(XGameEventType eventType, XGameEventHandler func) {
            removeEventListener(eventType.ToString(), func);
        }

        #endregion
    }
}