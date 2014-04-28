using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class XGameTriggerController : XGameController {

    public XGameTrigger CreateTrigger() {
        XGameTrigger trigger = new XGameTrigger();
        return trigger;
    }

    public void RegisterTrigger(XGameTrigger trigger) {

    }

    public List<MethodInfo> GetAllGameActions() {
        List<MethodInfo> result = new List<MethodInfo>();
        Type type = typeof(GameFunctionProxy);
        MethodInfo[] mis = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        for (int i = 0; i < mis.Length; i++) {
            //if (mis[i].ReturnType == typeof(void)) {
            //    ParameterInfo[] pis = mis[i].GetParameters();
            //    if (pis.Length == 1 && pis[0].ParameterType == typeof(XGameEvent)) {
            //        result.Add(mis[i]);
            //    }
            //}
            result.Add(mis[i]);
        }
        return result;
    }

    public List<MethodInfo> GetAllGameConditions() {
        List<MethodInfo> result = new List<MethodInfo>();
        Type type = typeof(GameFunctionProxy);
        MethodInfo[] mis = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        for (int i = 0; i < mis.Length; i++) {
            if (mis[i].ReturnType == typeof(bool)) {
                result.Add(mis[i]);
            }
        }
        return result;
    }

}
