using System.Collections.Generic;
using UnityEngine;

namespace _Project.Tile
{
	public class TileManager : MonoBehaviour
	{
		public static TileManager Instance;

		[SerializeField]
		private TileManagerData _properties;

		private List<Tile> _tiles;

		public MatrixInfo[,] MatrixInfo
		{
			set
			{
				_properties.MatrixHandler.SetMatrix = value;
			}
		}
	
		void Awake()
		{
			if(Instance == null)
			{
				Instance = this;
			}
			else if(Instance != this)
			{
				Destroy(gameObject);
			}
		}

		private void Start()
		{
			BuildTiles();
		}

		private void BuildTiles()
		{
			_tiles=_properties.TileBuilder.BuildRandomTiles();
		}

		public void TileDown(MatrixInfo matrixInfo)
		{
			List<MatrixInfo> matrixNeighbours = _properties.MatrixHandler.GetMyNeighbour(matrixInfo);
		
			foreach (MatrixInfo matrixNeighbour in matrixNeighbours)
			{ ;
				_tiles[_properties.MatrixHandler.GetIndex(matrixNeighbour.Row,matrixNeighbour.Column)].SwitchTileState(true);
			}
		}
	

	}
}
