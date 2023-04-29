using UnityEngine;

namespace _Project.Tile
{
	[System.Serializable]
	public class TileData
	{
		[Header("** Tile Settings **")] 
		public TileState TileState;
		public MeshRenderer MeshRenderer;
		public Color LightOnColor;
		public Color LightOffColor;
		public float ColorOnOffTime;
		public MatrixInfo MatrixInfo;
	}
	
}
