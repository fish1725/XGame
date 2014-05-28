#region

using Assets._Scripts.XGameMVC;
using ProD;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameMap {
    public class XGameWorldMapView : XGameView<XGameWorldMapModel> {
        #region Instance Methods

        public override void InitEvents() {
            Model.On("change:map", InitModel);
        }

        private void InitModel(XGameEvent e) {
            Map map = e.data as Map;
            if (map != null) {
                Cell[,] cells = map.cellsOnMap;
                for (int i = 0; i < cells.GetLength(0); i++) {
                    for (int j = 0; j < cells.GetLength(1); j++) {
                        if (cells[i, j].type != "Path" && cells[i, j].type != "Door") continue;
                        GameObject brick = Instantiate(Resources.Load<GameObject>("Floor")) as GameObject;
                        if (brick == null) continue;
                        brick.transform.parent = transform;
                        brick.transform.position = new Vector3(cells[i, j].address.x, 0, cells[i, j].address.y);
                        //brick.transform.localScale = new Vector3(2, 2, 2);
                    }
                }
            }
            gameObject.isStatic = true;
        }

        #endregion
    }
}