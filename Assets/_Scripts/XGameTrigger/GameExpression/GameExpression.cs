using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;
using UnityEngine;

[System.Xml.Serialization.XmlInclude(typeof(GameCondition))]
[System.Xml.Serialization.XmlInclude(typeof(GameConstantExpression))]
[System.Xml.Serialization.XmlInclude(typeof(GameMethodCallExpression))]
public abstract class GameExpression {
	public GameExpressionType NodeType { get; set; }

	[System.Xml.Serialization.XmlIgnore]
	public Type Type { get; set; }

	[System.Xml.Serialization.XmlIgnore]
	public abstract Expression Result { get; }

	protected GameExpression () {
		this.NodeType = GameExpressionType.Null;
		this.Type = typeof(object);
	}

	protected GameExpression (GameExpressionType nodeType, Type type) {
		this.NodeType = nodeType;
		this.Type = type;
	}

	public static GameMethodCallExpression Call (string methodName, GameExpression[] arguments) {
		return GameExpression.Call (null, methodName, arguments);
	}

	public static GameMethodCallExpression Call (GameExpression instance, string methodName, GameExpression[] arguments) {
		return new GameMethodCallExpression (instance, methodName, arguments);
	}

	public static GameConstantExpression Constant (object value) {
		return new GameConstantExpression (value);
	}

	public static GameConstantExpression Constant (object value, Type type) {
		return new GameConstantExpression (value, type);
	}

}

