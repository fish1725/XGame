using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public class GameTrigger {

    private List<XGameEvent> _gameEvent = null;
    private List<GameCondition> _gameCondition = null;
    private List<GameAction> _gameActions = null;
    private Func<bool> _gameConditionCompiled = null;

    public List<XGameEvent> gameEvent {
        get { return _gameEvent; }
        set { _gameEvent = value; }
    }

    public List<GameCondition> gameCondition {
        get { return _gameCondition; }
        set {
            _gameCondition = value;
            CompileGameCondition();
        }
    }

    public List<GameAction> gameActions {
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
            foreach (GameAction action in gameActions) {
                action.Execute(e);
            }
        }
    }

    void CompileGameCondition() {
        Expression re = Expression.Constant(true);
        for (int i = 0; i < gameCondition.Count; i++) {
            re = Expression.And(re, gameCondition[i].Result);
        }
        _gameConditionCompiled = Expression.Lambda<Func<bool>>(re).Compile();
    }
}
