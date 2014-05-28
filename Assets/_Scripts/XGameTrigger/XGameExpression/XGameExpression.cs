#region

using System;
using System.Linq.Expressions;
using System.Xml.Serialization;

#endregion

namespace Assets._Scripts.XGameTrigger.XGameExpression {
    [XmlInclude(typeof (XGameCondition.XGameCondition))]
    [XmlInclude(typeof (XGameConstantExpression))]
    [XmlInclude(typeof (XGameMethodCallExpression))]
    public abstract class XGameExpression {
        #region C'tors

        protected XGameExpression() {
            nodeType = XGameExpressionType.Null;
            type = typeof (object);
        }

        protected XGameExpression(XGameExpressionType nodeType, Type type) {
            this.nodeType = nodeType;
            this.type = type;
        }

        #endregion

        #region Instance Properties

        [XmlIgnore]
        public abstract Expression result { get; }

        [XmlAttribute(AttributeName = "nodeType")]
        public XGameExpressionType nodeType { get; set; }

        [XmlIgnore]
        public Type type { get; set; }

        #endregion

        #region Class Methods

        public static XGameMethodCallExpression Call(string methodName, XGameExpression[] arguments) {
            return Call(null, methodName, arguments);
        }

        public static XGameMethodCallExpression Call(XGameExpression instance, string methodName,
            XGameExpression[] arguments) {
            return new XGameMethodCallExpression(instance, methodName, arguments);
        }

        public static XGameConstantExpression Constant(object value) {
            return new XGameConstantExpression(value);
        }

        public static XGameConstantExpression Constant(object value, Type type) {
            return new XGameConstantExpression(value, type);
        }

        #endregion
    }
}