using UnityEngine;
using System.Collections;

namespace ProD
{
	public class TestSceneGUI_1 : MonoBehaviour 
	{
		public WorldMap worldMap;
		
		private bool showGUI = true;
		public int seedNum = 0;
		private string seedStr = "Type in a number for seed.\n0 acts as random.";
		
		
		public float button_Width_Modifier = 2f;
		public float button_Width = 90f;
		public float button_Start_Y = 380;
		public Color guiColor;
		
		public float window_Start_X = 804;
		public float window2_Start_X = 504;
		public float window_Start_Y = 66;
		public float window2_Start_Y = 10;
		public float window_Heigth = 492;
		public float window1_Heigth = 436;	
		public float window_Width = 200;
		
		public float window1_Content_Start_Y = 30;
		
		public float label_Start_Y = 14;
		private float label_Width = 125;
		public float label_Width_0 = 175;
		
		public float space_Vertical_0 = 10;
		public float space_Vertical_1 = 32;
		public float space_Vertical_2 = 37;
		public float space_Vertical_3 = 17;
		
		public float slider_Start_X = 30;
		public float slider_Start_Y = 48;
		public float slider_Width = 160;
		
		public float textField_Start_Y = 38;
		
		public float slider_worldMap_Width = 1f;
		public float slider_worldMap_Height = 1f;
		public float slider_mapWidth = 33.0F;
		public float slider_mapHeight = 33.0F;
		public float slider_roomWidthMin = 3.0F;
		public float slider_roomWidthMax = 11.0F;
		public float slider_roomHeightMin = 3.0F;
		public float slider_roomHeightMax = 11.0F;
		public float slider_roomFrequency = 24.0F;
		
		public bool checkBox_FrameMap = false;
		public bool checkBox_alternativeCorridors = false;
		public bool checkBox_useSeed = false;
		
	
		
		public WorldMap Generate( string generatorName )
		{
			worldMap =  Generator_Generic_World.Generate(generatorName, Mathf.FloorToInt(slider_worldMap_Width), Mathf.FloorToInt(slider_worldMap_Height), Mathf.FloorToInt(slider_mapWidth), Mathf.FloorToInt(slider_mapHeight));
			Materializer.Instance.MaterializeWorldMap(worldMap);
			ProDManager.Instance.SpawnPlayer(worldMap);
			return worldMap;
		}
		
		void OnGUI () 
		{
			GUI.backgroundColor = guiColor;
			
			#region SHOW/HIDE
			GUI.backgroundColor = Color.red;
			if(GUI.Button(new Rect(window_Start_X + 20, 20 , button_Width*button_Width_Modifier , 16 ), "Hide/Show Panel")) 
				showGUI = !showGUI;
			
			if(!showGUI) return;
			GUI.backgroundColor = guiColor;
			#endregion

			#region window 1
			GUI.Box(new Rect(window_Start_X + 10,window_Start_Y, window_Width, window1_Heigth),"");			
		
			GUI.Label(										new Rect(window_Start_X + slider_Start_X,        window1_Content_Start_Y + (0*space_Vertical_3), label_Width, 30), "Borders");
			checkBox_FrameMap = GUI.Toggle(					new Rect(window_Start_X + slider_Start_X + slider_Width-16, window1_Content_Start_Y + (0*space_Vertical_3), label_Width, 30), checkBox_FrameMap, "");
			
			GUI.Label(										new Rect(window_Start_X + slider_Start_X,        window1_Content_Start_Y + (1*space_Vertical_3), label_Width, 30), "Alternative Corridors");
			checkBox_alternativeCorridors = GUI.Toggle(		new Rect(window_Start_X + slider_Start_X + slider_Width-16, window1_Content_Start_Y + (1*space_Vertical_3), label_Width, 30), checkBox_alternativeCorridors, "");
			
			GUI.Label(										new Rect(window_Start_X + slider_Start_X,        window1_Content_Start_Y + (2*space_Vertical_3), label_Width, 30), "Use Seed");
			checkBox_useSeed = GUI.Toggle(					new Rect(window_Start_X + slider_Start_X + slider_Width-16, window1_Content_Start_Y + (2*space_Vertical_3), label_Width, 30), checkBox_useSeed, "");

			
			seedStr = GUI.TextField(new Rect(window_Start_X + slider_Start_X, window1_Content_Start_Y + (2.8f*space_Vertical_3), slider_Width, 37), seedStr);
			int.TryParse(seedStr, out seedNum);

			if(GUI.Button(new Rect(window_Start_X + 20, button_Start_Y ,button_Width*button_Width_Modifier,60), "Generate")) 
			{
				if (checkBox_useSeed && seedNum != 0)
				{
					ProDManager.Instance.UseSeed = true;
					ProDManager.Instance.Seed = seedNum;	
				}
				else 
					ProDManager.Instance.UseSeed = false;
				
				Generator_Dungeon.SetSpecificProperties("Abyss", "Path", "Wall", 
					Mathf.FloorToInt(slider_roomWidthMin), 
					Mathf.FloorToInt(slider_roomWidthMax), 
					Mathf.FloorToInt(slider_roomHeightMin), 
					Mathf.FloorToInt(slider_roomHeightMax), 
					Mathf.FloorToInt(slider_roomFrequency), 
					6, !checkBox_alternativeCorridors, 12, checkBox_FrameMap, checkBox_alternativeCorridors);
				Generate("Dungeon");	
			}
			#endregion window 1
			
			#region window 2
			GUI.Box(new Rect(window2_Start_X + 10,window2_Start_Y, window_Width, window_Heigth),"");
			
			GUI.Label(new Rect(window2_Start_X + slider_Start_X, label_Start_Y,label_Width_0,window_Heigth), 
				"Generator_Dungeon.cs" 
				+ "\n"
				+ "\n" + "Tweak and generate via panel."
				+ "\n" + "Drag via LMB & Zoom via MS."
				);
			
			slider_worldMap_Width = GUI.HorizontalSlider(	new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (0*space_Vertical_2) + space_Vertical_1, slider_Width, 30), slider_worldMap_Width, 1.0F, 3.0F);
			slider_worldMap_Width = Mathf.Ceil(slider_worldMap_Width);
			GUI.Label(										new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (0*space_Vertical_2) + space_Vertical_0, label_Width, 30), "World Map Width " + slider_worldMap_Width);
	
			slider_worldMap_Height = GUI.HorizontalSlider(	new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (1*space_Vertical_2) + space_Vertical_1, slider_Width, 30), slider_worldMap_Height, 1.0F, 3.0F);
			slider_worldMap_Height = Mathf.Ceil(slider_worldMap_Height);
			GUI.Label(										new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (1*space_Vertical_2) + space_Vertical_0, label_Width, 30), "World Map Height " + slider_worldMap_Height);
			
			
			
			slider_mapWidth = GUI.HorizontalSlider(			new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (2*space_Vertical_2) + space_Vertical_1, slider_Width, 30), slider_mapWidth, 15.0F, 55.0F);
			slider_mapWidth = Mathf.Ceil(slider_mapWidth);
			if(slider_mapWidth%2 == 0) slider_mapWidth+=1;
			GUI.Label(										new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (2*space_Vertical_2) + space_Vertical_0, label_Width, 30), "Map Width " + slider_mapWidth);
			
			
			
			slider_mapHeight = GUI.HorizontalSlider(		new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (3*space_Vertical_2) + space_Vertical_1, slider_Width, 30), slider_mapHeight, 15.0F, 55.0F);
			slider_mapHeight = Mathf.Ceil(slider_mapHeight);
			if(slider_mapHeight%2 == 0) slider_mapHeight+=1;
			GUI.Label(										new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (3*space_Vertical_2) + space_Vertical_0, label_Width, 30), "Map Height " + slider_mapHeight);
			
			
			
			slider_roomFrequency = GUI.HorizontalSlider(	new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (4*space_Vertical_2) + space_Vertical_1, slider_Width, 30), slider_roomFrequency, 1.0F, 24.0F);
			slider_roomFrequency = Mathf.Ceil(slider_roomFrequency);
			GUI.Label(										new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (4*space_Vertical_2) + space_Vertical_0, label_Width, 30), "Room Frequency " + slider_roomFrequency);
			
			
			
			slider_roomWidthMin = GUI.HorizontalSlider(		new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (5*space_Vertical_2) + space_Vertical_1, slider_Width, 30), slider_roomWidthMin, 3.0F, 23.0F);
			slider_roomWidthMin = Mathf.Ceil(slider_roomWidthMin);
			if(slider_roomWidthMin%2 == 0) slider_roomWidthMin+=1;
			GUI.Label(										new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (5*space_Vertical_2) + space_Vertical_0, label_Width, 30), "Room Width Min " + slider_roomWidthMin);
			
			
			
			slider_roomWidthMax = GUI.HorizontalSlider(		new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (6*space_Vertical_2) + space_Vertical_1, slider_Width, 30), slider_roomWidthMax, 3.0F, 23.0F);
			slider_roomWidthMax = Mathf.Ceil(slider_roomWidthMax);
			if(slider_roomWidthMax%2 == 0) slider_roomWidthMax+=1;
			GUI.Label(										new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (6*space_Vertical_2) + space_Vertical_0, label_Width, 30), "Room Width Max " + slider_roomWidthMax);
			
			
			
			slider_roomHeightMin = GUI.HorizontalSlider(	new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (7*space_Vertical_2) + space_Vertical_1, slider_Width, 30), slider_roomHeightMin, 3.0F, 23.0F);
			slider_roomHeightMin = Mathf.Ceil(slider_roomHeightMin);
			if(slider_roomHeightMin%2 == 0) slider_roomHeightMin+=1;
			GUI.Label(										new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (7*space_Vertical_2) + space_Vertical_0, label_Width, 30), "Room Height Min " + slider_roomHeightMin);
			
			
			
			slider_roomHeightMax = GUI.HorizontalSlider(	new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (8*space_Vertical_2) + space_Vertical_1, slider_Width, 30), slider_roomHeightMax, 3.0F, 23.0F);
			slider_roomHeightMax = Mathf.Ceil(slider_roomHeightMax);
			if(slider_roomHeightMax%2 == 0) slider_roomHeightMax+=1;
			GUI.Label(										new Rect(window2_Start_X + slider_Start_X, slider_Start_Y + (8*space_Vertical_2) + space_Vertical_0, label_Width, 30), "Room Height Max " + slider_roomHeightMax);
			
		
			
			if(slider_roomHeightMin > slider_roomHeightMax) slider_roomHeightMin = slider_roomHeightMax;
			if(slider_roomWidthMin > slider_roomWidthMax) slider_roomWidthMin = slider_roomWidthMax;
			#endregion  window 2
		}
	}
}
