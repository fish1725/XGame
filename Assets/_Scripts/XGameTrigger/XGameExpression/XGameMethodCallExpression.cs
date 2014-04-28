using System.Reflection;
using System.Linq.Expressions;

public class XGameMethodCallExpression : XGameExpression {

    [System.Xml.Serialization.XmlIgnore]
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
            _method = typeof(GameFunctionProxy).GetMethod(value);
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
