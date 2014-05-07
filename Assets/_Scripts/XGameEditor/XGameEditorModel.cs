using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XGameEditorModel : XGameModel {

    public List<XGameTrigger> triggers {
        get { return Get("triggers") as List<XGameTrigger>; }
        set { Set("triggers", value); }
    }

    public void AddTrigger(XGameTrigger trigger) {
        Add<XGameTrigger>("triggers", trigger);
    }

    public void RemoveTrigger(XGameTrigger trigger) {
        Remove<XGameTrigger>("triggers", trigger);
    }

    public List<XGameWindowModel> windows {
        get { return Get("windows") as List<XGameWindowModel>; }
        set { Set("windows", value); }
    }

    public void AddWindow(XGameWindowModel window) {
        Add<XGameWindowModel>("windows", window);
    }

    public void RemoveWindow(XGameWindowModel window) {
        Remove<XGameWindowModel>("windows", window);
    }

}
