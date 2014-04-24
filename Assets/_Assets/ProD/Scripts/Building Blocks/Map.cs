using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ProD
{
	public class Map 
	{
		private WorldMap _WorldMap;
		public WorldMap worldMap
	    {
	        get { return _WorldMap; }
	        set { _WorldMap = value; }
		}
		
		private string _Theme = "Default";
		public string theme
	    {
	        get { return _Theme; }
	        set { _Theme = value; }
		}
		
		private int _Size_X;
		public int size_X
	    {
	        get { return _Size_X; }
		}
		
		private int _Size_Y;
		public int size_Y
	    {
	        get { return _Size_Y; }
		}
		
		private Address _AddressOnWorldMap = new Address(0,0);
		public Address addressOnWorldMap
		{
			get { return _AddressOnWorldMap;  }
			set { _AddressOnWorldMap = value; }	
		}
		
		private Cell[,] _CellsOnMap;
		public Cell[,] cellsOnMap
	    {
	        get { return _CellsOnMap;  }
			set { _CellsOnMap = value; }
		}
		
		public string[,] cellsOnMapInString
	    {
	        get 
			{ 
				return MethodLibrary.ConvertCellArrayIntoStringArray(this);
			}
		}
		
		private bool _Generated = false;
		public bool generated
	    {
	        get { return _Generated;  }
	        set { _Generated = value; }
		}
		
		private float _WindAngle;
		public float WindAngle
		{
			get{return _WindAngle;}
			set{_WindAngle = value;}
		}
		
		public Cell GetCell(int pos_X, int pos_Y)
		{
			if (pos_X < 0 || pos_X >= size_X || pos_Y < 0 || pos_Y >= size_Y)
				return null;
			else
				return _CellsOnMap[pos_X, pos_Y];
		}
		
		private List<Room> _rooms = new List<Room>();
		public List<Room> Rooms 
		{
			get { return _rooms; }
			set { _rooms = value; }
		}
		
		public Map(){}
		public Map(int _Size_X, int _Size_Y)
		{
			this._Size_X = _Size_X;
			this._Size_Y = _Size_Y;
			
			if(_Size_X > 100 || _Size_Y > 100)
			{
				Debug.LogWarning("You are making quite a big map. Just a friendly reminder that it can take a long time:"
								+ "\n" + " size_X is " + _Size_X + " size_Y is " + _Size_Y);	
			}
			
			if(_Size_X < 4 || size_Y < 4)
			{
				Debug.LogWarning("Values for size_X and size_Y may be too small. Just a friendly reminder of your values:"
								+ "\n" + " size_X is " + _Size_X + " size_Y is " + _Size_Y);
			}
			
			/* All map sizes should be odd numbered due to a maze algorithm we're using. */
			if(_Size_X%2 != 1)
			{
				Debug.LogError("Value for size_X is even. For maze generation methods we recommend odd numbers."
					 			+ "\n" + "I'm gonna +1 your size for you. You may change this through code.");
				this._Size_X++;
			}
			if(_Size_Y%2 != 1)
			{
				Debug.LogError("Value for size_Y is even. For maze generation methods we recommend odd numbers."
					 			+ "\n" + "I'm gonna +1 your size for you. You may change this through code.");
				this._Size_Y++;
			}
			
			_CellsOnMap = new Cell[this._Size_X, this._Size_Y];
			MethodLibrary.ResetCellsOnMap(this, "Abyss");
		}
		
		/// <summary>
		/// Marks all the cells in a map as unvisited. 
		/// Should be called after every method where you visit cells in a map.
		/// </summary>
		public void UnvisitMap()
		{
			for (int j = 0; j < size_Y; j++)
			{
				for (int i = 0; i < size_X; i++) {
					cellsOnMap[i,j].dirMan.Visited = false;
				}
			}
		}
	}
}
