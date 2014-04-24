using UnityEngine;
using System.Collections;
using System;
using System.Linq.Expressions;

public class GameConstantExpression : GameExpression {

	[System.Xml.Serialization.XmlIgnore]
	public override Expression Result {
		get {
			return Expression.Constant (Value, this.Type);
		}
	}

	private object value;
	public object Value { 
		get {
			return this.value;
		}
		set {
			this.value = value;
			this.Type = (value != null) ? value.GetType () : typeof(object);
		}
	}

	public GameConstantExpression () : base() {
		this.Value = null;
	}

	public GameConstantExpression (object value) : base (GameExpressionType.Constant, (value != null) ? value.GetType () : typeof(object)) {
		this.Value = value;
	}

	public GameConstantExpression (object value, Type type) : base (GameExpressionType.Constant, type) {
		this.Value = value;
	}

}
