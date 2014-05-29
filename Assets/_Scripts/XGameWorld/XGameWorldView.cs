#region

using Assets._Scripts.XGameMap;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameTrigger.XGameEvent;
using Assets._Scripts.XGameUnit;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameWorld {
    public class XGameWorldView : XGameView<XGameWorldModel> {
        #region Instance Methods

        public override void InitEvents() {
            base.InitEvents();
            Model.On("change:worldMap", InitWorldMap);
            Model.On("add:characters", AddCharacter);
        }

        private void AddCharacter(XGameEvent e) {
            XGameCharacterModel c = e.data as XGameCharacterModel;
            GameObject gameUnits = GameObject.Find("XGameUnits") ?? new GameObject("XGameUnits");
            gameUnits.transform.parent = transform;
            XGame.CreateView<XGameCharacterView, XGameCharacterModel>(c, gameUnits.gameObject);
        }

        private void InitWorldMap(XGameEvent e) {
            XGameWorldMapModel map = e.data as XGameWorldMapModel;
            GameObject gameMap = GameObject.Find("XGameMap") ?? new GameObject("XGameMap") {isStatic = true};
            gameMap.transform.parent = transform;
            XGame.CreateView<XGameWorldMapView, XGameWorldMapModel>(map, gameMap.gameObject);
        }

        #endregion
    }
}