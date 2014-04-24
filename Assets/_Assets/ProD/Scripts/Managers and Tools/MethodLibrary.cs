using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ProD
{
	public static class MethodLibrary 
	{
		//DO FOLLOWING BEFORE RELEASE:
		//TODO: Test all methods and make sure they all work accordingly.
		
		//ADD FOLLOWING METHODS IN THE FUTURE:
		//TODO: 1- Cavern generation requires a method to check if all cavern formations are connected.
		//TODO: 2- Better staircase placement method.
		//TODO: 3- Integrate Quad Tree generation.
		//TODO: 4- Map connection algorithm for dungeons and caverns and more.
		//TODO: 5- Make it possible to define corridor thickness.
		
		/// <summary>
		/// Gets the count of a type of cells in a map.
		/// </summary>
		/// <returns>
		/// The count of cell type.
		/// </returns>
		/// <param name='type'>
		/// The type of the cell you want the count of.
		/// </param>
		/// <param name='map'>
		/// The specific map you want to know the cellcount of.
		/// </param>
		public static int GetCountOfCellType(string type, Map map)
		{
			return GetListOfCellType(type,map).Count;
		}
		
		/// <summary>
		/// Gets a list of cells of a specific type by iterating through the map
		/// </summary>
		/// <returns>
		/// The list of the cells of that type.
		/// </returns>
		/// <param name='type'>
		/// The type of the cells you want the list of.
		/// </param>
		/// <param name='map'>
		/// The specific map that you want the list of cells from.
		/// </param>
		public static List<Cell> GetListOfCellType(string type, Map map)
		{
			List<Cell>  cells = new List<Cell>();
			for(int i = 0; i < map.size_X; i++)
			{
				for(int j = 0; j < map.size_Y; j++)
				{
					if(map.cellsOnMap[i,j].type.ToString() == type) 
						cells.Add(map.cellsOnMap[i,j]);	
				}
			}
			return cells;
		}
		
		/// <summary>
		/// Sets all the cells in a given map of type A to type B by iterating through them.
		/// </summary>
		/// <param name='map'>
		/// The map you want to change.
		/// </param>
		/// <param name='type_A'>
		/// The type of the cells you want to change.
		/// </param>
		/// <param name='type_B'>
		/// The type you want the cells to change to.
		/// </param>
		public static void SetCellsOfTypeAToB(Map map, string type_A, string type_B)
		{
			for(int i = 0; i < map.size_X; i++)
			{
				for(int j = 0; j < map.size_Y; j++)
				{
					if(map.cellsOnMap[i,j].type == type_A) 
						map.cellsOnMap[i,j].SetCellType(type_B);
				}
			}
		}
		
		/// <summary>
		/// Converts the cell array into string array, containing the cell types.
		/// </summary>
		/// <returns>
		/// A two dimensional string array 
		/// </returns>
		/// <param name='map'>
		/// The map to convert
		/// </param>
		public static string[,] ConvertCellArrayIntoStringArray(Map map)
		{
			string[,] mapInString  = new string[map.size_X, map.size_Y];
			
			for(int i = 0; i < map.size_X; i++)
			{
				for(int j = 0; j < map.size_Y; j++)
				{
					if(map.cellsOnMap[i,j] != null) 
						mapInString[i,j] = map.cellsOnMap[i,j].type.ToString();
				}
			}
			return mapInString;
		}
		
		/// <summary>
		/// Resets all the cells on the map, by setting them all to one specified type
		/// </summary>
		/// <param name='map'>
		/// The map to convert.
		/// </param>
		/// <param name='type'>
		/// The cell type to convert all the cells to.
		/// </param>
		public static void ResetCellsOnMap(Map map, string type)
		{
			for(int i = 0; i < map.size_X; i++)
			{
				for(int j = 0; j < map.size_Y; j++)
				{
					map.cellsOnMap[i,j] = new Cell(new Address(i,j));
					map.cellsOnMap[i,j].SetCellType(type);
				}
			}
		}
		
		/// <summary>
		/// Converts all unreachable cells. 
		/// This method will iterate through a map and convert cell type_A to type_B if and only if a cell is surrounded
		/// by cells on all 8 directions with the same type as himself.
		/// This is usually called for converting Wall type cells into Abyss type cells to benefit from a lower object count in a scene
		/// </summary>
		/// <param name='map'>
		/// The map to convert the cells of.
		/// </param>
		/// <param name='type_A'>
		/// The type of the cells you want converted, if they are surrounded by cells of the same type.
		/// </param>
		/// <param name='type_B'>
		/// The type to convert the cells to.
		/// </param>
		public static void ConvertUnreachableCells(Map map, string type_A, string type_B)
		{
			//Iterate once and report all surrounded cells in a coordinate list.
			//If you start fixing right away then you will get a checker pattern.
			List<Vector2> surroundedPositions = new List<Vector2>();
			for(int i = 0; i < map.size_X; i++)
			{
				for(int j = 0; j < map.size_Y; j++)
				{
					//Check if cell has type_A around it.
					//Set cell to type if it's surrounded by type_A.
					List<Cell> neighbours = FindNeighbourCells(map,i,j, true, true);
					if (neighbours.Count > 0 && neighbours.TrueForAll(c => c.type == type_A))
						surroundedPositions.Add(new Vector2((float)i,(float)j));
				}
			}
			
			//Apply the changes with the help of the coordinate list.
			foreach (Vector2 p in surroundedPositions)
				map.cellsOnMap[(int)p.x,(int)p.y].SetCellType(type_B);
		}
		
		/// <summary>
		/// Finds the neighbouring cells at a specified coordinate.
		/// </summary>
		/// <returns>
		/// The list of neighbouring cells.
		/// </returns>
		/// <param name='map'>
		/// The map of the cell to find the neighbouring cells of.
		/// </param>
		/// <param name='pos_X'>
		/// The x coordinate of the cell.
		/// </param>
		/// <param name='pos_Y'>
		/// The y coordinate of the cell.
		/// </param>
		public static List<Cell> FindNeighbourCells(Map map , int pos_X, int pos_Y)
		{
			return FindNeighbourCells(map,pos_X, pos_Y,true, false);
		}
			
		/// <summary>
		/// Finds the neighbouring cells at a specified coordinate.
		/// </summary>
		/// <returns>
		/// The list of neighbouring cells.
		/// </returns>
		/// <param name='map'>
		/// The map of the cell to find the neighbouring cells of.
		/// </param>
		/// <param name='pos_X'>
		/// The x coordinate of the cell.
		/// </param>
		/// <param name='pos_Y'>
		/// The y coordinate of the cell.
		/// </param>
		/// <param name='diagonal'>
		/// Option to include diagonal cells among the list of neighbours
		/// </param>
		/// <param name='includeSelf'>
		/// Option to include the cell itself among the list of neighbours
		/// </param>
		public static List<Cell> FindNeighbourCells(Map map, int pos_X, int pos_Y, bool diagonal, bool includeSelf)
		{
			List<Cell> result = new List<Cell>();
			for (int i = -1; i <= 1; i++) {	
				for (int j = -1; j <= 1; j++) {
					//If i and j are 0 and includeSelf is false, continue. If both i and j are not 0 and diagonal is false, continue
					if (((i == 0 && j == 0) && !includeSelf) || ((i != 0 && j != 0) && !diagonal))
						continue;
					Cell c = map.GetCell(pos_X+i, pos_Y+j);
					if (c != null)
						result.Add(c);	
				}
			}
			return result;
		}
		
		/// <summary>
		/// Grows each cell of the specified type in a plus shape.
		/// It will iterate through the map and for each cell of the specified type, it will 
		/// set the neighboring cells' types on North, East, West and South to the specified type.
		/// This is usually used to spice up a biome.
		/// </summary>
		/// <param name='type'>
		/// The specified type of cell to grow.
		/// </param>
		/// <param name='growthSize'>
		/// The growth size.
		/// </param>
		/// <param name='map'>
		/// The map to change.
		/// </param>
		public static void GrowCellsInPlusShape(string type, int growthSize, Map map)
		{
			List<Cell> cells = new List<Cell>();
			cells = GetListOfCellType(type, map);
			
			foreach (Cell c in cells)
			{
				for(int i = c.x-growthSize; i <= c.x+growthSize; i++ )
				{
					if(i >= 0 && i < map.size_X) 
					{
						map.cellsOnMap[i,c.y].SetCellType(type);
					}
				}
				for(int j = c.y-growthSize; j <= c.y+growthSize; j++ )
				{
					if(j >= 0 && j < map.size_Y) 
					{
						map.cellsOnMap[c.x,j].SetCellType(type);
					}
				}
			}
		}
		
		/// <summary>
		/// Connects all cells of a specified type.
		/// Iterate through all cell of this type and apply a chance of connection to a closest cell of this type.
		/// Closest cell is searched as follows: For (x,y) look for the closest cell on (x+n,y+n).
		/// </summary>
		/// <param name='type'>
		/// The type of cells to connect
		/// </param>
		/// <param name='map'>
		/// The map to change.
		/// </param>
		/// <param name='chance'>
		/// The chance of connecting two cells.
		/// </param>
		public static void ConnectAllCells(string type, Map map, float chance)
		{
			List<Cell> cellsVisited = new List<Cell>();
			
			List<Cell> cells = new List<Cell>();
			cells = GetListOfCellType(type, map);
			//Debug.Log(cells.Count + " count of wall cells on map.");
	
			foreach (Cell c in cells)
			{
				if(cellsVisited.Contains(c)) 
					continue; //This will skip if we connected c already.
				else if(Random.Range(0,100) < chance)
				{
					int stepLength = 1;
					int searchStep_X = c.x;
					int searchStep_Y = c.y;
					bool connected = false;
					while(!connected)
					{
						//If there are no walls to connect to and we ran out of map space anyhow.
						if((c.x+stepLength) >= map.size_X && (c.y+stepLength) >= map.size_Y) connected = true;
						
						//Search from bottom to top part of the L shape.
						searchStep_X = c.x + stepLength;
						for(searchStep_Y = c.y; searchStep_Y <= searchStep_X; searchStep_Y++)
						{
							if(searchStep_X >= map.size_X || searchStep_Y >= map.size_Y) break;
							if(map.cellsOnMap[searchStep_X, searchStep_Y].type.Equals(type) )
							{
								ConnectTwoCells(map.cellsOnMap[c.x,c.y], map.cellsOnMap[searchStep_X,searchStep_Y], map, type);
								cellsVisited.Add(map.cellsOnMap[c.x,c.y]);
								cellsVisited.Add(map.cellsOnMap[searchStep_X,searchStep_Y]);
								connected = true;
							}
						}
						
						//Search from left to right part of the L shape.
						searchStep_Y = c.y + stepLength;
						for(searchStep_X = c.x; searchStep_X <= searchStep_Y; searchStep_X++)
						{
							if(searchStep_X >= map.size_X || searchStep_Y >= map.size_Y) break;
							if(map.cellsOnMap[searchStep_X, searchStep_Y].type.Equals(type) )
							{
								ConnectTwoCells(map.cellsOnMap[c.x,c.y], map.cellsOnMap[searchStep_X,searchStep_Y], map, type);
								cellsVisited.Add(map.cellsOnMap[c.x,c.y]);
								cellsVisited.Add(map.cellsOnMap[searchStep_X,searchStep_Y]);
								connected = true;
							}
						}
						
						//Make the L shape one size bigger.
						stepLength++;
					}
				}	
			}
		}
		
		/// <summary>
		/// Connects the fromCell and toCell.
		/// This is done by iterating through and setting the space between them to the specified type.
		/// </summary>
		/// <param name='fromCell'>
		/// The From cell.
		/// </param>
		/// <param name='toCell'>
		/// The To cell.
		/// </param>
		/// <param name='map'>
		/// The map to alter.
		/// </param>
		/// <param name='type'>
		/// The type of cell that is used for connecting the two cells.
		/// </param>	
		public static void ConnectTwoCells(Cell fromCell, Cell toCell, Map map, string type)
		{
			ConnectTwoCells(fromCell, toCell, map, type, true, 1);
		}
		
		/// <summary>
		/// Connects the fromCell and toCell.
		/// This is done by iterating through and setting the space between them to the specified type.
		/// </summary>
		/// <param name='fromCell'>
		/// The From cell.
		/// </param>
		/// <param name='toCell'>
		/// The To cell.
		/// </param>
		/// <param name='map'>
		/// The map to alter.
		/// </param>
		/// <param name='type'>
		/// The type of cell that is used for connecting the two cells.
		/// </param>
		/// <param name='diagonal'>
		/// If set to true, the cells will be connected with a diagonal corridor. 
		/// Else, it will generate an "L-shaped" corridor.
		/// </param>
		/// <param name='thickness'>
		/// The the thickness of the corridor.
		/// </param>
		public static void ConnectTwoCells(Cell fromCell, Cell toCell, Map map, string type, bool diagonal, int thickness)
		{
			Vector2 direction = new Vector2(toCell.x, toCell.y) - new Vector2(fromCell.x, fromCell.y);		
			Vector2 stepDirection = new Vector2();
			
			if (!diagonal) 
			{
				stepDirection.x = direction.x / Mathf.Abs(direction.x);
				stepDirection.y = direction.y / Mathf.Abs(direction.y);
				int tempX = fromCell.x;// + (int)stepDirection.x;
				while (tempX != toCell.x)
				{
					//map.cellsOnMap[tempX, fromCell.y].SetCellType(type);
					for (int i = 0; i < thickness; i++)
						map.cellsOnMap[tempX, fromCell.y + i].SetCellType(type);
					tempX += (int)stepDirection.x;
				}
				int tempY = fromCell.y; //+ (int)stepDirection.y;
				while (tempY != toCell.y)
				{
					//map.cellsOnMap[tempX, tempY].SetCellType(type);
					for (int i = 0; i < thickness; i++)
						map.cellsOnMap[tempX + i, tempY].SetCellType(type);
					tempY += (int)stepDirection.y;
				}
			}
			else
			{
		
				stepDirection.x = direction.x / Mathf.Abs(direction.x);
				stepDirection.y = direction.y / Mathf.Abs(direction.y);
				
				Vector2 absDir = new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
				
				int xSegmentSize = (int)((absDir.x +1) / (absDir.y +1)) +1;
				int ySegmentSize = (int)((absDir.y +1) / (absDir.x +1)) +1;
				
				int stepCount = (int)(absDir.x + absDir.y);
				
				int tempX = fromCell.x;
				int tempY = fromCell.y;
				for (int i = 1; i <= stepCount; i++)
				//while (tempY != toCell.y && tempX != toCell.x)
				{
					if (xSegmentSize > ySegmentSize)
					{
						if ((i+1) % xSegmentSize == 0 && tempY != toCell.y)
							tempY += (int)stepDirection.y ;
						else 
							tempX += (int)stepDirection.x;
						
						for (int j = 0; j < thickness; j++)
							map.cellsOnMap[tempX , tempY + j].SetCellType(type);
					}
					else 
					{
						if ((i+1) % ySegmentSize == 0 && tempX != toCell.x)
							tempX += (int)stepDirection.x;
						else 
							tempY += (int)stepDirection.y;
						
						for (int j = 0; j < thickness; j++)
							map.cellsOnMap[tempX + j, tempY].SetCellType(type);
					}
	
					//map.cellsOnMap[tempX, tempY].SetCellType(type);
				}
			}
		}
	
		/// <summary>
		/// Iterates through all cells of type_A and remove one of a pair of cells in minDistance proximity.
		/// The removed one of the pair will be replaced by a type_B cell.
		/// </summary>
		/// <param name='type_A'>
		/// The type_of cells to apply the minimum distance too.
		/// </param>
		/// <param name='type_B'>
		/// The type that cells within the minimum distance will change to.
		/// </param>
		/// <param name='map'>
		/// The map to alter.
		/// </param>
		/// <param name='minDistance'>
		/// The minimum distance that cells of the chosen type should be away from eachother
		/// </param>
		public static void ApplyMinDistance(string type_A, string type_B, Map map, int minDistance)
		{
			List<Cell> cells = new List<Cell>();
			cells = GetListOfCellType(type_A, map);
			//Debug.Log(cells.Count + " count of wall cells on map.");
			
			foreach (Cell c in cells)
			{
				//Pass if it's not the type we want to filter for.
				if(!c.type.Equals(type_A)) 
					continue; 
				for(int i = c.x - minDistance; i < c.x + minDistance; i++)
				{
					for(int j = c.y - minDistance; j < c.y + minDistance; j++)
					{	
						//Ignore self.
						if(i == c.x && j == c.y)
							continue; 
						if( i >= 0 && j >= 0 
							&& i < map.size_X 
							&& j < map.size_Y 
							&& map.cellsOnMap[i,j].type.Equals(type_A) )
							map.cellsOnMap[i,j].SetCellType(type_B);
					}
				}
			}
		}
			
		/// <summary>
		/// Iterates through a map, expanding the type every cell of a certain type to its 8 neighboring cells.
		/// Like an ink stain expands, the cell type will do the same
		/// </summary>
		/// <param name='types'>
		/// The types of cells to expand.
		/// </param>
		/// <param name='map'>
		/// The map to alter.
		/// </param>
		/// <param name='recurse'>
		/// The number of times to recur this method
		/// </param>
		public static void ExpandCell(List<string> types, Map map, int recurse)
		{	
			List<Vector2> cellsToExpand = new List<Vector2>();
			for(int i = 0; i < map.size_X; i++)
			{
				for(int j = 0; j < map.size_Y; j++)
				{	
					if(types.Contains(map.cellsOnMap[i,j].type)) 
						cellsToExpand.Add(new Vector2(i,j));
				}
			}
			foreach (Vector2 p in cellsToExpand)
					{
						List<Cell> neighbours = FindNeighbourCells(map,(int)p.x,(int)p.y);
						foreach (Cell c in neighbours)
							map.cellsOnMap[c.x,c.y].SetCellType(map.cellsOnMap[(int)p.x,(int)p.y].type);	
					}
	
			if(recurse > 0) 
				ExpandCell(types, map, recurse - 1);
		}
		
		/// <summary>
		/// Adds noise to a map, by setting a cell at a random place to the specified type. 
		/// This recurs a specified amount of times.
		/// </summary>
		/// <param name='map'>
		/// The map to apply the noise to
		/// </param>
		/// <param name='type'>
		/// The type of cells the noise is going to generate
		/// </param>
		/// <param name='repeat'>
		/// The amount of times this should recur.
		/// </param>
		public static void AddNoise_I(Map map, string type, int repeat)
		{
			//Get random address on map.
			int tempPos_X = Random.Range(0, map.size_X);
			int tempPos_Y = Random.Range(0, map.size_Y);
			
			//Set random address to type.
			map.cellsOnMap[tempPos_X, tempPos_Y].SetCellType(type);
			
			//Iterate until you reach 0.
			if(repeat > 0) AddNoise_I(map, type, repeat - 1);
		}
		
		/// <summary>
		/// Adds noise to a map by iterating through the map, 
		/// and for each cell theres a chance it will be set to the specified type.
		/// This method is being used in generating the cavern in the Test Scene.
		/// </summary>
		/// <param name='map'>
		/// The map to add the noise to.
		/// </param>
		/// <param name='type'>
		/// The type of cells the noise is going to generate.
		/// </param>
		/// <param name='chance'>
		/// The chance any cell will be set to the specified type.
		/// </param>
		/// <param name='frameSize'>
		/// The size of the frame around the noise, consisting of the specified type
		/// </param>
		public static void AddNoise_II(Map map, string type, int chance, int frameSize)
		{
			for(int i = 0; i < map.size_X; i++)
			{
				for(int j = 0; j < map.size_Y; j++)
				{	
					if(i < frameSize ||
					   j < frameSize || 
					   i > map.size_X-1 - frameSize ||
					   j > map.size_Y-1 - frameSize ||
					   Random.Range(0,100) < chance)
						map.cellsOnMap[i,j].SetCellType(type);
				}
			}
		}
		
		/* This method will get random addresses on a map recurse many times
		 * and it will set the cells' types on those addresses to type. 
		 * Addresses will be only on even numbers!*/
		/// <summary>
		/// Adds noise to a map, by setting a cell at a random place, wihting a specified frame to the specified type. 
		/// This repeats a set amount of times. 
		/// </summary>
		/// <param name='map'>
		/// The map to add the noise to.
		/// </param>
		/// <param name='type'>
		/// The type of cells the noise is going to generate.
		/// </param>
		/// <param name='repeat'>
		/// The amount of times this should repeat.
		/// </param>
		/// <param name='frame'>
		/// The frame, or margin, to generate the noise in.
		/// </param>
		public static void AddNoise_III(Map map, string type, int repeat, int frame)
		{
			//Get random address on map.
			int tempPos_X = Random.Range(frame, map.size_X-frame);
			int tempPos_Y = Random.Range(frame, map.size_Y-frame);
			//if(tempPos_X%2 == 1) tempPos_X--;
			//if(tempPos_Y%2 == 1) tempPos_Y--;
			
			//Set random address to type.
			map.cellsOnMap[tempPos_X, tempPos_Y].SetCellType(type);
			
			//Iterate until you reach 0.
			if(repeat > 0) AddNoise_III(map, type, repeat - 1, frame);
		}
		
		/// <summary>
		/// Filters noise in a map. It will iterate through the map and will set a cell type to type
		/// if the cell has at least conversionDensity many cells of type type around it. 
		/// This method is being used in generating the cavern in the Test Scene.
		/// </summary>
		/// <param name='map'>
		/// The map to filter the noise of.
		/// </param>
		/// <param name='type'>
		/// The type of cells use to "clear" the noise.
		/// </param>
		/// <param name='conversionDensity'>
		/// The amount of neighbours of the specified type a cell needs to have to be converted .
		/// </param>
		/// <param name='frameSize'>
		/// The margin to filter the noise in.
		/// </param>
		/// <param name='repeat'>
		/// The amount of recursion.
		/// </param>
		public static void FilterNoise(Map map, string type, int conversionDensity, int frameSize, int repeat)
		{
			List<Vector2> noisePositions = new List<Vector2>();
			for(int i = frameSize; i < map.size_X-frameSize; i++)
			{
				for(int j = frameSize; j < map.size_Y-frameSize; j++)
				{
					int cellCount = 0;
					cellCount = FindNeighbourCells(map,i,j,true,true).FindAll(c => c.type == type).Count;
					if(cellCount > conversionDensity) 
					{	
						noisePositions.Add(new Vector2((float)i,(float)j));
					}
				}
			}
			foreach (Vector2 p in noisePositions)
				map.cellsOnMap[(int)p.x,(int)p.y].SetCellType(type);
			
			if(repeat > 0) FilterNoise(map, type, conversionDensity, frameSize, repeat - 1);
		}
	
		/* This method is being used in generating both the maze and dungeon in the given Test Scene.
		 * 
		 */
		/// <summary>
		/// Creates a maze. The method will run through the whole map and mark every odd numbered cell as an unvisited cell.
		/// After that, the method will pick a random unvisited cell and start making a corridor until it reaches a dead end.
		/// Once it reaches a dead end the method will look for previously visited cells and seek unvisited neighbors.
		/// Once it reaches a new unvisited neighbor, it will start making a corridor again. This will repeat until all cells are visited.
		/// </summary>
		/// <param name='map'>
		/// The map to make a maze in.
		/// </param>
		/// <param name='type_Path'>
		/// The type of the "walkable" part of the maze.
		/// </param>
		/// <param name='type_Abyss'>
		/// The type_of the "obstacle" part of the maze.
		/// </param>
		public static void CreateMaze(Map map, string type_Path, string type_Abyss)
		{
			//Debug.Log("Creating maze.");
			List<Cell> listOfMazeACells = new List<Cell>();
			List<Cell> listOfMazeBCells = new List<Cell>();
			
			//Set every cell on odd numbered x and y coordinates as a B type_Path cell.
			for(int i = 1; i < map.size_X; i+=2)
			{
				for(int j = 1; j < map.size_Y; j+=2)
				{
					Cell tempCell = map.cellsOnMap[i,j];
					if(tempCell.type == type_Abyss) //This is usually Abyss or Empty cells.
					{
						tempCell.SetCellType("Maze_B");
						tempCell.SetCellAddress(i,j);
						listOfMazeBCells.Add(tempCell);
					}
				}
			}
			
			//Get a B type cell and turn it into A. This is your starting cell.
			int r = Random.Range(0, listOfMazeBCells.Count);
			Cell firstCell = listOfMazeBCells[r];
			listOfMazeBCells.RemoveAt(r);
			firstCell.SetCellType("Maze_A");
			listOfMazeACells.Add(firstCell);
			
			int currentCell_X = firstCell.x;
			int currentCell_Y = firstCell.y;
			
			//Traverse until all B cells are marked as A.
			while( GetCountOfCellType("Maze_B", map) > 0)
			{
				//Pick a random direction from your current cell.
				DirectionManager.Direction currentDirection = map.cellsOnMap[currentCell_X,currentCell_Y].dirMan.GetNextDirection();
				
				//Try to find a random cell you turned into A already and see if that one has any directions available.
				while(currentDirection == DirectionManager.Direction.NONE)
				{			
					r = Random.Range(0, listOfMazeACells.Count);
					Cell randomCell = listOfMazeACells[r];			
					currentDirection = listOfMazeACells[r].dirMan.GetNextDirection();
					if(currentDirection == DirectionManager.Direction.NONE)
					{
						listOfMazeACells.RemoveAt(r);
					}
					currentCell_X = randomCell.x;
					currentCell_Y = randomCell.y;
				}
				
				int targetCell_X = currentCell_X;
				int targetCell_Y = currentCell_Y;
				
				switch(currentDirection)
				{
				case DirectionManager.Direction.North:
					targetCell_Y = currentCell_Y + 2;
					break;
				case DirectionManager.Direction.East:
					targetCell_X = currentCell_X + 2;
					break;
				case DirectionManager.Direction.West:
					targetCell_X = currentCell_X - 2;
					break;
				case DirectionManager.Direction.South:
					targetCell_Y = currentCell_Y - 2;
					break;
				}
				
				//	1 - if the cell on target destination is NOT in bounds of map, pick next destination.
				//  2 - if the cell on that destination is a violated cell, pick next destination.
	            //  3 - 1 and 2 are false, which means there exists an available NONViolated cell to lay a path towards.
	
			    if (targetCell_X < 1 || targetCell_Y < 1 ||
			        targetCell_X >= map.size_X - 1 || targetCell_Y >= map.size_Y - 1
			        || map.cellsOnMap[targetCell_X, targetCell_Y].type != "Maze_B") continue;
				
			    int cellInMid_X = currentCell_X;
			    int cellInMid_Y = currentCell_Y;
			    if(targetCell_X > currentCell_X) cellInMid_X++;
			    if(targetCell_X < currentCell_X) cellInMid_X--;
			    if(targetCell_Y > currentCell_Y) cellInMid_Y++;
			    if(targetCell_Y < currentCell_Y) cellInMid_Y--; 
			    map.cellsOnMap[cellInMid_X, cellInMid_Y].SetCellType(type_Path);
					
			    //Make target the current cell.
			    Cell targetCell = map.cellsOnMap[targetCell_X,targetCell_Y];
			    targetCell.SetCellType("Maze_A");
				
			    //listOfMazeBCells.RemoveAll(temp => temp.type == "Maze_A");
				for (int i = listOfMazeBCells.Count-1; i > 0; i--) 
				{
					if(listOfMazeBCells[i].type == "Maze_A") 
					{
						listOfMazeBCells.RemoveAt(i);
						break;	
					}
				}
				
			    listOfMazeACells.Add(targetCell);
			    currentCell_X = targetCell_X;
			    currentCell_Y = targetCell_Y;
			}
			
			SetCellsOfTypeAToB(map, "Maze_A", type_Path); //typeOfMaze is usually Path.
			//Debug.Log("Created maze.");
		}
		
		/// <summary>
		/// Creates rooms. It gets a random room size with the given parameters.
		/// Then it looks for a suitable location for the room in the map.
		/// Then it places the room if there's enough empty space in there.
		/// If the current address for the room is already occupied, the method will look for other addresses roomRetry many times.
		/// This is why you will not get the exact amount of rooms when you fill in roomFrequency.
		/// The method will repeat the placement of the rooms until it created or given up on the specified number of rooms.
		/// </summary>
		/// <returns>
		/// The list of rooms.
		/// </returns>
		/// <param name='map'>
		/// The map to create the rooms in.
		/// </param>
		/// <param name='type_Wall'>
		/// The "inaccessible" part of the rooms.
		/// </param>
		/// <param name='type_Path'>
		/// The "accessible" part of the rooms.
		/// </param>
		/// <param name='min_X'>
		/// Minimum width of the rooms.
		/// </param>
		/// <param name='max_X'>
		/// Maximum width of the rooms.
		/// </param>
		/// <param name='min_Y'>
		/// Minimum height of the rooms.
		/// </param>
		/// <param name='max_Y'>
		/// Maximum height of the rooms.
		/// </param>
		/// <param name='roomFrequency'>
		/// Target amount of rooms to place.
		/// </param>
		/// <param name='roomRetry'>
		/// The amount of retries when placing a room, before giving up on it.
		/// </param>
		/// <param name='createDoors'>
		/// If set to true, this creates doors for the rooms.
		/// </param>
		public static List<Room> CreateRooms(Map map, string type_Wall, string type_Path, int min_X, int max_X, int min_Y, int max_Y, int roomFrequency, int roomRetry, bool createDoors)
		{
			//Debug.Log("Creating rooms.");
			List<Room> rooms = new List<Room>();
			
			//Checking if we want any rooms in the map
			if(roomFrequency < 1) return rooms;
			
			//Checking if room size is too small or too big to calculate
		    if(min_X < 3 || max_X < 3 ||max_X >= map.size_X)
		    {
		        Debug.LogError("Your values for room size X are too small or too big for map size! Setting room sizes of X to 3");
		        min_X = 3; max_X = 3;
		    }
			if(min_Y < 3 || max_Y < 3 || max_Y >= map.size_Y)
		    {
		        Debug.LogError("Your values for room size Y are too small or too big for map size! Setting room sizes of Y to 3");
		        min_Y = 3; max_Y = 3;
		    }
			if(min_X > max_X)
			{
		        Debug.LogError("Your values for room size X are swapped! Reswapping them.");
				int tempX = min_X;
				min_X = max_X;
				max_X = tempX;
			}
			if(min_Y > max_Y)
			{
		        Debug.LogError("Your values for room size Y are swapped! Reswapping them.");
				int tempY = min_Y;
				min_Y = max_Y;
				max_Y = tempY;
			}
			
			int roomsCreated = 0;
			do
			{
				//Make a random size room
				int currentRoom_X = Random.Range(min_X,max_X);
				int currentRoom_Y = Random.Range(min_Y,max_Y);
	
				//Room size must be odd, force even numbers down
				if(currentRoom_X%2 != 1) currentRoom_X-=1;
				if(currentRoom_Y%2 != 1) currentRoom_Y-=1;
				
				//Pick a random cell for placement:
				//1 - Rooms must not overflow outside of map
				//2 - Rooms must be on even numbers due to maze generation algorithm
				int currentPlacement_X = Random.Range(0, map.size_X - (currentRoom_X+2));
				int currentPlacement_Y = Random.Range(0, map.size_Y - (currentRoom_Y+2));
				if(currentPlacement_X%2 != 0)currentPlacement_X--;
				if(currentPlacement_Y%2 != 0)currentPlacement_Y--;
				
				//Code will look for suitable location for the room this many times
				int retry = roomRetry;
				
				//Check every tile the room will occupy
				//Do not place a room if it's overlapping with Walls
				for(int i = currentPlacement_X; i <= currentPlacement_X+currentRoom_X+2; i++)
				{
					for(int j = currentPlacement_Y; j <= currentPlacement_Y+currentRoom_Y+2; j++)
					{
						if(map.cellsOnMap[i,j].type == type_Wall)
						{
					        if(retry > 0)
					        {
					            retry--;
					            currentPlacement_X = Random.Range(1, map.size_X - (currentRoom_X+2));
					            currentPlacement_Y = Random.Range(1, map.size_Y - (currentRoom_Y+2));
					            if(currentPlacement_X%2 != 0)currentPlacement_X--;
					            if(currentPlacement_Y%2 != 0)currentPlacement_Y--;
					            i = currentPlacement_X;
					            j = currentPlacement_Y;
					        }
					            //Ran out of retries for placing a room. Giving up on this room!
					        else
					        {
					            //Breaking the for loops.
					            i = currentPlacement_X+currentRoom_X+2;
					            j = currentPlacement_Y+currentRoom_Y+2;	
					        }
						}
					}
				}
				
				//We didn't run out of retries. Place the room!
				if(retry != 0)
				{
					//Save a reference to this room.
					int roomStart_X = currentPlacement_X;
					int roomEnd_X = currentPlacement_X+currentRoom_X+1;
					int roomStart_Y = currentPlacement_Y;
					int roomEnd_Y = currentPlacement_Y+currentRoom_Y+1;
					Room tempRoom = new Room(roomStart_X, roomStart_Y, roomEnd_X, roomEnd_Y);
					rooms.Add(tempRoom);
					
					//Assign cells
					for(int i = currentPlacement_X; i < currentPlacement_X+currentRoom_X+2; i++)
					{
						for(int j = currentPlacement_Y; j < currentPlacement_Y+currentRoom_Y+2; j++)
						{
							if(i == currentPlacement_X 
								|| i == currentPlacement_X+currentRoom_X+1
								|| j == currentPlacement_Y
								|| j == currentPlacement_Y+currentRoom_Y+1)
							{
								map.cellsOnMap[i,j].SetCellType(type_Wall); //Perimeter, usually wall.
							}
							else 
							{
								map.cellsOnMap[i,j].SetCellType(type_Path); //Interior, usually path.
							}
						}
					}
					//HARDCODED: This should be taken out of this. It should be called to work through list of rooms a map has.
					if(createDoors) CreateDoors("Door", map, tempRoom, 1);
					
				}
				
				roomsCreated++;
	
			} while (roomsCreated < roomFrequency);
			map.Rooms = rooms;
			return rooms;
			//Debug.Log("Created rooms.");
		}
		
		public static void CreateDoors(string type_Door, Map map, int numOfDoor)
		{
			
			foreach (Room r in map.Rooms)
				CreateDoors(type_Door, map, r, numOfDoor);
		}
		
		/// <summary>
		/// Creates doors in a room. This method takes a starting address x y and a room width and length.
		/// The method will scout the location and find a suitable cell to place numOfDoors many doors.
		/// </summary>
		/// <param name='type_Door'>
		/// The type of cell used to create doors.
		/// </param>
		/// <param name='map'>
		/// The map that the doors should be put into.
		/// </param>
		/// <param name='x'>
		/// The x coordinate of the room to put doors in.
		/// </param>
		/// <param name='y'>
		/// The y coordinate of the room to put doors in.
		/// </param>
		/// <param name='width'>
		/// The width of the room to put doors in.
		/// </param>
		/// <param name='height'>
		/// The height of the room to put doors in.
		/// </param>
		/// <param name='numOfDoors'>
		/// Number of doors to create.
		/// </param>
		public static void CreateDoors(string type_Door, Map map, Room room, int numOfDoors)
		{
			int x = room.RoomStart_X;
			int y = room.RoomStart_Y;
			int width = room.RoomEnd_X;
			int height = room.RoomEnd_Y;
			if(numOfDoors < 1)
			{
				Debug.Log ("You chose to have no doors to any of your rooms. Forcing 1 door per room.");
				numOfDoors = 1;
			}
		    if(numOfDoors > (((width-x)*2)+((height-y)*2))) 
		    {
		        Debug.LogError("Too many doors allocated for this room. Forcing it to have one door.");
		        numOfDoors = 1;
		    }
		    if(x >= width || y >= height) 
		    {
		        Debug.LogError("Coordinates are too small or equal to one another to place doors in the address.");
		        return;
		    }
			
			//Following for loops will put all door candidates in this list.
			List<Cell> perimeterCells = new List<Cell>();
			
			for(int i = x; i <= width; i++)
			{
				for(int j = y; j <= height; j++)
				{
					if((numOfDoors == 0) ||                                           		//No doors are available.
	                    (i == 0 || j == 0 || i == map.size_X-1 || j == map.size_Y-1) ||       //No doors on edges.
				        ((i%2 == 0 && j%2 == 0) || (i%2 == 1 && j%2 == 1)) ||        		//For doors i must be odd when j is even and vice versa.
				        (!(i == x || i == width || j == y || j == height)) || 				//Forcing to traverse on the walls only.
				        (((i == x) && (j == y || j == height)) || ((i == width) && (j == y || j == height)))) {continue;} //Skip corners of the room.
					
				        perimeterCells.Add(map.cellsOnMap[i,j]); 							//If all conditions above are matched, then this is a good tile for placing a door on.
				}
			}
			
			//So that we don't go through the for loop setting cells we've set already.
			if(numOfDoors > perimeterCells.Count) numOfDoors = perimeterCells.Count;
			
			while(numOfDoors > 0 && perimeterCells.Count > 0)
			{
				numOfDoors--;
				int i = Random.Range(0,perimeterCells.Count);
				perimeterCells[i].SetCellType(type_Door);
				perimeterCells.RemoveAt(i);
			}
		}
		
		/// <summary>
		/// Closes the dead end cells. This seemingly trick method is there for one reason.
		/// When you use CreateMaze to make corridors you end up with a lot of dead en corridors that lead nowhere.
		/// This method will find those corridors and close them one by one.
		/// TODO: Optimize this method by following the corridors trails and not scanning for deadends every single step.
		/// </summary>
		/// <param name='map'>
		/// The map to alter.
		/// </param>
		/// <param name='type_Wall'>
		/// The types of cells that are "impassable".
		/// </param>
		/// <param name='type_Path'>
		/// The types of cells that are "passable".
		/// </param>
		public static void CloseDeadEndCells(Map map, string type_Wall, string type_Path)
		{
			List<Cell> deadEndCells = GetDeadEndCells(map, type_Wall, type_Path);
			while(deadEndCells.Count > 0)
			{
				foreach(Cell c in deadEndCells)
				{
					c.SetCellType(type_Wall);
				}
				deadEndCells = GetDeadEndCells(map, type_Wall, type_Path);
			}
		}
		
		/// <summary>
		/// Gets list of cells that have 3 or more impassable cells (cells of type_Wall) around them in all four directions on NEWS.
		/// </summary>
		/// <returns>
		/// The dead end cells.
		/// </returns>
		/// <param name='map'>
		/// The map to find the dead end cells of.
		/// </param>
		/// <param name='type_Wall'>
		/// The type of cell that is "impassable".
		/// </param>
		/// <param name='type_Path'>
		/// The type of cell that is "passable".
		/// </param>
		public static List<Cell> GetDeadEndCells(Map map, string type_Wall, string type_Path)
		{
			List<Cell>  deadEndCells = new List<Cell>();
			for(int i = 1; i < map.size_X-1; i++)
			{
				for(int j = 1; j < map.size_Y-1; j++)
				{
					int wallCountOnNEWS = 0;
					Cell c = map.cellsOnMap[i,j];
					////cell.dirMan.Reset();
					if(map.cellsOnMap[i,j].type == type_Path)
					{
						//Reset all directions according to walls.
						if(map.cellsOnMap[i-1,j].type == type_Wall) wallCountOnNEWS++;
						else c.dirMan.Visit(c.dirMan.west);
						if(map.cellsOnMap[i+1,j].type == type_Wall) wallCountOnNEWS++;
						else c.dirMan.Visit(c.dirMan.east);
						if(map.cellsOnMap[i,j+1].type == type_Wall) wallCountOnNEWS++;
						else c.dirMan.Visit(c.dirMan.north);
						if(map.cellsOnMap[i,j-1].type == type_Wall) wallCountOnNEWS++;
						else c.dirMan.Visit(c.dirMan.south);
						
						if(wallCountOnNEWS == 3) 
						{
							deadEndCells.Add(c);
						}
					}
				}
			}
			return deadEndCells;
		}
	
		/// <summary>
		/// Places stairs in a map. This method will get a stairCount and place that many staircases in the given map.
		/// At this point you can't define where the staircases go.
		/// </summary>
		/// <param name='map'>
		/// The map to place stairs in.
		/// </param>
		/// <param name='stairCount'>
		/// The number of stairs to place.
		/// </param>
		/// <param name='type_Path'>
		/// The type of cells that are free to place stairs on.
		/// </param>
		/// <param name='type_Stairs'>
		/// The type that represent stairs.
		/// </param>
		public static void PlaceStairs(Map map, int stairCount, string type_Path, string type_Stairs)
		{
			if(stairCount == 0) return;
			
			int placedCount = 0;
			List<Cell> availableCells = new List<Cell>();
			if (map.Rooms.Count > 0)
			{
				foreach (Room r in map.Rooms)
					availableCells.AddRange(r.GetCellsInRoom(map).FindAll(p => p.type == type_Path));
			}
			else
				availableCells = GetListOfCellType(type_Path, map);
			
			//Go through all available cells and remove one at a time.
			//If the cell has 8 empty cells around it then place stairs.
			if (availableCells.Count <= 0)
				return;
			do{
				int randomCell = Random.Range(0,availableCells.Count-1);
				Cell c = availableCells[randomCell];
				availableCells.RemoveAt(randomCell);
	
				List<Cell> neighbours = FindNeighbourCells(map, c.x, c.y, true, false);
				if(neighbours.FindAll(n => n.type == type_Path).Count == 8 )
				{
					c.SetCellType(type_Stairs);
					placedCount++;
				}
			} while (placedCount != stairCount && availableCells.Count > 0);
		}
	
		/// <summary>
		/// Reduces the U corridors in for example a maze. 
		/// It will shorten cells that are wall in center and have 7 paths around them.
		/// This is used for turning a maze into hallways between rooms in a dungeon.
		/// </summary>
		/// <param name='map'>
		/// The map to alter.
		/// </param>
		/// <param name='type_Wall'>
		/// The type of cell that defines a wall.
		/// </param>
		/// <param name='type_Path'>
		/// The type of cells that defines passable terrain.
		/// </param>
		/// <param name='repeat'>
		/// How much times to repeat this algorithm.
		/// </param>
		public static void ReduceUCorridors(Map map, string type_Wall, string type_Path, int repeat)
		{
			if(repeat == 0) return;
			List<Cell> uCorridorCells = GetUCorridorCells(map, type_Wall, type_Path);
			for(int i = 0; i < uCorridorCells.Count; i++)
			{
				Cell c = uCorridorCells[i];
	
				//Does it have 7 paths around it?
				bool fixable = true;
				int pathCount = 0;
				int wallCellX = 0;
				int wallCellY = 0;
				for(int ii = c.x-1; ii <= c.x+1; ii++)
				{
					for(int jj = c.y-1; jj <= c.y+1; jj++)
					{
						if( ii < 0 || jj < 0 || ii > map.size_X-1 || jj > map.size_Y-1 || (ii == c.x && jj == c.y)) continue;
						if(map.cellsOnMap[ii,jj].type == type_Path) pathCount++;
						else if(map.cellsOnMap[ii,jj].type == type_Wall)
						{
							wallCellX = ii;
							wallCellY = jj;
						}
					}
				}
				
				//Do the corner paths of U shape have paths connected to them? We don't want to mess a multi connected corridor.
				//If there are multiple connections to the corner pieces of U, the fixable = false;
				if(pathCount == 7)
				{
					//According to its orientation, check the corner of U.
					//Case for wallCell under center cell.
					if(
						(wallCellX == c.x && wallCellY < c.y) &&
					    (!map.cellsOnMap[c.x-2, c.y+1].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x-1, c.y+2].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x+1, c.y+2].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x+2, c.y+1].type.Equals(type_Wall))
					   )
					{
						fixable = false;	
					}
					//Case for wallCell to the left of center cell.
					else if(
						(wallCellX < c.x && wallCellY == c.y) &&
					    (!map.cellsOnMap[c.x+2, c.y-1].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x+1, c.y-2].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x+1, c.y+2].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x+2, c.y+1].type.Equals(type_Wall))
					  )
					{
						fixable = false;	
					}
					//Case for wallCell above center cell.
					else if(
						(wallCellX == c.x && wallCellY > c.y) &&
					    (!map.cellsOnMap[c.x-2, c.y-1].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x-1, c.y-2].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x+1, c.y-2].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x+2, c.y-1].type.Equals(type_Wall))
					  )
					{
						fixable = false;	
					}
					//Case for wallCell to the right of center cell.
					else if(
						(wallCellX > c.x && wallCellY == c.y) &&
					    (!map.cellsOnMap[c.x-2, c.y-1].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x-1, c.y-2].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x-1, c.y+2].type.Equals(type_Wall) ||
					     !map.cellsOnMap[c.x-2, c.y+1].type.Equals(type_Wall))
					  )
					{
						fixable = false;	
					}
				}
				else fixable = false;
				
				if(fixable)
				{
					if(wallCellX == c.x && wallCellY < c.y)
					{
						//Turn part of U into wall.
						map.cellsOnMap[c.x-1, c.y].SetCellType(    type_Wall ); //West
						map.cellsOnMap[c.x-1, c.y+1].SetCellType(  type_Wall ); //Northwest
						map.cellsOnMap[c.x, c.y+1].SetCellType(    type_Wall ); //North
						map.cellsOnMap[c.x+1, c.y+1].SetCellType(  type_Wall ); //Northeast
						map.cellsOnMap[c.x+1, c.y].SetCellType(    type_Wall ); //East
						//mapArray[c.x+1, c.y-1].SetCell(  type_Wall ); //Southeast
						map.cellsOnMap[c.x, c.y-1].SetCellType(    type_Path ); //South
						//mapArray[c.x-1, c.y-1].SetCell(  type_Wall ); //Southwest
					}
					else if(wallCellX < c.x && wallCellY == c.y)
					{
						//Turn part of U into wall.
						map.cellsOnMap[c.x-1, c.y].SetCellType(    type_Path ); //West
						//mapArray[c.x-1, c.y+1].SetCell(  type_Wall ); //Northwest
						map.cellsOnMap[c.x, c.y+1].SetCellType(    type_Wall ); //North
						map.cellsOnMap[c.x+1, c.y+1].SetCellType(  type_Wall ); //Northeast
						map.cellsOnMap[c.x+1, c.y].SetCellType(    type_Wall ); //East
						map.cellsOnMap[c.x+1, c.y-1].SetCellType(  type_Wall ); //Southeast
						map.cellsOnMap[c.x, c.y-1].SetCellType(    type_Wall ); //South
						//mapArray[c.x-1, c.y-1].SetCell(  type_Wall ); //Southwest
					}
					else if(wallCellX == c.x && wallCellY > c.y)
					{
						//Turn part of U into wall.
						map.cellsOnMap[c.x-1, c.y].SetCellType(    type_Wall ); //West
						//mapArray[c.x-1, c.y+1].SetCell(  type_Wall ); //Northwest
						map.cellsOnMap[c.x, c.y+1].SetCellType(    type_Path ); //North
						//mapArray[c.x+1, c.y+1].SetCell(  type_Wall ); //Northeast
						map.cellsOnMap[c.x+1, c.y].SetCellType(    type_Wall ); //East
						map.cellsOnMap[c.x+1, c.y-1].SetCellType(  type_Wall ); //Southeast
						map.cellsOnMap[c.x, c.y-1].SetCellType(    type_Wall ); //South
						map.cellsOnMap[c.x-1, c.y-1].SetCellType(  type_Wall ); //Southwest
					}
					else if(wallCellX > c.x && wallCellY == c.y)
					{
						//Turn part of U into wall.
						map.cellsOnMap[c.x-1, c.y].SetCellType(    type_Wall ); //West
						map.cellsOnMap[c.x-1, c.y+1].SetCellType(  type_Wall ); //Northwest
						map.cellsOnMap[c.x, c.y+1].SetCellType(    type_Wall ); //North
						//mapArray[c.x+1, c.y+1].SetCell(  type_Wall ); //Northeast
						map.cellsOnMap[c.x+1, c.y].SetCellType(    type_Path ); //East
						//mapArray[c.x+1, c.y-1].SetCell(  type_Wall ); //Southeast
						map.cellsOnMap[c.x, c.y-1].SetCellType(    type_Wall ); //South
						map.cellsOnMap[c.x-1, c.y-1].SetCellType(  type_Wall ); //Southwest
					}
				}
			}
			
			ReduceUCorridors(map, type_Wall, type_Path, repeat-1);
			
		}
		
		/// <summary>
		/// Finds cells of corridors that are U shaped. 
		/// These are cells that are wall in center and have 7 paths around them.
		/// </summary>
		/// <returns>
		/// The cells that are part of a U corridor.
		/// </returns>
		/// <param name='map'>
		/// The map to get the U-corridor cells of.
		/// </param>
		/// <param name='type_Wall'>
		/// The type of cell that defines a wall.
		/// </param>
		/// <param name='type_Path'>
		/// The type of cell that defines passable terrain.
		/// </param>
		public static List<Cell> GetUCorridorCells(Map map, string type_Wall, string type_Path)
		{
			List<Cell>  uCorridorCells = new List<Cell>();
			for(int i = 0; i < map.size_X; i++)
			{
				for(int j = 0; j < map.size_Y; j++)
				{
					if(map.cellsOnMap[i,j].type != type_Wall) continue;
					int pathCount = 0;
					for(int ii = i-1; ii <= i+1; ii++)
					{
						for(int jj = j-1; jj <= j+1; jj++)
						{
						    if( ii < 0 || jj < 0 || ii > map.size_X-1 || jj > map.size_Y-1 ) continue;
						    if(map.cellsOnMap[ii,jj].type == type_Path) pathCount++;
						}
					}
					if(pathCount == 7) 
					{	
						uCorridorCells.Add(map.cellsOnMap[i,j]);
					}
				}
			}
			return uCorridorCells;
		}
		
		/// <summary>
		/// Frames the map with a given cell type and thickness.
		/// </summary>
		/// <param name='map'>
		/// The map to frame.
		/// </param>
		/// <param name='type_Wall'>
		/// Type_of cell of the frame.
		/// </param>
		/// <param name='thickness'>
		/// Thickness of the frame.
		/// </param>
		public static void FrameMap(Map map, string type_Wall, int thickness)
		{
			for(int i = 0; i < map.size_X; i++)
			{
				for(int j = 0; j < map.size_Y; j++)
				{
					if( !(j > thickness -1
						&& j < map.size_Y-thickness
						&& i > thickness -1
						&& i < map.size_X-thickness
						))
						map.cellsOnMap[i,j].SetCellType(type_Wall);
				}
			}
		}
		
		/// <summary>
		/// Elongate a specified type of cells in a map, horizontally or vertically, by a random amount.
		/// </summary>
		/// <param name='map'>
		/// The map to alter
		/// </param>
		/// <param name='type'>
		/// The type of cells to elongate
		/// </param>
		/// <param name='minLength'>
		/// Minimum length to elongate the cells
		/// </param>
		/// <param name='maxLength'>
		/// Maximum length to elongate the cells
		/// </param>
		/// <param name='horizontal'>
		/// If set to true, this will elongate horizontally. Otherwise it will elongate vertically
		/// </param>
		public static void Elongate(Map map, string type, int minLength, int maxLength, bool horizontal)
		{
			//Get list of all cells of type type.
			List<Cell> cells = new List<Cell>();
			cells = GetListOfCellType(type, map);
			
			//Go through every cell in the list.
			foreach(Cell c in cells)
			{
				//Get a random integer between minLength and maxLength
				int length = Random.Range(minLength, maxLength);
				
				//Without ending out of bounds elongate the cell with the random value.
				if(horizontal)
				{
					for (int i = c.address.x; i < c.address.x+length; i++) 
					{
						if(i > map.size_X-1) break;
						else map.cellsOnMap[i, c.address.y].SetCellType(type);		
					}
				}
				else
				{
					for (int i = c.address.y; i < c.address.y+length; i++) 
					{
						if(i > map.size_Y-1) break;
						else map.cellsOnMap[c.address.x, i].SetCellType(type);		
					}	
				}
				
			}
		}
		
		
		
		/// <summary>
		/// Finds isolated areas of a specific cell type and connects them.
		/// </summary>
		/// <param name='m'>
		/// The map containing the areas.
		/// </param>
		/// <param name='type'>
		/// The type of cell to look for when determining isolated areas.
		/// </param>
		/// <param name='diagonal'>
		/// When set to true, the isolated areas will be connected diagonally.
		/// Otherwise the will be connected with "L-shaped" corridors.
		/// </param>
		/// <param name='thickness'>
		/// The thickness of the connecting corridors.
		/// </param>
		public static void ConnectIsolatedAreas(Map m, string type, bool diagonal, int thickness)
		{
			
			List<Room> isolatedAreas = GetIsolatedAreas(m, type);
			if(isolatedAreas.Count < 2) return;
			for (int i = 0; i < isolatedAreas.Count-1; i++)
			{
				int randomPos_A = Random.Range(0, isolatedAreas[i].cellsInRoom.Count -1);
				Cell Cell_A = isolatedAreas[i].cellsInRoom[randomPos_A];
				int randomPos_B = Random.Range(0, isolatedAreas[i +1].cellsInRoom.Count -1);
				Cell Cell_B = isolatedAreas[i+1].cellsInRoom[randomPos_B];
				ConnectTwoCells(Cell_A, Cell_B, m, type, diagonal, thickness);
			}
		}
		
		/// <summary>
		/// Gets the isolated areas.
		/// </summary>
		/// <returns>
		/// A list of rooms, containing the isolated areas.
		/// </returns>
		/// <param name='m'>
		/// The map to get the isolated areas of.
		/// </param>
		/// <param name='type'>
		/// The type of cell to look for when determining isolated areas.
		/// </param>
		public static List<Room> GetIsolatedAreas( Map m, string type ) //Should return list of areas.
		{
			//Make a list of isolatedCaves.
			List<Room> isolatedAreas = new List<Room>();
			
			//Get cells type type.
			List<Cell> cells = new List<Cell>();
			cells = GetListOfCellType(type, m);
			
			//While cells is not empty.
			while(cells.Count > 0)
			{
				//Make a list of flooded cells in an isolated cave.
				List<Cell> floodedCells = new List<Cell>();
				
				//Flood starting from a random cell and flood all neighbours!
				//Save all flooded locations to floodedCells as you go.
				Flood(cells[0],m, ref floodedCells, ref cells);
				
				//Make a room to represent an isolated area and save it to our list of isolated areas.
				Room isolatedArea = new Room(floodedCells);
				isolatedAreas.Add(isolatedArea);
				
			}
			if (m.Rooms.Count <= 0)
				m.Rooms = isolatedAreas;
			m.UnvisitMap();
			return isolatedAreas;
		}
		
		/// <summary>
		/// Flood the specified c, m, floodedCells and cells.
		/// </summary>
		/// <param name='c'>
		/// C.
		/// </param>
		/// <param name='m'>
		/// M.
		/// </param>
		/// <param name='floodedCells'>
		/// Flooded cells.
		/// </param>
		/// <param name='cells'>
		/// Cells.
		/// </param>
		private static void Flood(Cell c, Map m, ref List<Cell> floodedCells, ref List<Cell> cells )
		{
			//Flood and visit the cell.
			c.dirMan.Visited = true;
			floodedCells.Add(c);
			cells.Remove(c);
			
			//Flood all neighbours of aforementioned cell.
			List<Cell> neighbours = MethodLibrary.FindNeighbourCells(m,c.x,c.y,false,false);
			foreach (Cell n in neighbours)
				if (n.type == c.type && !n.dirMan.Visited)
					Flood(n, m, ref floodedCells, ref cells);
		}
		
		/// <summary>
		/// Enwalls cells of a given type with cells of another type. 
		/// Iterates through all the cells of the given type. For each cell, it looks at the neighbouring cells.
		/// If one of those cells is not the given type or the wall type, it changes to the wall type.
		/// </summary>
		/// <param name='map'>
		/// The map to alter.
		/// </param>
		/// <param name='type_Path'>
		/// The type of cell that needs to be "walled".
		/// </param>
		/// <param name='type_Wall'>
		/// The cell type that defines a wall.
		/// </param>
		public static void EnwallCells(Map map, string type_Path, string type_Wall)
		{
			List<Cell> accessibleCells = GetListOfCellType(type_Path, map);
			foreach (Cell c in accessibleCells)
			{
				List<Cell> neighbours = FindNeighbourCells(map, c.x, c.y, true, false);
				for (int i = 0; i < neighbours.Count; i++)
				{
					Cell n = neighbours[i];
					if (n.type != type_Path && n.type != type_Wall)
						n.SetCellType(type_Wall);
				}
			}
		}
		
		public static void RemoveSmallRooms(Map map, string type_Path, string type_Wall, int threshold)
		{
			List<Room> rooms = GetIsolatedAreas(map, type_Path);
			foreach (Room r in rooms)
				FillRoom(map, r, type_Wall, threshold);
		}
		
		/// <summary>
		/// Fills the room with the specified type if the amount of cells is lower than the threshold.
		/// </summary>
		/// <param name='map'>
		/// The map to alter.
		/// </param>
		/// <param name='room'>
		/// The room to fill.
		/// </param>
		/// <param name='type'>
		/// The type of cell to fill the room with.
		/// </param>
		/// <param name='threshold'>
		/// The threshold. If the amount of cells in the room is smaller than the threshold, fill it.
		/// If the threshold is set to -1, it always fills the room.
		/// </param>
		public static void FillRoom(Map map, Room room, string type, int threshold)
		{
			if (room.cellsInRoom == null || room.cellsInRoom.Count <= 0 || (room.cellsInRoom.Count >= threshold && threshold != -1))
				return;
			foreach (Cell c in room.cellsInRoom)
				map.cellsOnMap[c.x, c.y].SetCellType(type);
		}
		
	}
}