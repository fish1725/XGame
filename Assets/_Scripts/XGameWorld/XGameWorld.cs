using UnityEngine;
using System.Collections;

public class XGameWorld : XGame {
    private XGameWorldController _worldController = null;
    private XGameTriggerController _triggerController = null;

    public override void Setup() {
        base.Setup();
        _worldController = CreateController<XGameWorldController>();
        _triggerController = CreateController<XGameTriggerController>();

        // triggers
        XGameAction action = XGameAction.Call(null, "ActionTest", new XGameExpression[] { XGameExpression.Constant(100) });
        XGameCondition condition = XGameCondition.BooleanComparison(XGameExpression.Constant(2), XGameExpression.Constant(1), XGameConditionOperator.GreaterThan);
        XGameCondition condition2 = XGameCondition.BooleanComparison(XGameExpression.Constant(3), XGameExpression.Constant(3), XGameConditionOperator.GreaterThan);
        Debug.Log(action);
        XGameTrigger trigger = _triggerController.CreateTrigger(new XGameEvent[] { new XGameEvent(XGameEventType.Character_Created) }, new XGameCondition[] { condition, condition2 }, new XGameAction[] { action });

        _triggerController.RegisterTrigger(trigger);

        // world
        XGameWorldModel world = _worldController.CreateWorld();

        XGameWorldView view = CreateView<XGameWorldView>(world);
        view.transform.parent = transform;

        _worldController.InitWorldMap(world);
        _worldController.InitCharacters(world);

        RegisterInstance<XGameWorldModel>(world);

    }
}
