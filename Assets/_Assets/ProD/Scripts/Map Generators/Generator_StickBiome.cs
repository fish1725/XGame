using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ProD
{
	public static class Generator_StickBiome
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
		private static string type_Abyss   		 = "Abyss";
		private static string type_Path   		 = "Path";
		private static string type_Wall  		 = "Wall";
		private static int    frame_Size 		 = 1;
		private static int    repeat_0     		 = 50;
		
		//This method is only here for runtime alteration, mainly for debug testing.
		public static void SetSpecificProperties(string type_Abyss_, string type_Path_, string type_Wall_,
			int conversion_Density_, int frame_Size_, int repeat_0_, int repeat_1_)
		{
			type_Abyss   		= type_Abyss_;
			type_Path   		= type_Path_;
			type_Wall  		 	= type_Wall_;
			frame_Size 			= frame_Size_;
			repeat_0       		= repeat_0_;
			
		}
		
		//Generate() has the actual methods that generate the map data. 
		//You may customize this method with MethodLibrary methods.
		public static Map Generate()
		{
			PrepareMap();
			MethodLibrary.AddNoise_III(map, type_Wall, repeat_0, 0);
			MethodLibrary.Elongate(map, type_Wall, 3, 4, true);
			MethodLibrary.SetCellsOfTypeAToB(map,type_Abyss, type_Path);
			MethodLibrary.FrameMap(map,type_Wall, frame_Size);
			//MethodLibrary.Elongate(map, type_Wall, 2, 7, false);
			
			return map;
		}
		#endregion USER DEFINED DEFAULT CONTENT
	}
}