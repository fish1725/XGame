using UnityEngine;
using System.Collections;

namespace ProD
{
	public static class Generator_Cavern
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
	
	//These variables are here for runtime alteration, mainly for debug testing.
	private static string type_Abyss   = "Abyss";
	private static string type_Path    = "Path";
	private static string type_Wall    = "Wall";
	private static int    thickness    = 2;
	private static int    repeat_0     		 = 300;
	private static int    repeat_1     		 = 8;
	private static int    conversion_Density = 4;
	private static int    frame_Size 		 = 1;
	
	//This method is only here for runtime alteration, mainly for debug testing.
	public static void SetSpecificProperties(string type_Abyss_, string type_Path_, string type_Wall_,
		int thickness_, int repeat_0_ , int repeat_1_, int conversion_Density_, int frame_Size_)
	{
		type_Abyss   	   = type_Abyss_;
		type_Path    	   = type_Path_;
		type_Wall    	   = type_Wall_;
		thickness    	   = thickness_;
		repeat_0     	   = repeat_0_;
		repeat_1   		   = repeat_1_;
		conversion_Density = conversion_Density_;
		frame_Size 		   = frame_Size_;
	}
	
	//Generate() has the actual methods that generate the map data. 
	//You may customize this method with MethodLibrary methods.
	public static Map Generate()
	{
		PrepareMap();
		MethodLibrary.FrameMap(map, type_Wall, thickness);
		MethodLibrary.AddNoise_I(map, type_Wall, repeat_0);
		MethodLibrary.FilterNoise(map, type_Wall, conversion_Density, frame_Size, repeat_1);
		MethodLibrary.SetCellsOfTypeAToB(map, type_Abyss, type_Path);
		MethodLibrary.FilterNoise(map, type_Path, conversion_Density, frame_Size, repeat_1);
		MethodLibrary.ConnectIsolatedAreas(map, type_Path, true, 1);
		return map;
	}
	#endregion USER DEFINED DEFAULT CONTENT

}
}