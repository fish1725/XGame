#region

using System;
using System.Linq.Expressions;

#endregion

namespace Assets._Scripts.XGameTrigger.XGameExpression {
    public class XGameConstantExpression : XGameExpression {
        #region Fields

        private object _value;

        #endregion

        #region C'tors

        public XGameConstantExpression() {
            value = null;
        }

        public XGameConstantExpression(object value)
            : base(XGameExpressionType.Constant, (value != null) ? value.GetType() : typeof (object)) {
            this.value = value;
        }

        public XGameConstantExpression(object value, Type type)
            : base(XGameExpressionType.Constant, type) {
            this.value = value;
        }

        #endregion

        #region Instance Properties

        [System.Xml.Serialization.XmlIgnore]
        public override Expression result {
            get { return Expression.Constant(value, type); }
        }

        public object value {
            get { return _value; }
            set {
                _value = value;
                type = (value != null) ? value.GetType() : typeof (object);
            }
        }

        #endregion
    }
}