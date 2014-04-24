using UnityEngine;
using System.Collections;

namespace ProD
{
	public class TestSceneGUI_3 : MonoBehaviour 
	{
		//These are the GUIs used in the Test Scenes
		private bool showGUI  = true;
		
		public Color guiColor;
		
		public float window_Start_X = 804;
		public float window_Start_Y = 52;
		public float window_Width = 200;
		public float window_Height = 370;
		
		public float label_Start_X = 25;
		public float label_Start_Y = 14;
		public float label_Width = 178;
		public float label_Height = 32;
		
		public float button_Width = 180f;
		public float button_Start_Y = 62;
		
		
		private WorldMap worldMap;
		public float worldMap_Size_X = 1f;
		public float worldMap_Size_Y = 1f;
		public float mapWidth = 33.0F;
		public float mapHeight = 33.0F;
	
		public WorldMap Generate( string generatorName )
		{
			worldMap =  Generator_Generic_World.Generate(generatorName, Mathf.FloorToInt(worldMap_Size_X), Mathf.FloorToInt(worldMap_Size_Y), Mathf.FloorToInt(mapWidth), Mathf.FloorToInt(mapHeight));
			Materializer.Instance.MaterializeWorldMap(worldMap);
			ProDManager.Instance.SpawnPlayer(worldMap);
			return worldMap;
		}
		
		void OnGUI () 
		{
			GUI.backgroundColor = guiColor;
			
			#region SHOW/HIDE
			GUI.backgroundColor = Color.red;
			if(GUI.Button(new Rect(window_Start_X + 20, 20 , button_Width , 16 ), "Hide/Show Panel")) 
				showGUI = !showGUI;
			
			if(!showGUI) return;
			GUI.backgroundColor = guiColor;
			#endregion
			
			GUI.Box(new Rect(window_Start_X + 10,window_Start_Y,window_Width,window_Height),"");
			
			GUI.Label(new Rect(window_Start_X + label_Start_X,window_Start_Y + label_Start_Y,label_Width,492), 
				"Press a button to preview a map of 29 by 29 cells." 
				+ "\n" 
				+ "\n" + "Drag via LMB & Zoom via MS"
				+ "\n"
				+ "\n" + "Move Player via Numpad"
				
				);
			
			if(GUI.Button(new Rect(window_Start_X + 20,window_Start_Y + button_Start_Y+(30*2),button_Width,20), "Cavern")) 
			{
				Generate("Cavern");
			}
			if(GUI.Button(new Rect(window_Start_X + 20,window_Start_Y + button_Start_Y+(30*3),button_Width,20), "Dungeon")) 
			{
				Generate("Dungeon");	
			}
			if(GUI.Button(new Rect(window_Start_X + 20,window_Start_Y + button_Start_Y+(30*4),button_Width,20), "DungeonRuins")) 
			{
				Generate("DungeonRuins");	
			}
			if(GUI.Button(new Rect(window_Start_X + 20,window_Start_Y + button_Start_Y+(30*5),button_Width,20), "Rocky Hill")) 
			{
				Generate("RockyHill");
			}
			if(GUI.Button(new Rect(window_Start_X + 20,window_Start_Y + button_Start_Y+(30*6),button_Width,20), "Maze")) 
			{
				Generate("Maze");	
			}
			if(GUI.Button(new Rect(window_Start_X + 20,window_Start_Y + button_Start_Y+(30*7),button_Width,20), "PerlinLikeBiome")) 
			{
				Generate("PerlinLikeBiome");	
			}
			if(GUI.Button(new Rect(window_Start_X + 20,window_Start_Y + button_Start_Y+(30*8),button_Width,20), "Stick Biome")) 
			{
				Generate("StickBiome");	
			}
			if(GUI.Button(new Rect(window_Start_X + 20,window_Start_Y + button_Start_Y+(30*9),button_Width,20), "Stick Dungeon")) 
			{
				Generate("StickDungeon");	
			}
		}
	}
}
