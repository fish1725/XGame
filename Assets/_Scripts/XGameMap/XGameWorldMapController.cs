#region

using Assets._Scripts.XGameMVC;
using ProD;

#endregion

namespace Assets._Scripts.XGameMap {
    public class XGameWorldMapController : XGameController {
        #region Instance Methods

        public XGameWorldMapModel CreateWorldMap() {
            XGameWorldMapModel map = new XGameWorldMapModel();
            return map;
        }

        public void InitMap(XGameWorldMapModel worldMap) {
            worldMap.map = Generator_Dungeon.Generate();
        }

        #endregion
    }
}