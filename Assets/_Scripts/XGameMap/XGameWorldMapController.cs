using UnityEngine;
using System.Collections;
using ProD;

public class XGameWorldMapController : XGameController {

    public XGameWorldMapModel CreateWorldMap() {
        XGameWorldMapModel map = new XGameWorldMapModel();
        return map;
    }

    public void InitMap(XGameWorldMapModel worldMap) {
        worldMap.map = Generator_Dungeon.Generate();
    }

}
