#region

using Assets._Scripts.XGameMVC;
using ProD;

#endregion

namespace Assets._Scripts.XGameMap {
    public class XGameWorldMapModel : XGameModel {
        #region Fields

        #endregion

        #region C'tors

        #endregion

        #region Instance Properties

        public Map map {
            get { return Get("map") as Map; }
            set { Set("map", value); }
        }

        public XGameMap[,] maps { get; set; }

        public XGameWorldMapProperties prop { get; set; }

        #endregion

        #region Class Methods

        public static XGameWorldMapModel generate(XGameWorldMapProperties prop) {
            XGameWorldMapModel worldMap = new XGameWorldMapModel {
                prop = prop,
                maps = new XGameMap[prop.sizeX, prop.sizeZ]
            };
            for (int i = 0; i < prop.sizeX; i++) {
                for (int j = 0; j < prop.sizeZ; j++) {
                    XGameMap map = new XGameMap();
                    worldMap.maps[i, j] = map;
                }
            }
            return worldMap;
        }

        #endregion
    }

    public class XGameWorldMapProperties {
        #region Fields

        public int sizeX;
        public int sizeZ;

        #endregion
    }
}