
using System;
using System.Linq.Expressions;
public class XGameAction : XGameMethodCallExpression {

    private Action _resultCompiled = null;

    public XGameAction (XGameExpression instance, string methodName, XGameExpression[] arguments) : base(instance, methodName, arguments) {

    }

    public void Execute() {
        if (_resultCompiled == null) {
            _resultCompiled = Expression.Lambda<Action>(result).Compile();
        }
        _resultCompiled();
    }

    public static XGameAction Call(XGameExpression instance, string methodName, XGameExpression[] arguments) {
        return new XGameAction(instance, methodName, arguments);
    }

}
