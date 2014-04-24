using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace ProD
{
	public class Materializer : Singleton<Materializer> 
	{
		//TODO: Write a method that nicely parents cells in a given scene to their respective maps and so forth.
		
		#region [ private fields ]
		private Dictionary<string, GameObject> tileDictionary;
		private bool _groupTiles;
		private GameObject parentObject;
		#endregion
		
		#region [ public properties ]
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Materializer"/> groups the generated tiles.
		/// This reduces clutter in the Unity scene.
		/// WARNING! This requires reparenting and can be very expensive. Grouping is turned off by default.
		/// </summary>
		/// <value>
		/// <c>true</c> if group tiles; otherwise, <c>false</c>.
		/// </value>
		public bool GroupTiles 
		{
			get { return _groupTiles; }
			set { _groupTiles = value; }
		}
		#endregion
		
		public static List<GameObject> allPrefabs = new List<GameObject>();
		
		/// <summary>
		/// Materializes the world map.
		/// </summary>
		/// <param name='worldMap'>
		/// The world map to materialize.
		/// </param>
		public void MaterializeWorldMap(WorldMap worldMap)
		{
			for (int j = 0; j < worldMap.size_Y; j++) 
			{
				for (int i = 0; i < worldMap.size_X; i++) 
				{
					if (GroupTiles) {
						parentObject = new GameObject();
						parentObject.name = "Map_" + i.ToString() + "_" + j.ToString();
						parentObject.transform.parent = this.transform;
					}
					MaterializeMap(worldMap.maps[i,j]);
				}
			}
		}
		
		/// <summary>
		/// Unmaterializes the world map by destorying all the prefabs that were instantiated
		/// </summary>
		/// <param name='worldMap'>
		/// World map.
		/// </param>
		public  void UnmaterializeWorldMap (WorldMap worldMap)
		{
			while(allPrefabs.Count > 0)
			{
				GameObject tempGO = allPrefabs[allPrefabs.Count-1];
				allPrefabs.RemoveAt(allPrefabs.Count-1);
				Destroy(tempGO);
			}
		}
		
		/// <summary>
		/// Materializes a map.
		/// </summary>
		/// <param name='map'>
		/// A map of a level.
		/// </param>
		public void MaterializeMap(Map map)
		{
			tileDictionary = new Dictionary<string, GameObject>();
				
			for (int j = 0; j < map.size_Y; j++) 
			{
				for (int i = 0; i < map.size_X; i++) 
				{
					string[] orientation = new string[2];
					orientation = GetOrientation(map, new Address(i,j));
					GameObject prefab;
					prefab = GetPrefab(map.cellsOnMap[i,j].type, orientation, map.theme);		
					PlacePrefab(prefab, map, new Address(i,j), orientation);
				}	
			}
		}
		
		
	
		//TODO:  We can do all these with a binary system. Convert to a binary system later on.
		/// <summary>
		/// Gets the orientation of a tile at a given adress. The orientation of a tile
		/// means on many sides, and which sides, a wall tile will need a surface/texture.
		/// </summary>
		/// <returns>
		/// The orientation, which consists of two strings, one for orientation (Corner, One Sided etc.)
		/// and one for rotation (North, South, West, East etc.)
		/// </returns>
		/// <param name='map'>
		/// The map that contains the tile.
		/// </param>
		/// <param name='a'>
		/// The adress of the tile you want the orientation of.
		/// </param>
		private string[] GetOrientation(Map map, Address a)
		{
			string[] orientation = new string[2];
			orientation[0] = "Default";
			orientation[1] = "";

			//TODO: Add a boolean for rotation instead.
			if(map.theme == "Terminal Theme") return orientation;
			string type = map.cellsOnMap[a.x,a.y].type;
			if     (type == "Door") type = "Wall";
			else if(type == "Path") return orientation;
			
			bool[] b = new bool[4];
			
			//Check for every cell around address. If a surrounding cell is type of type then mark it true.
			//Check clockwise startin from North
			if(a.x   >= 0 && a.x   < map.size_X && a.y+1 >= 0 && a.y+1 < map.size_Y) //Check if address i,j is inside the map boundaries.
				if(map.cellsOnMap[a.x,   a.y+1].type == type) b[0] = true; //North 
			if(a.x+1 >= 0 && a.x+1 < map.size_X && a.y   >= 0   && a.y < map.size_Y)	
				if(map.cellsOnMap[a.x+1, a.y  ].type == type) b[1] = true; //East
			if(a.x   >= 0 && a.x   < map.size_X && a.y-1 >= 0 && a.y-1 < map.size_Y)	
				if(map.cellsOnMap[a.x,   a.y-1].type == type) b[2] = true; //South
			if(a.x-1 >= 0 && a.x-1 < map.size_X && a.y   >= 0 && a.y   < map.size_Y)	
				if(map.cellsOnMap[a.x-1, a.y  ].type == type) b[3] = true; //West
	
			/*
			 * Example
			 * .0.
			 * 3.1
			 * .2.
			 * 
			 * Core     Column 		Corner          	Tip					TwoSided 	OneSided   
			 * .#.		...	   		.#. .#. ... ... 	.#. ... ... ... 	.#. ...  	.#. .#. .#. ...
			 * ###		.#.	   		##. .## .## ##. 	.#. ##. .#. .## 	.#. ###  	.## ### ##. ###
			 * .#.		...	   		... ... .#. .#. 	... ... .#. ... 	.#. ...  	.#. ... .#. .#. 
			 * 						NW  NE  SE  SW      S   E   N   W       V   H       W   S   E   N
			 * 
			 * NA - Not available.
			 * No - North
			 * NW - NorthWest
			 * Ve - Vertical
			 * 
			 */
			
			//According to the marks around the cell return orientation.
			if     ( b[0] &&  b[1] &&  b[2] &&  b[3]) { orientation[0] = "Core";   orientation[1] = ""; }
			else if(!b[0] && !b[1] && !b[2] && !b[3]) { orientation[0] = "Column"; orientation[1] = ""; }
			
			else if( b[0] && !b[1] && !b[2] &&  b[3]) { orientation[0] = "Corner"; orientation[1] = "NW"; }
			else if( b[0] &&  b[1] && !b[2] && !b[3]) { orientation[0] = "Corner"; orientation[1] = "NE"; }
			else if(!b[0] &&  b[1] &&  b[2] && !b[3]) { orientation[0] = "Corner"; orientation[1] = "SE"; }
			else if(!b[0] && !b[1] &&  b[2] &&  b[3]) { orientation[0] = "Corner"; orientation[1] = "SW"; }
			
			else if( b[0] && !b[1] && !b[2] && !b[3]) { orientation[0] = "Tip"; orientation[1] = "S"; }
			else if(!b[0] && !b[1] && !b[2] &&  b[3]) { orientation[0] = "Tip"; orientation[1] = "E"; }
			else if(!b[0] && !b[1] &&  b[2] && !b[3]) { orientation[0] = "Tip"; orientation[1] = "N"; }
			else if(!b[0] &&  b[1] && !b[2] && !b[3]) { orientation[0] = "Tip"; orientation[1] = "W"; }
			
			else if( b[0] && !b[1] &&  b[2] && !b[3]) { orientation[0] = "TwoSided"; orientation[1] = "V"; }
			else if(!b[0] &&  b[1] && !b[2] &&  b[3]) { orientation[0] = "TwoSided"; orientation[1] = "H"; }
			
			else if( b[0] &&  b[1] &&  b[2] && !b[3]) { orientation[0] = "OneSided"; orientation[1] = "W"; }
			else if( b[0] &&  b[1] && !b[2] &&  b[3]) { orientation[0] = "OneSided"; orientation[1] = "S"; }
			else if( b[0] && !b[1] &&  b[2] &&  b[3]) { orientation[0] = "OneSided"; orientation[1] = "E"; }
			else if(!b[0] &&  b[1] &&  b[2] &&  b[3]) { orientation[0] = "OneSided"; orientation[1] = "N"; }
			
			return orientation;
			
		}
		
		/// <summary>
		/// Gets the prefab for a specified type of tile. 
		/// The prefabs need to be in the corresponding directories, or this will return null.
		/// </summary>
		/// <returns>
		/// The prefab.
		/// </returns>
		/// <param name='type'>
		/// The type of tile you want the prefab of
		/// </param>
		/// <param name='orientation'>
		/// The orientation of the prefab. For example, it could be a corner tile or a straight wall.
		/// </param>
		/// <param name='theme'>
		/// The theme of the tile.
		/// </param>
		public GameObject GetPrefab(string type, string[] orientation, string theme)
		{
			GameObject prefab = null;
			//Check in the dictionary if this type of tile already exists. If not, load it from the resources.

			string prefabType = orientation[0];
			//Debug.Log(prefabType);
				
			string directory = theme + "/Cells/" + type + "/PRE_"+ type + "_" + prefabType;
			//Debug.Log(directory);
			if (tileDictionary.ContainsKey(directory))
				prefab = tileDictionary[directory];
			else 
			{
				prefab = Resources.Load(directory) as GameObject;
				if(prefab == null)
				{
					//If there are no matches in the directory try for default prefab.
					directory = theme + "/Cells/" + type + "/PRE_"+ type + "_" + "Default";
					if (tileDictionary.ContainsKey(directory))
						prefab = tileDictionary[directory];
					else 
					{
						prefab = Resources.Load(directory) as GameObject;
						//Add the tile to the tileDictionary.
						tileDictionary.Add(directory,prefab);
					}
				}
				else 
					//Add the tile to the tileDictionary.
					tileDictionary.Add(directory,prefab);
			}

			return prefab;
		}
		
		/// <summary>
		/// Places the prefab by instantiating it at a given location on the map. 
		/// </summary>
		/// <param name='prefab'>
		/// The prefab.
		/// </param>
		/// <param name='map'>
		/// The map which the tile belongs to.
		/// </param>
		/// <param name='address'>
		/// The adress of the tile in the map.
		/// </param>
		/// <param name='orientation'>
		/// The orientation of the tile.
		/// </param>
		public void PlacePrefab(GameObject prefab, Map map, Address address, string[] orientation)
		{
			if(prefab == null)
			{
				//Debug.LogError("Null is not a valid prefab for placement.");
				return;
			}

			//IMPORTANT: This is for when you're using a top down approach. This allows you the use of 3D objects with built-in gravity.
			if(ProDManager.Instance.thirdAxis == ProDManager.ThirdAxis.y)
			{
				//Location in worldMap
				float prefab_X = map.addressOnWorldMap.x * map.size_X * ProDManager.Instance.prefab_Size_X;//* prefab.transform.localScale.x;
				float prefab_Y = 0;
				float prefab_Z = map.addressOnWorldMap.y * map.size_Y * ProDManager.Instance.prefab_Size_Y; //* prefab.transform.localScale.z;
				
				//Location in Map
				prefab_X += address.x *ProDManager.Instance.prefab_Size_X; //prefab.transform.localScale.x*
				prefab_Y += prefab.transform.position.y;
				prefab_Z += address.y *ProDManager.Instance.prefab_Size_Y; //prefab.transform.localScale.y*
				
				GameObject cellGO = (GameObject) Instantiate(prefab, new Vector3(prefab_X, prefab_Y, prefab_Z), prefab.transform.rotation);
				// Reparent the instantiated object. This is expensive, so only do it if GroupTiles is enabled
				if (GroupTiles)
					cellGO.transform.parent = this.parentObject.transform;
		
				cellGO.transform.localScale = new Vector3(ProDManager.Instance.prefab_Size_X, 1, ProDManager.Instance.prefab_Size_Y);
				
				string rotation = orientation[1];
				
				if(rotation == "V") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.y + 90f,cellGO.transform.rotation.z));
				
				else if(rotation == "W") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.y + 90f,cellGO.transform.rotation.z));
				else if(rotation == "N") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.y + 180f,cellGO.transform.rotation.z));
				else if(rotation == "E") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.y + 270,cellGO.transform.rotation.z));
					
				else if(rotation == "SE") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.y + 90,cellGO.transform.rotation.z));
				else if(rotation == "SW") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.y + 180,cellGO.transform.rotation.z));
				else if(rotation == "NW") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.y + 270,cellGO.transform.rotation.z));
				
				allPrefabs.Add(cellGO.gameObject);
			}
			//IMPORTANT: This is for when you want to use the 2D system that came with Unity 4.0. Other scripts like movement and camera are not compatible with this yet.
			//TODO: This isn't working due to a bug. Wijnand HEEEEELP!
			else if(ProDManager.Instance.thirdAxis == ProDManager.ThirdAxis.z)
			{
				Debug.LogWarning("Hi! We apologize for the inconveniency this may cause you. This feature is still under construction.");

				//Location in worldMap
				float prefab_X = map.addressOnWorldMap.x * map.size_X * ProDManager.Instance.prefab_Size_X;//* prefab.transform.localScale.x;
				float prefab_Z = 0;
				float prefab_Y = map.addressOnWorldMap.y * map.size_Y * ProDManager.Instance.prefab_Size_Y; //* prefab.transform.localScale.z;
				
				//Location in Map
				prefab_X += address.x *ProDManager.Instance.prefab_Size_X; //prefab.transform.localScale.x*
				prefab_Z += prefab.transform.position.y;
				prefab_Y += address.y *ProDManager.Instance.prefab_Size_Y; //prefab.transform.localScale.y*
				
				GameObject cellGO = (GameObject) Instantiate(prefab, new Vector3(prefab_X, prefab_Y, prefab_Z), prefab.transform.rotation);
				// Reparent the instantiated object. This is expensive, so only do it if GroupTiles is enabled
				if (GroupTiles)
					cellGO.transform.parent = this.parentObject.transform;
				
				cellGO.transform.localScale = new Vector3(ProDManager.Instance.prefab_Size_X, ProDManager.Instance.prefab_Size_Y, 1);
				
				string rotation = orientation[1];

				cellGO.transform.Rotate(270,0,0);

				if(rotation == "V") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.z, 90f));

				else if(rotation == "W") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.z,90f));
				else if(rotation == "N") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.z,180f));
				else if(rotation == "E") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.z,270));
				
				else if(rotation == "SE") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.z,90));
				else if(rotation == "SW") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.z,180));
				else if(rotation == "NW") cellGO.transform.Rotate(new Vector3(cellGO.transform.rotation.x,cellGO.transform.rotation.z,270));

				allPrefabs.Add(cellGO.gameObject);
			}
			return;
		}
		
	}
}