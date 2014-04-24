using UnityEngine;
using System.Collections;

public class XGameEvent {
    protected XGameEventType _type;
    protected string _message;
    protected object _data;

    public XGameEventType type {
        get { return _type; }
        set { _type = value; }
    }

    public string name {
        get { return _type.ToString(); }
    }

    public string message {
        get { return _message; }
        set { _message = value; }
    }

    public object data {
        get { return _data; }
        set { _data = value; }
    }

    public XGameEvent() {
        _type = XGameEventType.None;
        _message = "Message";
        _data = null;
    }

}
