#region

using System;
using System.Linq.Expressions;
using System.Reflection;
using Assets._Scripts.XGameMVC;

#endregion

namespace Assets._Scripts.XGameTrigger.XGameExpression {
    public class XGameMethodCallExpression : XGameExpression {
        #region Fields

        private MethodInfo _method;

        #endregion

        #region C'tors

        public XGameMethodCallExpression() {
        }

        public XGameMethodCallExpression(XGameExpression instance, string methodName,
            XGameExpression[] arguments)
            : base(XGameExpressionType.Call, typeof (object)) {
            this.instance = instance;
            this.methodName = methodName;
            this.arguments = arguments;
        }

        #endregion

        #region Instance Properties

        public override Expression result {
            get {
                Expression[] exps = new Expression[arguments.Length];
                for (int i = 0; i < arguments.Length; i++) {
                    exps[i] = arguments[i].result;
                }
                if (instance == null) return Expression.Call(_method, exps);
                return Expression.Call(instance.result, _method, exps);
            }
        }

        public XGameExpression[] arguments { get; set; }
        public XGameExpression instance { get; set; }

        public string methodName {
            get { return _method.Name; }
            set {
                if (instance != null) {
                    _method = instance.type.GetMethod(value);
                }
                else {
                    Type superClassType = typeof (XGameController);
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
                type = _method.ReturnType;
            }
        }

        #endregion

        #region Instance Methods

        //public override string ToString() {
        //    object[] attrs = _method.GetCustomAttributes(false);
        //    XGameFunctionDescriptionAttribute attribute = attrs[0] as XGameFunctionDescriptionAttribute;
        //    if (attribute != null)
        //        return string.Format(attribute.name, arguments);
        //}

        #endregion
    }
}