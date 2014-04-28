using UnityEngine;
using System.Collections;
using System;
using System.Linq.Expressions;

public class XGameConstantExpression : XGameExpression {

    [System.Xml.Serialization.XmlIgnore]
    public override Expression result {
        get {
            return Expression.Constant(value, this.type);
        }
    }

    private object _value;
    public object value {
        get {
            return this._value;
        }
        set {
            this._value = value;
            this.type = (value != null) ? value.GetType() : typeof(object);
        }
    }

    public XGameConstantExpression()
        : base() {
        this.value = null;
    }

    public XGameConstantExpression(object value)
        : base(XGameExpressionType.Constant, (value != null) ? value.GetType() : typeof(object)) {
        this.value = value;
    }

    public XGameConstantExpression(object value, Type type)
        : base(XGameExpressionType.Constant, type) {
        this.value = value;
    }

}
