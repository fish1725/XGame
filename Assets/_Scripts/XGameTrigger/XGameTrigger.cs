using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public class XGameTrigger {

    private List<XGameEvent> _gameEvents = null;
    private List<XGameCondition> _gameConditions = null;
    private List<XGameAction> _gameActions = null;
    private Func<bool> _gameConditionCompiled = null;

    public List<XGameEvent> gameEvents {
        get { return _gameEvents; }
        set { _gameEvents = value; }
    }

    public List<XGameCondition> gameConditions {
        get { return _gameConditions; }
        set {
            _gameConditions = value;
            CompileGameCondition();
        }
    }

    public List<XGameAction> gameActions {
        get {
            return _gameActions;
        }
        set {
            _gameActions = value;
        }
    }

    public void Execute(XGameEvent e) {
        if (_gameConditionCompiled == null) {
            CompileGameCondition();
        }
        if (_gameConditionCompiled()) {
            foreach (XGameAction action in gameActions) {
                action.Execute(e);
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
}
