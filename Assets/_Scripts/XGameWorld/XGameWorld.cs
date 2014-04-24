using UnityEngine;
using System.Collections;

public class XGameWorld : XGame {
    private XGameWorldController _worldController = null;

    public override void Setup() {
        base.Setup();
        _worldController = CreateController<XGameWorldController>();

        XGameWorldModel world = _worldController.CreateWorld();

        XGameWorldView view = CreateView<XGameWorldView>(world);
        view.transform.parent = transform;

        _worldController.InitWorldMap(world);
        _worldController.InitCharacters(world);

        RegisterInstance<XGameWorldModel>(world);
    }
}
