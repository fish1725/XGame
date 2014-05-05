using UnityEngine;
using System.Collections;
using ProD;

public class XGameWorldMapView : XGameView<XGameWorldMapModel> {

    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public override void InitEvents() {
        Model.On("change:map", InitModel);
    }

    void InitModel(XGameEvent e) {
        Map map = e.data as Map;
        Cell[,] cells = map.cellsOnMap;
        for (int i = 0; i < cells.GetLength(0); i++) {
            for (int j = 0; j < cells.GetLength(1); j++) {
                if (cells[i, j].type == "Path" || cells[i, j].type == "Door") {
                    GameObject brick = GameObject.Instantiate(Resources.Load<GameObject>("Floor")) as GameObject;
                    brick.transform.parent = transform;
                    brick.transform.position = new Vector3(cells[i, j].address.x, 0, cells[i, j].address.y);
                    //brick.transform.localScale = new Vector3(2, 2, 2);
                }
            }
        }
        gameObject.isStatic = true;
    }
}
