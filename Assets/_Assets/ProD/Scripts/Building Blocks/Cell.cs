using UnityEngine;

namespace ProD
{
	//You should note that this script doesn't inherit from Monobehaviour.
	//We do this so we can generate cells on the go without having to create GameObjects for them first.
	public class Cell
	{
		private Address _Address;
		public Address address
		{ 
			get { return _Address; }
			
		}
		public void SetCellAddress(Address a)
		{
			_Address = a;
		}
		public void SetCellAddress(int x, int y)
		{
			_Address = new Address(x,y);
		}
		public int x
		{
			get{ return address.x;}	
		}
		public int y
		{
			get{ return address.y;}	
		}
		
		//Setting cell's type.
		private string _type = null;
		public string type{ get { return _type; } }
		public void SetCellType(string typeName)
		{
			_type = typeName;
			//Whenever you set a new cell type, the cell will lose its connection to its GameObject.
			//This acts as a flag that will be questioned when refreshing the GameObjects in the scene.
			_MyGameObject = null; 
			
		}
		
		//This is set if we are replacing the map with prefabs.
		private GameObject _MyGameObject;
		public GameObject myGameObject
		{ 
			get { return _MyGameObject; }
			set { _MyGameObject = value; }
		}
		
		private CellManager _CM;
		public CellManager cm
		{ 
			get { return _CM; }
			set { _CM = value; }
		}
		
		private bool _Illuminated = false;
		public bool illuminated
		{ 
			get { return _Illuminated; }
			set { _Illuminated = value; }
		}
		
		public DirectionManager dirMan;
		
		public Cell(Address a)
		{
			SetCellAddress(a);
			dirMan = new DirectionManager();
		}
		
		public Color colorValue = Color.gray;
	}
}