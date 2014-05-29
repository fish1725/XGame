using System;
using System.Collections.Generic;
using System.Reflection;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameWorld;

namespace Assets._Scripts.XGameTrigger {
    public class XGameTriggerController : XGameController {

        public XGameTriggerModel CreateTrigger(string name, List<XGameEvent.XGameEvent> events, List<XGameCondition.XGameCondition> conditions, List<XGameAction.XGameAction> actions) {
            XGameTriggerModel trigger = new XGameTriggerModel {
                id = Guid.NewGuid(),
                name = name,
                gameEvents = events,
                gameConditions = conditions,
                gameActions = actions,
                testint = 1
            };
            return trigger;
        }

        public void RegisterTrigger(XGameTriggerModel trigger) {
            List<XGameEvent.XGameEvent> events = trigger.gameEvents;
            foreach (XGameEvent.XGameEvent e in events) {
                XGameWorldEventDispatcher.instance.addEventListener(e.type, ge => trigger.Execute());
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

        public List<XGameEvent.XGameEvent> GetAllGameEvents() {
            List<XGameEvent.XGameEvent> events = new List<XGameEvent.XGameEvent>();
            Array es = Enum.GetValues(typeof(XGameEventType));
            foreach (XGameEventType e in es) {
                events.Add(new XGameEvent.XGameEvent(e));
            }
            return events;
        }

    }
}
