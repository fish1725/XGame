//PlayerMovement.cs is a simple script for creating and moving your player.
//To use it place it on the player prefab in Resources folder or any other player prefab you come up with.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ProD
{
	public class PlayerMovement : MonoBehaviour 
	{
		public bool allowCrossMovement = true;
		public float layer = 1f; //Player's y distance from the map.
		public bool CameraFollow = true;
		public bool AutomaticalTracking = false;
		
		//private Address currentMapIndicator;
		private Map currentMap = null;
		private WorldMap currentWorld = null;
		private Cell currentCell;
		
		private CameraObjectTracker myCameraTracker;
		
		public void Awake()
		{
			SetCamera("Main Camera");
		}
		
		/// <summary>
		/// Places the player on the map.
		/// </summary>
		/// <param name='newWorld'>
		/// The world to put the player in.
		/// </param>
		public void SetupPlayer(WorldMap newWorld)
		{
			SetupPlayer(newWorld, new Address(0,0), null);
		}
		
		/// <summary>
		/// Places the player on the map.
		/// </summary>
		/// <param name='newWorld'>
		/// The world to put the player in.
		/// </param>
		/// <param name='mapAdress'>
		/// Indicates which map of the world the player is put in.
		/// </param>
		/// <param name='spawnPoint'>
		/// The address in the map the player is spawned at. If its null, the player will spawn at a random spot.
		/// </param>
		public void SetupPlayer(WorldMap newWorld, Address mapAdress, Address spawnPoint)
		{
			if (newWorld.maps == null || newWorld.size_X <= 0 || newWorld.size_Y <= 0)
				return;
			
			currentWorld = newWorld;
			
			//Resize player to cell size
			float scale = ProDManager.Instance.prefab_Size_X;
			transform.localScale = new Vector3(scale,1,scale);
			
			MovePlayerToMap(mapAdress, spawnPoint);
		} 
		
		/// <summary>
		/// Moves the player to a map within the world.
		/// </summary>
		/// <param name='mapAdress'>
		/// Indicates the position of the map within the world.
		/// </param>
		/// <param name='mapAdress'>
		/// The address in the map the player is moved to. If its null, the player will spawn at a random spot.
		/// </param>
		public void MovePlayerToMap(Address mapAdress, Address spawnPoint)
		{
			if (currentWorld == null || mapAdress.x < 0 || mapAdress.x >= currentWorld.size_X || 
				mapAdress.y < 0 || mapAdress.y >= currentWorld.size_Y)
				return;
			
			
			currentMap = currentWorld.maps[mapAdress.x,mapAdress.y];
			if (spawnPoint == null)
			{
				List<Cell> placementList = MethodLibrary.GetListOfCellType("Path", currentMap );
				MoveToCell(placementList[Random.Range(0,placementList.Count-1)]);
			}
			else 
				MoveToCell(currentMap.GetCell(spawnPoint.x, spawnPoint.y));
		}
	
		//Ask the necessary questions for moving to the new cell.
		//If all conditions are met move the player to that cell.
		public void MoveToCell(Cell targetCell)
		{
			if (currentWorld == null || currentMap == null)
				return;
			bool cellIsMovableTo = false;
			
			if((targetCell.type == "Path" || targetCell.type == "Door") /* && some other conditions are met. */)
			{
				cellIsMovableTo = true;	
			}
			
			if(cellIsMovableTo)
			{
				//
				Address actualCellPosition = currentWorld.GetAddressWorldPosition(currentMap, new Address(targetCell.x, targetCell.y));
	
				//Put the player on the world location 1 units above map.
				
				float newPos_X = this.transform.localScale.x*((float)actualCellPosition.x);//*ProDManager.Instance.prefab_Size_X;
				float newPos_Z = this.transform.localScale.z*((float)actualCellPosition.y);//*ProDManager.Instance.prefab_Size_Y;
				
				transform.position = new Vector3(newPos_X, layer, newPos_Z);
				//Set his theoretical location on the array.
				currentCell = targetCell;
				
				//Reposition camera on player.
				if (CameraFollow && !AutomaticalTracking && myCameraTracker != null)
					myCameraTracker.SetCameraToPosition(transform.position);
			}	
		}
		
		/// <summary>
		/// Sets the camera tracking this player
		/// </summary>
		/// <param name='cameraName'>
		/// The name of the camera to track the player.
		/// </param>
		public void SetCamera(string cameraName)
		{
			myCameraTracker = GameObject.Find(cameraName).GetComponent<CameraObjectTracker>();
			if (AutomaticalTracking)
			{
				myCameraTracker.AutomaticalTracking = true;
				myCameraTracker.Target = this.transform;
			}
		}
		
		//Set movement input here
		void Update() 
		{
			if (currentWorld == null || currentMap == null)
				return;
			//Move using CursorKeys and 2,4,6,8 on numPad
	        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8))
			{
				MoveToCell(currentMap.cellsOnMap[currentCell.x, currentCell.y+1]);
	    	}
			else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2))
			{
				MoveToCell(currentMap.cellsOnMap[currentCell.x, currentCell.y-1]);
	    	}
			if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4))
			{
				MoveToCell(currentMap.cellsOnMap[currentCell.x-1, currentCell.y]);
	    	}
			if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6))
			{
				MoveToCell(currentMap.cellsOnMap[currentCell.x+1, currentCell.y]);
	    	}
			
			//Move using 1,3,7,9 on numPad
			if(allowCrossMovement)
			{
				if(Input.GetKeyDown(KeyCode.Keypad9))
				{
					MoveToCell(currentMap.cellsOnMap[currentCell.x+1, currentCell.y+1]);
		    	}
				if(Input.GetKeyDown(KeyCode.Keypad3))
				{
					MoveToCell(currentMap.cellsOnMap[currentCell.x+1, currentCell.y-1]);
		    	}
				if(Input.GetKeyDown(KeyCode.Keypad7))
				{
					MoveToCell(currentMap.cellsOnMap[currentCell.x-1, currentCell.y+1]);
		    	}
				if(Input.GetKeyDown(KeyCode.Keypad1))
				{
					MoveToCell(currentMap.cellsOnMap[currentCell.x-1, currentCell.y-1]);
		    	}
			}
		}
	}
}
