using System;
using System.Collections;
using UnityEngine;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
public class XGameFunctionAttribute : Attribute {

}

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
public class XGameFunctionDescriptionAttribute : Attribute {

    private string _name;

    public XGameFunctionDescriptionAttribute(string name) {
        this._name = name;
    }

    public string name { get { return _name; } }
}

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
public class GameFunctionTypeAttribute : Attribute {

    private Type _name;

    public GameFunctionTypeAttribute(Type name) {
        this._name = name;
    }

    public Type name { get { return _name; } }
}