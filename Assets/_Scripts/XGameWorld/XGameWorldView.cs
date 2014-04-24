﻿using ProD;
using System.Collections.Generic;
using UnityEngine;

public class XGameWorldView : XGameView {

    new public XGameWorldModel Model {
        get { return base.Model as XGameWorldModel; }
        set { base.Model = value; }
    }

    public override void InitEvents() {
        base.InitEvents();
        Model.On("change:worldMap", InitWorldMap);
        Model.On("add:characters", InitCharacters);
    }

    void InitWorldMap(XGameEvent e) {
        XGameWorldMapModel map = e.data as XGameWorldMapModel;
        XGameWorldMapView view = XGame.CreateView<XGameWorldMapView>(map);
        GameObject gameMap = GameObject.Find("XGameMap");
        if (gameMap == null) {
            gameMap = new GameObject("XGameMap");
            gameMap.isStatic = true;
        }
        gameMap.transform.parent = transform;
        view.transform.parent = gameMap.transform;
    }

    void InitCharacters(XGameEvent e) {
        XGameCharacterModel c = e.data as XGameCharacterModel;
        XGameCharacterView view = XGame.CreateView<XGameCharacterView>(c);
        GameObject gameUnits = GameObject.Find("XGameUnits");
        if (gameUnits == null) {
            gameUnits = new GameObject("XGameUnits");
        }
        gameUnits.transform.parent = transform;
        view.transform.parent = gameUnits.transform;
    }
}
