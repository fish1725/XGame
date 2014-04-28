
using System;
using System.Linq.Expressions;
public class XGameAction : XGameMethodCallExpression {

    private Action<XGameEvent> _resultCompiled = null;

    public void Execute(XGameEvent e) {
        if (_resultCompiled == null) {
            _resultCompiled = Expression.Lambda<Action<XGameEvent>>(result).Compile();
        }
        _resultCompiled(e);
    }

}
