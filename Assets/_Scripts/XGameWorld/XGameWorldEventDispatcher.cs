#region

using Assets._Scripts.XGameTrigger.XGameEvent;

#endregion

namespace Assets._Scripts.XGameWorld {
    public class XGameWorldEventDispatcher : XGameEventDispatcher {
        #region Readonly & Static Fields

        private static XGameWorldEventDispatcher _instance;

        #endregion

        #region C'tors

        private XGameWorldEventDispatcher() {
        }

        #endregion

        #region Class Properties

        public static XGameWorldEventDispatcher instance {
            get { return _instance ?? (_instance = new XGameWorldEventDispatcher()); }
        }

        #endregion
    }
}