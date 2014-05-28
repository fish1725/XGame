#region

using System.Collections.Generic;
using Assets._Scripts.UI.Window.Items;
using Assets._Scripts.XGameMVC;

#endregion

namespace Assets._Scripts.UI.Window {
    public class XGameWindowModel : XGameModel {
        #region Instance Properties

        public bool active {
            get { return (bool) Get("active"); }
            set { Set("active", value); }
        }

        public List<IXGameWindowContentItemModel> content {
            get { return Get("content") as List<IXGameWindowContentItemModel>; }
            set { Set("content", value); }
        }

        #endregion

        #region Instance Methods

        public void AddContent(IXGameWindowContentItemModel item) {
            Add("content", item);
        }

        public void RemoveAllContent() {
            if (content != null) {
                List<IXGameWindowContentItemModel> itemsToRemove = new List<IXGameWindowContentItemModel>();
                for (int i = 0, length = content.Count; i < length; i++) {
                    itemsToRemove.Add(content[i]);
                }
                foreach (IXGameWindowContentItemModel item in itemsToRemove) {
                    Remove("content", item);
                }
            }
        }

        public void RemoveContent(IXGameWindowContentItemModel item) {
            Remove("content", item);
        }

        #endregion
    }
}