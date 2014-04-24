using System;
using System.Collections;
using UnityEngine;

[AttributeUsage(AttributeTargets.All,AllowMultiple = true,Inherited = true)]  
public class GameFunctionAttribute : Attribute {

}

[AttributeUsage(AttributeTargets.All,AllowMultiple = true,Inherited = true)]  
public class GameFunctionDescriptionAttribute : Attribute {
	
	private string name;
	
	public GameFunctionDescriptionAttribute (string name) {
		this.name = name;
	}
	
	public string Name { get { return name; } }
}

[AttributeUsage(AttributeTargets.All,AllowMultiple = true,Inherited = true)]  
public class GameFunctionTypeAttribute : Attribute {
	
	private Type name;
	
	public GameFunctionTypeAttribute (Type name) {
		this.name = name;
	}
	
	public Type Name { get { return name; } }
}