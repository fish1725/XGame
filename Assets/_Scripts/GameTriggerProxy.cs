using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class GameTriggerProxy {

    private object Data;
    public IDictionary<string, IList<XGameTrigger>> Triggers { get { return Data as IDictionary<string, IList<XGameTrigger>>; } set { Data = value; } }

    public GameTriggerProxy() {
        Triggers = new Dictionary<string, IList<XGameTrigger>>();
        Load();
        XmlSerializer serializer = new XmlSerializer(typeof(XGameTrigger));
        StringWriter sw = new StringWriter();
        serializer.Serialize(sw, Triggers[XGameEventType.Unit_Die.ToString()][0]);
        StringReader sr = new StringReader(sw.ToString());
        Debug.Log(sw.ToString());
        sw = new StringWriter();
        serializer.Serialize(sw, serializer.Deserialize(sr));
        Debug.Log(sw.ToString());

    }

    public void Load() {
        //System.Type tp = typeof (GameFunctionProxy);
        //MethodInfo mi = tp.GetMethod ("GetBool");
        //GameMethodCallExpression gmce = GameExpression.Call("GetBool", new GameExpression[] { GameExpression.Constant(1), GameExpression.Constant(2) });

        //RegisterTrigger (new GameTrigger () { 
        //    GameEvent = GameEvent.GameUnitEvent(GameUnitEventType.Die), 
        //    GameCondition = GameCondition.BooleanComparison(gmce, GameExpression.Constant(true), GameConditionOperator.Equal),
        //    GameActions = new List<GameAction>() { 
        //        new GameAction() { Name = "action1", Command = typeof (TestCommand) },
        //        new GameAction() { Name = "action2", Command = typeof (TestCommand) } 
        //    } 
        //});
        //		RegisterTrigger (new GameTrigger () { 
        //			GameEvent = new GameConstantExpression () { Value = ApplicationConstants.EVENT_TEST }, 
        //			GameCondition = new GameConstantExpression() { Value = true }, 
        //			GameActions = new List<GameAction>() { 
        //				new GameAction() { Name = "action1", Command = typeof (TestCommand) },
        //				new GameAction() { Name = "action2", Command = typeof (TestCommand) } 
        //			} 
        //		});
    }

    public void RegisterTrigger(XGameTrigger trigger) {
        //if (!Triggers.ContainsKey(trigger.GameEventString())) {
        //    Triggers[trigger.GameEventString()] = new List<GameTrigger>();
        //}
        //Triggers[trigger.GameEventString()].Add(trigger);
    }

    public List<XGameTrigger> GetAllTriggers() {
        List<XGameTrigger> result = new List<XGameTrigger>();
        foreach (IList<XGameTrigger> gts in Triggers.Values) {
            result.AddRange(gts);
        }
        return result;
    }

}
