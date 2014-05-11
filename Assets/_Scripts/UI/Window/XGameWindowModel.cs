using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XGameWindowModel : XGameModel {

    public List<XGameWindowContentItemModel> content {
        get { return Get("content") as List<XGameWindowContentItemModel>; }
        set { Set("content", value); }
    }

    public void AddContent(XGameWindowContentItemModel item) {
        Add<XGameWindowContentItemModel>("content", item);
    }

    public void RemoveContent(XGameWindowContentItemModel item) {
        Remove<XGameWindowContentItemModel>("content", item);
    }
}
