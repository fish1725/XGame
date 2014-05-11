using System.Collections.Generic;

public class XGameEditorController : XGameController {
    private XGameWindowController _windowController = new XGameWindowController();
    private XGameTriggerController _triggerController = new XGameTriggerController();
    private XGameCharacterController _characterController = new XGameCharacterController();

    public XGameWindowController windowController {
        get { return _windowController; }
        set { _windowController = value; }
    }

    public XGameEditorModel CreateEditor() {
        XGameEditorModel editor = new XGameEditorModel();
        return editor;
    }

    public void InitTriggers(XGameEditorModel editor) {
        // triggers
        XGameAction action = XGameAction.CreateAction(XGameExpression.Constant(_characterController), "SetTest", new XGameExpression[] { XGameExpression.Constant(100) });
        XGameCondition condition = XGameCondition.BooleanComparison(XGameExpression.Constant(2), XGameExpression.Constant(1), XGameConditionOperator.GreaterThan);
        XGameCondition condition2 = XGameCondition.BooleanComparison(XGameExpression.Constant(3), XGameExpression.Constant(3), XGameConditionOperator.GreaterThanOrEqual);
        XGameTrigger trigger = _triggerController.CreateTrigger("trigger", new XGameEvent[] { new XGameEvent(XGameEventType.Character_Created) }, new XGameCondition[] { condition, condition2 }, new XGameAction[] { action });

        List<XGameTrigger> triggers = new List<XGameTrigger>();
        for (int i = 0; i < 10; i++) {
            triggers.Add(trigger);
        }
        editor.triggers = triggers;
    }

    public XGameWindowModel CreateWindow(XGameEditorModel editor) {
        XGameWindowModel window = _windowController.CreateWindow();
        editor.AddWindow(window);
        return window;
    }

}
