#region

using Assets._Scripts.XGameMap;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameUnit;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameWorld {
    public class XGameWorldController : XGameController {
        #region Fields

        private XGameCharacterController _characterController = new XGameCharacterController();
        private XGameWorldMapController _worldMapController = new XGameWorldMapController();

        #endregion

        #region Instance Properties

        public XGameCharacterController characterController {
            get { return _characterController; }
            set { _characterController = value; }
        }

        public XGameWorldMapController worldMapController {
            get { return _worldMapController; }
            set { _worldMapController = value; }
        }

        #endregion

        #region Instance Methods

        public XGameWorldModel CreateWorld() {
            XGameWorldModel world = new XGameWorldModel();
            return world;
        }

        public void InitCharacters(XGameWorldModel world) {
            XGameCharacterModel c = _characterController.CreateCharacter();
            _characterController.SetCharacterPosition(c, new Vector3(20, 0, 20));
            world.AddCharacter(c);
            c = _characterController.CreateCharacter();
            _characterController.SetCharacterPosition(c, new Vector3(24, 0, 24));
            world.AddCharacter(c);
        }

        public void InitWorldMap(XGameWorldModel world) {
            world.worldMap = _worldMapController.CreateWorldMap();
            _worldMapController.InitMap(world.worldMap);
        }

        #endregion
    }
}