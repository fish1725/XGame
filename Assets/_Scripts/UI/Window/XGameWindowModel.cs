using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XGameWindowModel : XGameModel {

    public List<IXGameWindowContentItemModel> content {
        get { return Get("content") as List<IXGameWindowContentItemModel>; }
        set { Set("content", value); }
    }

    public void AddContent(IXGameWindowContentItemModel item) {
        Add<IXGameWindowContentItemModel>("content", item);
    }

    public void RemoveContent(IXGameWindowContentItemModel item) {
        Remove<IXGameWindowContentItemModel>("content", item);
    }

    public bool active {
        get { return (bool)Get("active"); }
        set { Set("active", value); }
    }
}
