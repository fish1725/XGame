using System.Reflection;
using System.Linq.Expressions;

public class GameMethodCallExpression : GameExpression {

	[System.Xml.Serialization.XmlIgnore]
	public override System.Linq.Expressions.Expression Result {
		get {
			Expression[] exps = new Expression[Arguments.Length];
			for (int i = 0; i < Arguments.Length; i++) {
				exps[i] = Arguments[i].Result;
			}
			if (Instance == null) return Expression.Call (method, exps);
			else return Expression.Call (Instance.Result, method, exps);
		}
	}

	public GameExpression[] Arguments { get; set; }

	public string MethodName { 
		get {
			return method.Name;
		} 
		set {			
			method = typeof (GameFunctionProxy).GetMethod (value);
			this.Type = method.ReturnType;
		}
	}

	public GameExpression Instance { get; set; }

	private MethodInfo method;

	public GameMethodCallExpression () {

	}

	public GameMethodCallExpression (GameExpression instance, string methodName, GameExpression[] arguments) : base (GameExpressionType.Call, typeof(object)) {
		this.Instance = instance;
		this.MethodName = methodName;
		this.Arguments = arguments;
	}

	public override string ToString () {
		object[] attrs = method.GetCustomAttributes (false);
		return string.Format ((attrs [0] as GameFunctionDescriptionAttribute).Name, Arguments);
	}
}
