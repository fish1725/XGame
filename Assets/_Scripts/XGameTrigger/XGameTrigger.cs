using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public class XGameTrigger : XGameModel {

    private Func<bool> _gameConditionCompiled = null;

    public List<XGameEvent> gameEvents {
        get { return Get("gameEvents") as List<XGameEvent>; }
        set { Set("gameEvents", value); }
    }

    public List<XGameCondition> gameConditions {
        get { return Get("gameConditions") as List<XGameCondition>; }
        set {
            Set("gameConditions", value);
            CompileGameCondition();
        }
    }

    public List<XGameAction> gameActions {
        get { return Get("gameActions") as List<XGameAction>; }
        set { Set("gameActions", value); }
    }

    public void Execute() {
        if (_gameConditionCompiled == null) {
            CompileGameCondition();
        }
        if (_gameConditionCompiled()) {
            foreach (XGameAction action in gameActions) {
                action.Execute();
            }
        }
    }

    void CompileGameCondition() {
        Expression re = Expression.Constant(true);
        for (int i = 0; i < gameConditions.Count; i++) {
            re = Expression.And(re, gameConditions[i].result);
        }
        _gameConditionCompiled = Expression.Lambda<Func<bool>>(re).Compile();
    }

    public float testint {
        get { return (float)Get("testint"); }
        set { Set("testint", value); }
    }
}
