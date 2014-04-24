using UnityEngine;
using System.Collections;

namespace ProD
{
	public static class Generator_DungeonRuins
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
		//private static string theme = "Stone Dungeon Theme";
		private static string theme = "Terminal Theme";
		
		
		//These variables are here for runtime alteration, mainly for debug testing.
		private static string type_Abyss   = "Abyss";
		private static string type_Path    = "Path";
		private static string type_Wall    = "Wall";
		private static int    room_Min_X   = 3;
		private static int    room_Max_X   = 11;
		private static int    room_Min_Y   = 3;
		private static int    room_Max_Y   = 11;
		private static int    room_Freq    = 10;
		private static int    room_Retry   = 6;
		private static bool   create_Doors = true;
		private static int    repeat       = 12;
		
		//This method is only here for runtime alteration, mainly for debug testing.
		public static void SetSpecificProperties(string type_Abyss_, string type_Path_, string type_Wall_,
			int room_Min_X_, int room_Max_X_, int room_Min_Y_, int room_Max_Y_, int room_Freq_, int room_Retry_,
			bool create_Doors_, int repeat_)
		{
			type_Abyss   = type_Abyss_;
			type_Path    = type_Path_;
			type_Wall    = type_Wall_;
			room_Min_X   = room_Min_X_;
			room_Max_X   = room_Max_X_;
			room_Min_Y   = room_Min_Y_;
			room_Max_Y   = room_Max_Y_;
			room_Freq    = room_Freq_;
			room_Retry   = room_Retry_;
			create_Doors = create_Doors_;
			repeat       = repeat_;
		}
		
		//Generate() has the actual methods that generate the map data. 
		//You may customize this method with MethodLibrary methods.
		public static Map Generate()
		{
			PrepareMap();
			MethodLibrary.CreateRooms(map, type_Wall, type_Path, room_Min_X, room_Max_X, room_Min_Y, room_Max_Y,
				room_Freq, room_Retry, create_Doors);
			
			//MethodLibrary.CreateDoors(type_Path, map, 1);
			MethodLibrary.CreateMaze(map, type_Path, type_Abyss);
			MethodLibrary.SetCellsOfTypeAToB(map, type_Abyss, type_Wall);
			MethodLibrary.CloseDeadEndCells(map, type_Wall, type_Path);
			MethodLibrary.ReduceUCorridors(map, type_Wall, type_Path, repeat);
			MethodLibrary.ConvertUnreachableCells(map, type_Wall, type_Abyss);
			
			MethodLibrary.AddNoise_I(map, type_Path, 300);
			MethodLibrary.FrameMap(map,type_Wall, 1);
			//MethodLibrary.EnwallCells(map, type_Path, type_Wall);
			MethodLibrary.SetCellsOfTypeAToB(map, type_Abyss, type_Wall);
			MethodLibrary.RemoveSmallRooms(map, type_Path, type_Wall,6);
			MethodLibrary.RemoveSmallRooms(map, type_Abyss, type_Wall,-1);
			MethodLibrary.ConvertUnreachableCells(map, type_Wall, type_Abyss);
			//MethodLibrary.FrameMap(map,type_Wall, 1);
			return map;
		}
		#endregion USER DEFINED DEFAULT CONTENT
	}
}