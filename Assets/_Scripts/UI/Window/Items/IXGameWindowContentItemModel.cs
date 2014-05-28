#region

using System;
using System.Collections.Generic;
using Assets._Scripts.XGameMVC;

#endregion

namespace Assets._Scripts.UI.Window.Items {
    public interface IXGameWindowContentItemModel : IXGameModel {
        #region Instance Properties

        string key { get; set; }
        IXGameWindowContentItemModel model { get; set; }
        string spriteName { get; set; }

        Type type { get; set; }
        object value { get; set; }

        List<IXGameWindowContentItemModel> windowContentItems { get; set; }

        #endregion

        #region Instance Methods

        void Save(object data);

        #endregion
    }
}