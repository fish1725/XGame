﻿using UnityEngine;
using System.Collections.Generic;

public class XGameWorldController : XGameController {
    private XGameWorldMapController _worldMapController = new XGameWorldMapController();
    private XGameCharacterController _characterController = new XGameCharacterController();

    public XGameWorldModel CreateWorld() {
        XGameWorldModel world = new XGameWorldModel();
        return world;
    }

    public void InitWorldMap(XGameWorldModel world) {
        world.worldMap = _worldMapController.CreateWorldMap();
        _worldMapController.InitMap(world.worldMap);
    }

    public void InitCharacters(XGameWorldModel world) {
        XGameCharacterModel c = _characterController.CreateCharacter();
        _characterController.SetCharacterPosition(c, new Vector3(20, 0, 20));
        world.AddCharacter(c);
        c = _characterController.CreateCharacter();
        _characterController.SetCharacterPosition(c, new Vector3(24, 0, 24));
        world.AddCharacter(c);
    }

}
