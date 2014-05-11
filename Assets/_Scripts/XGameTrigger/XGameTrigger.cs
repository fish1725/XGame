using System;
using System.Linq.Expressions;

public class XGameTrigger {

    private string _name = null;
    private XGameEvent[] _gameEvents = null;
    private XGameCondition[] _gameConditions = null;
    private XGameAction[] _gameActions = null;
    private Func<bool> _gameConditionCompiled = null;
    
    public string name {
        get { return _name; }
        set { _name = value; }
    }

    public XGameEvent[] gameEvents {
        get { return _gameEvents; }
        set { _gameEvents = value; }
    }

    public XGameCondition[] gameConditions {
        get { return _gameConditions; }
        set {
            _gameConditions = value;
            CompileGameCondition();
        }
    }

    public XGameAction[] gameActions {
        get {
            return _gameActions;
        }
        set {
            _gameActions = value;
        }
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
        for (int i = 0; i < gameConditions.Length; i++) {
            re = Expression.And(re, gameConditions[i].result);
        }
        _gameConditionCompiled = Expression.Lambda<Func<bool>>(re).Compile();
    }
}
