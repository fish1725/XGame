using UnityEngine;
using System.Collections;
using System.Linq.Expressions;
using System;

public class XGameCondition : XGameExpression {
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

    public XGameExpression left { get; set; }
    public XGameExpression right { get; set; }
    public XGameConditionOperator oper {
        get {
            return (XGameConditionOperator)Enum.Parse(typeof(XGameConditionOperator), this.nodeType.ToString());
        }
        set {
            this.nodeType = (XGameExpressionType)(Enum.Parse(typeof(XGameExpressionType), value.ToString()));
        }
    }

    public XGameCondition() {
    }

    private static XGameCondition GameConditionBinaryComparison(XGameExpression left, XGameExpression right, XGameConditionOperator op) {
        XGameCondition gc = new XGameCondition() {
            left = left,
            right = right,
            oper = op
        };
        return gc;
    }

    public static XGameCondition BooleanComparison(XGameExpression left, XGameExpression right, XGameConditionOperator op) {
        return GameConditionBinaryComparison(left, right, op);
    }
}