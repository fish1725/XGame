using UnityEngine;
using System.Collections;

namespace ProD
{
	public class Address
	{
		private int _X;
		public int x {
			get {
				return this._X;
			}
			set {
				_X = value;
			}
		}
		
		private int _Y;
		public int y {
			get {
				return this._Y;
			}
			set {
				_Y = value;
			}
		}
			
		public Address(int _X, int _Y)
		{
			this._X = _X;
			this._Y = _Y;
		}
		
		public bool Equals(Address a)
		{
			bool condition_0 = false;
			if(_X == a.x) condition_0 = true;
			bool condition_1 = false;
			if(_Y == a.y) condition_1 = true;
			
			if(condition_0 && condition_1) return true;
			else return false;
		}
		
		public DirectionManager.Direction IsLocatedOn_AccordingToAddress(Address a)
		{
			int diff_X = _X - a.x;
			int diff_Y = _Y - a.y;
	
			if(diff_X > 0)
			{
					 if(diff_Y >  0) return DirectionManager.Direction.NorthEast;
				else if(diff_Y <  0) return DirectionManager.Direction.SouthEast;
				else if(diff_Y == 0) return DirectionManager.Direction.East;
			}
			else if(diff_X < 0)
			{
					 if(diff_Y >  0) return DirectionManager.Direction.NorthWest;
				else if(diff_Y <  0) return DirectionManager.Direction.SouthWest;
				else if(diff_Y == 0) return DirectionManager.Direction.West;
			}
			else if(diff_X == 0)
			{
					 if(diff_Y >  0) return DirectionManager.Direction.North;
				else if(diff_Y <  0) return DirectionManager.Direction.South;
				else if(diff_Y == 0) return DirectionManager.Direction.NONE;
			}
			return DirectionManager.Direction.NONE;
		}
	}
}
