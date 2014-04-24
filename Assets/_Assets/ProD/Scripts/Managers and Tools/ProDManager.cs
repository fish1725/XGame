using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ProD
{
	/// <summary>
	/// Pro D manager. The main class for managing all ProD related stuff.
	/// </summary>
	public class ProDManager : Singleton<ProDManager> 
	{
		public WorldMap worldMap;
		public int prefab_Size_X = 1;
		public int prefab_Size_Y = 1;
		public bool instantiatePlayer = false;
		public bool groupTiles;
		
		public enum RenderType {Default, WholeWorldMap/*, AroundAPlayer*/};
		public RenderType rt = RenderType.WholeWorldMap;
		
		public bool UseSeed = false;
	
		private PlayerMovement player = null;
		
		private int _seed = 0;
	    public int Seed
	    {
			get { return _seed;}
			set {
				if(UseSeed)
				{
					_seed = value;
					Random.seed = _seed;
				}
			}
	    }
		
		public void ApplySeed()
		{
			if (UseSeed)
				Random.seed = _seed;
		}

		public enum ThirdAxis {y, z}
		public ThirdAxis thirdAxis = ThirdAxis.y;
		
		/// <summary>
		/// Spawns a player if there is not already one. Will only spawn if EnablePlayer is true
		/// </summary>
		/// <param name='world'>
		/// The world to spawn the player in.
		/// </param>
		public void SetupPlayer()
		{
			if (instantiatePlayer && player == null)
			{
				GameObject playerGO = (GameObject)Instantiate(Resources.Load("Player/PRE_Player"));
				player = playerGO.GetComponent<PlayerMovement>();
			}
		}
		
		/// <summary>
		/// Respawns the player in a specified world. Has to be used if a new world is created to properly respawn the player.
		/// </summary>
		/// <param name='world'>
		/// The world to respawn the player in.
		/// </param>
		public void SpawnPlayer(WorldMap world)
		{
			if (instantiatePlayer)
			{
				player.SetupPlayer(world);
			}
		}
		
		void Start () 
		{
			Materializer.Instance.GroupTiles = groupTiles;
			SetupPlayer();
		}
	}
}
