#region

using System;

#endregion

namespace Assets._Scripts.XGameUtil {
    public class XGameUtil {
        #region Class Methods

        public static string GetTypeName(Type t) {
            string name = t.Name;
            if (name.IndexOf("Int", StringComparison.Ordinal) >= 0) {
                name = "Int";
            }
            else if (name.IndexOf("Single", StringComparison.Ordinal) >= 0) {
                name = "Float";
            }
            else if (name.IndexOf("String", StringComparison.Ordinal) >= 0) {
                name = "String";
            }
            else if (name.IndexOf("List", StringComparison.Ordinal) >= 0) {
                name = "List";
            }
            return name;
        }

        #endregion
    }
}