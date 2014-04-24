using UnityEngine;
using System.Collections;

public class XGameTriggerController : XGameController {

    public GameTrigger CreateTrigger() {
        GameTrigger trigger = new GameTrigger();
        return trigger;
    }

    public void RegisterTrigger(GameTrigger trigger) {

    }

}
