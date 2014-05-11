using UnityEngine;
using System.Collections;

public class XGameWindowContentItemModel : XGameModel {

    public string spriteName {
        get { return Get("spriteName") as string; }
        set { Set("spriteName", value); }
    }
    public string desciption {
        get { return Get("desciption") as string; }
        set { Set("desciption", value); }
    }
    public object obj {
        get { return Get("obj"); }
        set { Set("obj", value); }
    }

}
