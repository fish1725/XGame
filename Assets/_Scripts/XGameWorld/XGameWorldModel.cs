#region

using System.Collections.Generic;
using Assets._Scripts.XGameMap;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameUnit;

#endregion

namespace Assets._Scripts.XGameWorld {
    public class XGameWorldModel : XGameModel {
        #region C'tors

        #endregion

        #region Instance Properties

        public List<XGameCharacterModel> characters {
            get { return Get("characters") as List<XGameCharacterModel>; }
            set { Set("characters", value); }
        }

        public XGameWorldMapModel worldMap {
            get { return Get("worldMap") as XGameWorldMapModel; }
            set { Set("worldMap", value); }
        }

        #endregion

        #region Instance Methods

        public void AddCharacter(XGameCharacterModel c) {
            Add("characters", c);
        }

        public void RemoveCharacter(XGameCharacterModel c) {
            Remove("characters", c);
        }

        #endregion
    }
}