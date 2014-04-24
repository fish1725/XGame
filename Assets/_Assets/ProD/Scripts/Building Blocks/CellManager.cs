using UnityEngine;

namespace ProD
{
	//This class is a containter for Cell.cs
	//You should note that Cell.cs doesn't inherit from Monobehaviour, but CellManager does.
	//We do this so we can generate cells on the go without having to create GameObjects for them first.
	public class CellManager : MonoBehaviour
	{
		private Cell _Cell;
		public Cell cell 
		{
			get {
				return this._Cell;
			}
			set {
				_Cell = value;
			}
		}
		
	}
}
