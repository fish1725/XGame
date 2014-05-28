#region

using Assets._Scripts.XGameMVC;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameUnit {
    public class XGameUnitModel : XGameModel {
        #region Instance Properties

        public Vector3 position {
            get { return (Vector3) Get("position"); }
            set { Set("position", value); }
        }

        public XGameUnitType unitType {
            get { return (XGameUnitType) Get("unitType"); }
            set { Set("unitType", value); }
        }

        #endregion
    }
}