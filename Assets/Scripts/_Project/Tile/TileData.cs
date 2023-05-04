using UnityEngine;

namespace _Project.Tile
{
	[System.Serializable]
	public class TileData
	{
		//tiler'lar için gerekli bilgiler
		[Header("** Tile Settings **")] 
		public TileState TileState;
		public MeshRenderer MeshRenderer;
		public Color LightOnColor;
		public Color LightOffColor;
		public Color EmptyColor;
		public float ColorOnOffTime;
		public MatrixInfo MatrixInfo;
	}
	
}
