using System.Collections;
using UnityEngine;

namespace Assets._Scripts.XGameMap {
    public class XGameMap {
        protected XGameCell[,] _cells;

        public XGameCell[,] cells {
            get {
                return _cells;
            }
            set {
                _cells = value;
            }
        }

        protected XGameMapProperties _prop;

        public XGameMapProperties prop {
            get {
                return _prop;
            }
            set {
                _prop = value;
            }
        }

        public XGameMap() {

        }

        public static XGameMap generate(XGameMapProperties prop) {
            XGameMap map = new XGameMap { prop = prop, cells = new XGameCell[prop.sizeX, prop.sizeZ] };
            for (int i = 0; i < prop.sizeX; i++) {
                for (int j = 0; j < prop.sizeZ; j++) {
                    XGameCell cell = new XGameSquareCell();
                    map.cells[i, j] = cell;
                }
            }
            return map;
        }
    }

    public class XGameMapProperties {
        public int sizeX;
        public int sizeZ;
    }
}