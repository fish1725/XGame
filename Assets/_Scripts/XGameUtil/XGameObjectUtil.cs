#region

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace Assets._Scripts.XGameUtil {
    public class XGameObjectUtil {
        #region Class Methods

        public static GameObject CreateGameObject(string name = "New GameObject", GameObject parent = null) {
            GameObject go = new GameObject(name);
            if (parent == null) return go;
            go.transform.parent = parent.transform;
            go.layer = parent.layer;
            return go;
        }

        public static T GetComponentInAncestors<T>(GameObject go) where T : Component {
            Transform temp = go.transform;
            while (temp) {
                T result = temp.GetComponent<T>();
                if (result) {
                    return result;
                }
                temp = temp.parent;
            }
            return null;
        }

        public static List<T> GetComponentsInAncestors<T>(GameObject go) where T : Component {
            Transform temp = go.transform;
            List<T> re = new List<T>();
            while (temp) {
                T result = temp.GetComponent<T>();
                if (result) {
                    re.Add(result);
                }
                temp = temp.parent;
            }
            return re.Count > 0 ? re : null;
        }

        public static void RemoveAllChildren(GameObject go) {
            foreach (Transform child in go.transform) {
                Object.Destroy(child.gameObject);
            }
        }

        public static IEnumerator WaitAndDo(int ret, Action act) {
            yield return ret;
            act();
        }

        public static IEnumerator WaitAndDo(YieldInstruction ret, Action act) {
            yield return ret;
            act();
        }

        #endregion
    }
}