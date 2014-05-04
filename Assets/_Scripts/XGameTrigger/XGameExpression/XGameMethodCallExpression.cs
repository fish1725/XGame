using System.Reflection;
using System.Linq.Expressions;
using System;

public class XGameMethodCallExpression : XGameExpression {

    public override System.Linq.Expressions.Expression result {
        get {
            Expression[] exps = new Expression[arguments.Length];
            for (int i = 0; i < arguments.Length; i++) {
                exps[i] = arguments[i].result;
            }
            if (instance == null) return Expression.Call(_method, exps);
            else return Expression.Call(instance.result, _method, exps);
        }
    }

    public XGameExpression[] arguments { get; set; }

    public string methodName {
        get {
            return _method.Name;
        }
        set {
            if (instance != null) {
                _method = instance.type.GetMethod(value);
            } else {
                Type superClassType = typeof(XGameController);
                Assembly a = Assembly.GetAssembly(superClassType);
                Type[] types = a.GetTypes();
                foreach (Type t in types) {
                    if (t.IsClass && t.IsSubclassOf(superClassType)) {
                        if (t.GetMethod(value) != null) {
                            _method = t.GetMethod(value);
                            break;
                        }
                    }
                }
            }
            if (_method == null) {
                throw new Exception("method '" + value + "' is not found!");
            }
            this.type = _method.ReturnType;
        }
    }

    public XGameExpression instance { get; set; }

    private MethodInfo _method;

    public XGameMethodCallExpression() {

    }

    public XGameMethodCallExpression(XGameExpression instance, string methodName, XGameExpression[] arguments)
        : base(XGameExpressionType.Call, typeof(object)) {
        this.instance = instance;
        this.methodName = methodName;
        this.arguments = arguments;
    }

    public override string ToString() {
        object[] attrs = _method.GetCustomAttributes(false);
        return string.Format((attrs[0] as XGameFunctionDescriptionAttribute).name, arguments);
    }
}
