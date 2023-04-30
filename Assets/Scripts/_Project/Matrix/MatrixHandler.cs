using System.Collections.Generic;
using UnityEngine;

namespace _Project.Matrix
{
	[System.Serializable]
	public  class MatrixHandler
	{
		private MatrixInfo[,] _matrixInfos;

		public  MatrixInfo[,] SetMatrix
		{
			set
			{
				_matrixInfos = value;
			}
		}

		public List<MatrixInfo> GetMyNeighbour(MatrixInfo matrixInfo)
		{
			List<MatrixInfo> myNeighbours = new List<MatrixInfo>();
	
			int rowSize = _matrixInfos.GetLength(0);
			int colSize = _matrixInfos.GetLength(1);

			if (matrixInfo.Row > 0) // check top neighbor
			{
				myNeighbours.Add(_matrixInfos[matrixInfo.Row-1, matrixInfo.Column]);

				if (matrixInfo.Column > 0) // check top-left neighbor
				{
					myNeighbours.Add(_matrixInfos[matrixInfo.Row-1, matrixInfo.Column-1]);
				}

				if (matrixInfo.Column < colSize-1) // check top-right neighbor
				{
					myNeighbours.Add(_matrixInfos[matrixInfo.Row-1, matrixInfo.Column+1]);
				}
			}

			if (matrixInfo.Row < rowSize-1) // check bottom neighbor
			{
				myNeighbours.Add(_matrixInfos[matrixInfo.Row+1, matrixInfo.Column]);

				if (matrixInfo.Column > 0) // check bottom-left neighbor
				{
					myNeighbours.Add(_matrixInfos[matrixInfo.Row+1, matrixInfo.Column-1]);
				}

				if (matrixInfo.Column < colSize-1) // check bottom-right neighbor
				{
					myNeighbours.Add(_matrixInfos[matrixInfo.Row+1, matrixInfo.Column+1]);
				}
			}

			if (matrixInfo.Column > 0) // check left neighbor
			{
				myNeighbours.Add(_matrixInfos[matrixInfo.Row, matrixInfo.Column-1]);
			}

			if (matrixInfo.Column < colSize-1) // check right neighbor
			{
				myNeighbours.Add(_matrixInfos[matrixInfo.Row, matrixInfo.Column+1]);
			}

			return myNeighbours;

			return myNeighbours;
		}
		
		public int GetIndex( int row, int column)
		{
			int m = _matrixInfos.GetLength(0);
			int n = _matrixInfos.GetLength(1);

			int index = row * n + column;

			return index;
		}

	}
	

}
