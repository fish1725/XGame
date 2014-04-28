using UnityEngine;
using System;
using System.Collections.Generic;

public delegate void XGameEventHandler(XGameEvent e);
public class XGameEventDispatcher {

    private Dictionary<string, Delegate> _eventTable = null;

    public XGameEventDispatcher() {
        _eventTable = new Dictionary<string, Delegate>();
    }

    public void addEventListener(string eventType, XGameEventHandler func) {
        if (!_eventTable.ContainsKey(eventType)) {
            _eventTable.Add(eventType, null);
        }
        _eventTable[eventType] = ((XGameEventHandler)_eventTable[eventType]) + func;
    }

    public void addEventListener(XGameEventType eventType, XGameEventHandler func) {
        addEventListener(eventType.ToString(), func);
    }

    public void removeEventListener(string eventType, XGameEventHandler func) {
        if (_eventTable.ContainsKey(eventType)) {
            _eventTable[eventType] = ((XGameEventHandler)_eventTable[eventType]) - func;
        }
    }

    public void removeEventListener(XGameEventType eventType, XGameEventHandler func) {
        removeEventListener(eventType.ToString(), func);
    }

    public void broadcast(string eventType, XGameEvent e = null) {
        if (_eventTable.ContainsKey(eventType) && _eventTable[eventType] != null) {
            ((XGameEventHandler)_eventTable[eventType])(e);
        }
    }

    public void broadcast(XGameEventType eventType, XGameEvent e = null) {
        broadcast(eventType.ToString(), e);
    }

    public void broadcast(XGameEvent e) {
        broadcast(e.name, e);
    }
}
