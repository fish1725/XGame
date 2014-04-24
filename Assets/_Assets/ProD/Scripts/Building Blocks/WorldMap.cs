using UnityEngine;
using System.Collections;

namespace ProD
{
	public class WorldMap
	{
		private int _Size_X = 1;
		public int size_X
	    {
	        get { return _Size_X; }
		}
		
		private int _Size_Y = 1;
		public int size_Y
	    {
	        get { return _Size_Y; }
		}
			
		private Map[,] _Maps;
		public Map[,] maps
	    {
	        get { return _Maps; }
			set { _Maps = value;}
		}
		
		public WorldMap() : this(1,1)
		{
			
		}
		public WorldMap(int _Size_X, int _Size_Y) : base()
		{
			this._Size_X = _Size_X;
			this._Size_Y = _Size_Y;
			_Maps = new Map[_Size_X, _Size_Y];
		}
		
		public int World_Grid_Size_X
		{
			get { 
				if (_Maps == null)
					return 0;
				else
				{
					int tempSizeX = 0;
					for (int i = 0; i < size_X; i++) {
						tempSizeX += _Maps[i,0].size_X;
					}
					return tempSizeX;
				}
			}	
		}
		
		public int World_Grid_Size_Y
		{
			get { 
				if (_Maps == null)
					return 0;
				else
				{
					int tempSizeY = 0;
					for (int i = 0; i < size_Y; i++) {
						tempSizeY += _Maps[0,i].size_Y;
					}
					return tempSizeY;
				}
			}	
		}
		/// <summary>
		/// Gets the world position of an address on a specified map.
		/// </summary>
		/// <returns>
		/// The world position.
		/// </returns>
		/// <param name='map'>
		/// The map that the address is on.
		/// </param>
		/// <param name='mapAddress'>
		/// Address on the map you want the world position of.
		/// </param>
		public Address GetAddressWorldPosition(Map map, Address mapAddress)
		{
			Address result = mapAddress;
			if (_Maps == null)
				return result;
			else
			{
				Address tempAddress = map.addressOnWorldMap;
				int tempX = 0;
				for (int i = 0; i < tempAddress.x; i++)
					tempX += _Maps[i,0].size_X;
				int tempY = 0;
				for (int j = 0; j < tempAddress.y; j++)
					tempY += _Maps[0,j].size_Y;
			
				result = new Address(tempX + mapAddress.x, tempY + mapAddress.y);		
			}
			return result;
		}
	}
}
