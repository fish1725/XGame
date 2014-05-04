using System;
using System.Linq.Expressions;
using System.Xml.Serialization;

[System.Xml.Serialization.XmlInclude(typeof(XGameCondition))]
[System.Xml.Serialization.XmlInclude(typeof(XGameConstantExpression))]
[System.Xml.Serialization.XmlInclude(typeof(XGameMethodCallExpression))]
public abstract class XGameExpression {

    [XmlAttribute(AttributeName = "nodeType")]
    public XGameExpressionType nodeType { get; set; }

    [System.Xml.Serialization.XmlIgnore]
    public Type type { get; set; }

    [System.Xml.Serialization.XmlIgnore]
    public abstract Expression result { get; }

    protected XGameExpression() {
        this.nodeType = XGameExpressionType.Null;
        this.type = typeof(object);
    }

    protected XGameExpression(XGameExpressionType nodeType, Type type) {
        this.nodeType = nodeType;
        this.type = type;
    }

    public static XGameMethodCallExpression Call(string methodName, XGameExpression[] arguments) {
        return XGameExpression.Call(null, methodName, arguments);
    }

    public static XGameMethodCallExpression Call(XGameExpression instance, string methodName, XGameExpression[] arguments) {
        return new XGameMethodCallExpression(instance, methodName, arguments);
    }

    public static XGameConstantExpression Constant(object value) {
        return new XGameConstantExpression(value);
    }

    public static XGameConstantExpression Constant(object value, Type type) {
        return new XGameConstantExpression(value, type);
    }

}

