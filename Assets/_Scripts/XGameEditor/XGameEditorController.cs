using System;
using System.Collections.Generic;

public class XGameEditorController : XGameController {

    public XGameEditorModel CreateEditor() {
        XGameEditorModel editor = new XGameEditorModel();
        return editor;
    }

    public void InitTriggers(XGameEditorModel editor) {
        // triggers
        XGameAction action = XGameAction.CreateAction(XGameExpression.Constant(XGame.Resolve<XGameCharacterController>()), "SetTest", new XGameExpression[] { XGameExpression.Constant(100) });
        XGameCondition condition = XGameCondition.BooleanComparison(XGameExpression.Constant(2), XGameExpression.Constant(1), XGameConditionOperator.GreaterThan);
        XGameCondition condition2 = XGameCondition.BooleanComparison(XGameExpression.Constant(3), XGameExpression.Constant(3), XGameConditionOperator.GreaterThanOrEqual);
        XGameTrigger trigger = XGame.Resolve<XGameTriggerController>().CreateTrigger("trigger", new List<XGameEvent> { new XGameEvent(XGameEventType.Character_Created) }, new List<XGameCondition> { condition, condition2 }, new List<XGameAction> { action });

        List<XGameTrigger> triggers = new List<XGameTrigger>();
        for (int i = 0; i < 10; i++) {
            triggers.Add(trigger);
        }
        editor.triggers = triggers;
    }

    public XGameWindowModel CreateWindow(XGameEditorModel editor) {
        XGameWindowModel window = XGame.Resolve<XGameWindowController>().CreateWindow();
        editor.AddWindow(window);
        return window;
    }

    public void RemoveWindow(XGameEditorModel editor, XGameWindowModel window) {
        editor.RemoveWindow(window);
    }

    public XGameWindowModel GetWindowByID(XGameEditorModel editor, Guid id) {
        return editor.windows.Find((model) => { return model.id == id; });
    }

}
