using UnityEngine;
using System.Collections;

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
}
