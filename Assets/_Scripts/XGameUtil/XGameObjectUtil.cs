using UnityEngine;
using System.Collections;
using System;

public class XGameObjectUtil {

    public static T GetComponentInAncestors<T>(GameObject go) where T : Component {
        Transform temp = go.transform;
        T result;
        while (temp) {
            result = temp.GetComponent<T>();
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
}
