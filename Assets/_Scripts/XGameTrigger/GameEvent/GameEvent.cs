using UnityEngine;

public class GameEvent {

    private string _eventName = null;

    public string eventName {
        get { return _eventName; }
        set { _eventName = value; }
    }

    public GameEvent() {
        _eventName = "";
    }

    public GameEvent(string eventName) {
        _eventName = eventName;
    }

    private static XGameEventType GetGameEventType(string prefix, string suffix) {
        XGameEventType get = XGameEventType.None;
        try {
            get = (XGameEventType)System.Enum.Parse(typeof(XGameEventType), prefix + suffix);
        } catch (System.Exception e) {
            Debug.Log(e.Message);
        }
        return get;
    }

    public static GameEvent GameUnitEvent(GameUnitEventType type) {
        XGameEventType get = GetGameEventType("Unit_", type.ToString());
        return new GameEvent(get.ToString());
    }

}
