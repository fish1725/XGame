#region

using System;
using System.Collections.Generic;
using Assets._Scripts.UI.Window;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameTrigger;
using Assets._Scripts.XGameTrigger.XGameAction;
using Assets._Scripts.XGameTrigger.XGameCondition;
using Assets._Scripts.XGameTrigger.XGameEvent;
using Assets._Scripts.XGameTrigger.XGameExpression;
using Assets._Scripts.XGameUnit;

#endregion

namespace Assets._Scripts.XGameEditor {
    public class XGameEditorController : XGameController {
        #region Instance Methods

        public XGameEditorModel CreateEditor() {
            XGameEditorModel editor = new XGameEditorModel();
            return editor;
        }

        public XGameWindowModel CreateWindow(XGameEditorModel editor) {
            XGameWindowModel window = XGame.Resolve<XGameWindowController>().CreateWindow();
            editor.AddWindow(window);
            return window;
        }

        public XGameWindowModel GetWindowByID(XGameEditorModel editor, Guid id) {
            return editor.windows.Find(model => model.id == id);
        }

        public void InitTriggers(XGameEditorModel editor) {
            // triggers
            XGameAction action =
                XGameAction.CreateAction(XGameExpression.Constant(XGame.Resolve<XGameCharacterController>()), "SetTest",
                    new XGameExpression[] {XGameExpression.Constant(100)});
            XGameCondition condition = XGameCondition.BooleanComparison(XGameExpression.Constant(2),
                XGameExpression.Constant(1), XGameConditionOperator.GreaterThan);
            XGameCondition condition2 = XGameCondition.BooleanComparison(XGameExpression.Constant(3),
                XGameExpression.Constant(3), XGameConditionOperator.GreaterThanOrEqual);

            List<XGameTriggerModel> triggers = new List<XGameTriggerModel>();
            for (int i = 0; i < 10; i++) {
                XGameTriggerModel trigger = XGame.Resolve<XGameTriggerController>()
                    .CreateTrigger("trigger" + i,
                        new List<XGameEvent> {new XGameEvent(XGameEventType.Character_Created)},
                        new List<XGameCondition> {condition, condition2}, new List<XGameAction> {action});
                triggers.Add(trigger);
            }
            editor.triggers = triggers;
        }

        public void RemoveWindow(XGameEditorModel editor, XGameWindowModel window) {
            editor.RemoveWindow(window);
        }

        #endregion
    }
}