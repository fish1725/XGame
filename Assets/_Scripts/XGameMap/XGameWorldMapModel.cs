using UnityEngine;
using System.Collections;
using ProD;
public class XGameWorldMapModel : XGameModel {
    private XGameMap[,] _maps;

    public XGameMap[,] maps {
        get {
            return _maps;
        }
        set {
            _maps = value;
        }
    }

    private XGameWorldMapProperties _prop;

    public XGameWorldMapProperties prop {
        get {
            return _prop;
        }
        set {
            _prop = value;
        }
    }

    public Map map {
        get { return Get("map") as Map; }
        set { Set("map", value); }
    }

    public XGameWorldMapModel() {

    }

    public static XGameWorldMapModel generate(XGameWorldMapProperties prop) {
        XGameWorldMapModel worldMap = new XGameWorldMapModel();
        worldMap.prop = prop;
        worldMap.maps = new XGameMap[prop.sizeX, prop.sizeZ];
        for (int i = 0; i < prop.sizeX; i++) {
            for (int j = 0; j < prop.sizeZ; j++) {
                XGameMap map = new XGameMap();
                worldMap.maps[i, j] = map;
            }
        }
        return worldMap;
    }
}

public class XGameWorldMapProperties {
    public int sizeX;
    public int sizeZ;
}
