#region

using System;
using System.Linq.Expressions;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameTrigger.XGameCondition {
    public class XGameCondition : XGameExpression.XGameExpression {
        #region Instance Properties

        public override Expression result {
            get {
                Expression exp = null;
                switch (oper) {
                    case XGameConditionOperator.Equal:
                        exp = Expression.Equal(left.result, right.result);
                        break;
                    case XGameConditionOperator.NotEqual:
                        exp = Expression.NotEqual(left.result, right.result);
                        break;
                    case XGameConditionOperator.GreaterThan:
                        exp = Expression.GreaterThan(left.result, right.result);
                        break;
                    case XGameConditionOperator.GreaterThanOrEqual:
                        exp = Expression.GreaterThanOrEqual(left.result, right.result);
                        break;
                    case XGameConditionOperator.LessThan:
                        exp = Expression.LessThan(left.result, right.result);
                        break;
                    case XGameConditionOperator.LessThanOrEqual:
                        exp = Expression.LessThanOrEqual(left.result, right.result);
                        break;
                    default:
                        Debug.Log("GameConditionOperator Error! " + oper.ToString());
                        break;
                }
                return exp;
            }
        }

        public XGameExpression.XGameExpression left { get; set; }

        public XGameConditionOperator oper {
            get { return (XGameConditionOperator) Enum.Parse(typeof (XGameConditionOperator), nodeType.ToString()); }
            set { nodeType = (XGameExpressionType) (Enum.Parse(typeof (XGameExpressionType), value.ToString())); }
        }

        public XGameExpression.XGameExpression right { get; set; }

        #endregion

        #region Class Methods

        public static XGameCondition BooleanComparison(XGameExpression.XGameExpression left,
            XGameExpression.XGameExpression right, XGameConditionOperator op) {
            return GameConditionBinaryComparison(left, right, op);
        }

        private static XGameCondition GameConditionBinaryComparison(XGameExpression.XGameExpression left,
            XGameExpression.XGameExpression right, XGameConditionOperator op) {
            XGameCondition gc = new XGameCondition {
                left = left,
                right = right,
                oper = op
            };
            return gc;
        }

        #endregion
    }
}