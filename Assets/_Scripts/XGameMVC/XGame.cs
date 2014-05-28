#region

using System.Collections;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameMVC {
    public class XGame : MonoBehaviour {
        #region Readonly & Static Fields

        private static readonly Hashtable _instances = new Hashtable();

        #endregion

        #region Instance Methods

        public virtual void Setup() {
        }

        protected void Start() {
            Setup();
        }

        #endregion

        #region Class Methods

        public static T CreateController<T>() where T : XGameController, new() {
            T controller = new T();
            RegisterInstance(controller);
            return controller;
        }

        public static T CreateView<T, TT>(TT model, GameObject parent = null)
            where T : XGameView<TT>
            where TT : IXGameModel {
            GameObject go = new GameObject {name = typeof (T).Name};
            if (parent != null) {
                go.transform.parent = parent.transform;
                go.layer = parent.layer;
            }
            go.transform.localScale = Vector3.one;
            T view = go.AddComponent<T>();
            view.Model = model;
            view.InitEvents();
            view.Init();
            return view;
        }

        public static T RegisterInstance<T>(T instance) {
            string name = typeof (T).ToString();
            _instances[name] = instance;
            return instance;
        }

        public static void RemoveView<T, TT>(T view)
            where T : XGameView<TT>
            where TT : IXGameModel {
            GameObject go = view.gameObject;
            view.dispose();
            Destroy(go);
        }

        public static T Resolve<T>() {
            return (T) (_instances[typeof (T).ToString()]);
        }

        #endregion
    }
}