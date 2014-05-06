using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XGameEditorModel : XGameModel {

    public List<XGameTrigger> triggers {
        get { return Get("triggers") as List<XGameTrigger>; }
        set { Set("triggers", value); }
    }

}
