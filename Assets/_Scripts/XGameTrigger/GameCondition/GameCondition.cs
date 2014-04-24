using UnityEngine;
using System.Collections;
using System.Linq.Expressions;
using System;

public class GameCondition : GameExpression {
	public override Expression Result {
		get {
			Expression exp = null;
			switch (Operator) {
			case GameConditionOperator.Equal:
				exp = Expression.Equal (Left.Result, Right.Result);
				break;
			case GameConditionOperator.NotEqual:
				exp = Expression.NotEqual (Left.Result, Right.Result);
				break;
			case GameConditionOperator.GreaterThan:
				exp = Expression.GreaterThan (Left.Result, Right.Result);
				break;
			case GameConditionOperator.GreaterThanOrEqual:
				exp = Expression.GreaterThanOrEqual (Left.Result, Right.Result);
				break;
			case GameConditionOperator.LessThan:
				exp = Expression.LessThan (Left.Result, Right.Result);
				break;
			case GameConditionOperator.LessThanOrEqual:
				exp = Expression.LessThanOrEqual (Left.Result, Right.Result);
				break;
			default:
				Debug.Log ("GameConditionOperator Error! " + Operator.ToString());
				break;
			}
			return exp;
		}
	}

	public GameExpression Left { get; set; }
	public GameExpression Right { get; set; }
	public GameConditionOperator Operator {
		get {
			return (GameConditionOperator)Enum.Parse(typeof(GameConditionOperator), this.NodeType.ToString());
		}
		set {
			this.NodeType = (GameExpressionType)(Enum.Parse(typeof(GameExpressionType), value.ToString()));
		}
	}

	public GameCondition() {
	}

	private static GameCondition GameConditionBinaryComparison (GameExpression left, GameExpression right, GameConditionOperator op) {
		GameCondition gc = new GameCondition() {
			Left = left,
			Right = right,
			Operator = op
		};
		return gc;
	}

	public static GameCondition BooleanComparison (GameExpression left, GameExpression right, GameConditionOperator op) {
		return GameConditionBinaryComparison (left, right, op);
	}
}