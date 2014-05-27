using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class XGameTriggerController : XGameController {

    public XGameTrigger CreateTrigger(string name, List<XGameEvent> events, List<XGameCondition> conditions, List<XGameAction> actions) {
        XGameTrigger trigger = new XGameTrigger();
        trigger.id = Guid.NewGuid();
        trigger.name = name;
        trigger.gameEvents = events;
        trigger.gameConditions = conditions;
        trigger.gameActions = actions;
        trigger.testint = new XGameUnitModel();
        return trigger;
    }

    public void RegisterTrigger(XGameTrigger trigger) {
        List<XGameEvent> events = trigger.gameEvents;
        foreach (XGameEvent e in events) {
            XGameWorldEventDispatcher.instance.addEventListener(e.type, (XGameEvent ge) => { trigger.Execute(); });
        }
    }

    public List<MethodInfo> GetAllGameActions() {
        List<MethodInfo> result = new List<MethodInfo>();
        Type type = typeof(XGameController);
        MethodInfo[] mis = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
        for (int i = 0; i < mis.Length; i++) {
            if (mis[i].ReturnType == typeof(void)) {
                result.Add(mis[i]);
            }
        }
        return result;
    }

    public List<MethodInfo> GetAllGameConditions() {
        List<MethodInfo> result = new List<MethodInfo>();
        Type type = typeof(XGameController);
        MethodInfo[] mis = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
        for (int i = 0; i < mis.Length; i++) {
            if (mis[i].ReturnType == typeof(bool)) {
                result.Add(mis[i]);
            }
        }
        return result;
    }

    public List<XGameEvent> GetAllGameEvents() {
        List<XGameEvent> events = new List<XGameEvent>();
        Array es = Enum.GetValues(typeof(XGameEventType));
        foreach (XGameEventType e in es) {
            events.Add(new XGameEvent(e));
        }
        return events;
    }

}
