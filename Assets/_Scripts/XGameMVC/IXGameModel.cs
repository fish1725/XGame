#region

using System;
using Assets._Scripts.XGameTrigger.XGameEvent;

#endregion

namespace Assets._Scripts.XGameMVC {
    public interface IXGameModel {
        #region Instance Properties

        Guid id { get; set; }

        string name { get; set; }

        #endregion

        #region Instance Methods

        object Get(string property);
        void Off(string eventName, XGameEventHandler func);
        void On(string eventName, XGameEventHandler func);
        void Set(string property, object data);

        #endregion
    }
}