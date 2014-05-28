#region

using System;
using System.Linq.Expressions;
using Assets._Scripts.XGameTrigger.XGameExpression;

#endregion

namespace Assets._Scripts.XGameTrigger.XGameAction {
    public class XGameAction : XGameMethodCallExpression {
        #region Fields

        private Action _resultCompiled;

        #endregion

        #region C'tors

        public XGameAction() {
        }

        public XGameAction(XGameExpression.XGameExpression instance, string methodName,
            XGameExpression.XGameExpression[] arguments)
            : base(instance, methodName, arguments) {
        }

        #endregion

        #region Instance Methods

        public void Execute() {
            if (_resultCompiled == null) {
                _resultCompiled = Expression.Lambda<Action>(result).Compile();
            }
            _resultCompiled();
        }

        #endregion

        #region Class Methods

        public static XGameAction CreateAction(XGameExpression.XGameExpression instance, string methodName,
            XGameExpression.XGameExpression[] arguments) {
            return new XGameAction(instance, methodName, arguments);
        }

        #endregion
    }
}