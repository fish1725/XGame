using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System;

public class XGameWorld : XGame {
    private XGameWorldController _worldController = null;
    private XGameTriggerController _triggerController = null;

    public override void Setup() {
        base.Setup();
        _worldController = CreateController<XGameWorldController>();
        _triggerController = CreateController<XGameTriggerController>();

        // triggers
        XGameAction action = XGameAction.CreateAction(XGameExpression.Constant(_worldController.characterController), "SetTest", new XGameExpression[] { XGameExpression.Constant(100) });
        XGameCondition condition = XGameCondition.BooleanComparison(XGameExpression.Constant(2), XGameExpression.Constant(1), XGameConditionOperator.GreaterThan);
        XGameCondition condition2 = XGameCondition.BooleanComparison(XGameExpression.Constant(3), XGameExpression.Constant(3), XGameConditionOperator.GreaterThanOrEqual);
        XGameTrigger trigger = _triggerController.CreateTrigger(new XGameEvent[] { new XGameEvent(XGameEventType.Character_Created) }, new XGameCondition[] { condition, condition2 }, new XGameAction[] { action });

        Type[] types = { typeof(XGameController) };
        XmlSerializer serializer = new XmlSerializer(typeof(XGameTrigger), types);
        StringWriter sw = new StringWriter();
        serializer.Serialize(sw, trigger);
        StringReader sr = new StringReader(sw.ToString());
        Debug.Log(sw.ToString());
        sw = new StringWriter();
        XGameTrigger t2 = serializer.Deserialize(sr) as XGameTrigger;
        serializer.Serialize(sw, t2);
        Debug.Log(sw.ToString());

        _triggerController.RegisterTrigger(t2);

        // world
        XGameWorldModel world = _worldController.CreateWorld();

        XGameWorldView view = CreateView<XGameWorldView, XGameWorldModel>(world);
        view.transform.parent = transform;

        _worldController.InitWorldMap(world);
        _worldController.InitCharacters(world);

        RegisterInstance<XGameWorldModel>(world);

    }
}
