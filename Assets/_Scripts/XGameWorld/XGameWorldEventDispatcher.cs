using UnityEngine;
using System.Collections;

public class XGameWorldEventDispatcher : XGameEventDispatcher {

    private static XGameWorldEventDispatcher _instance = null;

    public static XGameWorldEventDispatcher instance {
        get {
            if (_instance == null) {
                _instance = new XGameWorldEventDispatcher();
            }
            return _instance;
        }
    }

    private XGameWorldEventDispatcher() { }

}
