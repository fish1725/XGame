using UnityEngine;
using System.Collections;

namespace ProD
{
	public static class Generator_EmptyMap
{
		#region PROD DEFINED DEFAULT CONTENT
	private static Map map;
	private static void PrepareMap()
	{
		map = new Map(map_Size_X, map_Size_Y);
		map.theme = theme;
	}
	public static void SetGenericProperties(int map_Size_X_, int map_Size_Y_, string theme_)
	{
		map_Size_X = map_Size_X_;
		map_Size_Y = map_Size_Y_;
		theme      = theme_;
	}
	#endregion PROD DEFINED DEFAULT CONTENT
	
	#region USER DEFINED DEFAULT CONTENT
	//You should set all these variables to your liking.
	private static int map_Size_X = 29;
	private static int map_Size_Y = 29;
	private static string theme = "Terminal Theme";
	
	//This method is only here for runtime alteration, mainly for debug testing.
	public static void SetSpecificProperties()
	{

	}
	
	//Generate() has the actual methods that generate the map data. 
	//You may customize this method with MethodLibrary methods.
	public static Map Generate()
	{
		PrepareMap();
		return map;
	}
	#endregion USER DEFINED DEFAULT CONTENT

}
}

