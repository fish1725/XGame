using System;

public interface IXGameModel {
    void Set(string property, object value);

    object Get(string property);

    void On(string eventName, XGameEventHandler func);

    void Off(string eventName, XGameEventHandler func);

    Guid id { get; set; }

    string name { get; set; }
}
