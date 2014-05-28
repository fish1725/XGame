#region

using System;
using System.Collections;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameUtil {
    public class XGameObjectUtil {
        #region Class Methods

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