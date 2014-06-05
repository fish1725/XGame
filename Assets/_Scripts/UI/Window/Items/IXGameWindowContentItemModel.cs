#region

using System;
using System.Collections.Generic;
using Assets._Scripts.XGameMVC;

#endregion

namespace Assets._Scripts.UI.Window.Items {
    public interface IXGameWindowContentItemModel : IXGameModel {
        #region Instance Properties

        List<IXGameWindowContentItemModel> children { get; set; }
        string key { get; set; }
        XGameModel parent { get; set; }
        string spriteName { get; set; }
        Type type { get; set; }
        object value { get; set; }

        #endregion
    }
}