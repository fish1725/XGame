#region

using UnityEngine;

#endregion

namespace Assets._Scripts.XGameMVC {
    public class XGameView<T> : MonoBehaviour where T : IXGameModel {
        #region Instance Properties

        public T Model { get; set; }

        #endregion

        #region Instance Methods

        public virtual void Init() {
        }

        public virtual void InitEvents() {
        }

        public virtual void dispose() {
        }

        #endregion
    }
}