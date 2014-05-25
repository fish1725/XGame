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

    public void RemoveAllContent() {
        if (content != null) {
            List<IXGameWindowContentItemModel> itemsToRemove = new List<IXGameWindowContentItemModel>();
            for (int i = 0, length = content.Count; i < length; i++) {
                itemsToRemove.Add(content[i]);
            }
            foreach (IXGameWindowContentItemModel item in itemsToRemove) {
                Remove<IXGameWindowContentItemModel>("content", item);
            }
        }
            
    }

    public bool active {
        get { return (bool)Get("active"); }
        set { Set("active", value); }
    }
}
