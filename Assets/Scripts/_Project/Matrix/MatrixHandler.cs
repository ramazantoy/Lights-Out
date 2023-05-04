using System.Collections.Generic;
using UnityEngine;

namespace _Project.Matrix
{
	[System.Serializable]
	public  class MatrixHandler
	{
		public MatrixHandler(MatrixInfo[,] matrixInfos) // Oyun tahtasını bir matris gibi ele alıp arka planda rahat bir şekilde yönetilnesi
		{
			_matrixInfos = matrixInfos;
		}
		private MatrixInfo[,] _matrixInfos;

		public  MatrixInfo[,] Matrix
		{
			set
			{
				_matrixInfos = value;
			}
			get
			{
				return _matrixInfos;
			}
		}

		public List<MatrixInfo> GetMyNeighbourWithCross(MatrixInfo matrixInfo) // Çapraz dahil istenilen matrisin komşularının liste olarak verilmesi
		{
			List<MatrixInfo> myNeighbours = new List<MatrixInfo>();
	
			int rowSize = _matrixInfos.GetLength(0);
			int colSize = _matrixInfos.GetLength(1);

			if (matrixInfo.Row > 0) // üst komşu
			{
				myNeighbours.Add(_matrixInfos[matrixInfo.Row-1, matrixInfo.Column]);

				if (matrixInfo.Column > 0) // üst sol komşu
				{
					myNeighbours.Add(_matrixInfos[matrixInfo.Row-1, matrixInfo.Column-1]);
				}

				if (matrixInfo.Column < colSize-1) // üst sağ komşu
				{
					myNeighbours.Add(_matrixInfos[matrixInfo.Row-1, matrixInfo.Column+1]);
				}
			}

			if (matrixInfo.Row < rowSize-1) // alt komşu
			{
				myNeighbours.Add(_matrixInfos[matrixInfo.Row+1, matrixInfo.Column]);

				if (matrixInfo.Column > 0) // alt sol komşu
				{
					myNeighbours.Add(_matrixInfos[matrixInfo.Row+1, matrixInfo.Column-1]);
				}

				if (matrixInfo.Column < colSize-1) // alt sağ komşu
				{
					myNeighbours.Add(_matrixInfos[matrixInfo.Row+1, matrixInfo.Column+1]);
				}
			}

			if (matrixInfo.Column > 0) // sol komşu
			{
				myNeighbours.Add(_matrixInfos[matrixInfo.Row, matrixInfo.Column-1]);
			}

			if (matrixInfo.Column < colSize-1) // sağ komşu
			{
				myNeighbours.Add(_matrixInfos[matrixInfo.Row, matrixInfo.Column+1]);
			}

			return myNeighbours;
			
		}
	public List<MatrixInfo> GetMyNeighbour(MatrixInfo matrixInfo) // Oyunun kurallarına göre olan komşuların listesinin geri döndüren fonksiyon
	{
		List<MatrixInfo> myNeighbours = new List<MatrixInfo>();

		int rowSize = _matrixInfos.GetLength(0);
		int colSize = _matrixInfos.GetLength(1);

		if (matrixInfo.Row > 0) // üst komşu
		{
			myNeighbours.Add(_matrixInfos[matrixInfo.Row-1, matrixInfo.Column]);
		}

		if (matrixInfo.Row < rowSize-1) // alt komşu
		{
			myNeighbours.Add(_matrixInfos[matrixInfo.Row+1, matrixInfo.Column]);
		}

		if (matrixInfo.Column > 0) // sol komşu
		{
			myNeighbours.Add(_matrixInfos[matrixInfo.Row, matrixInfo.Column-1]);
		}

		if (matrixInfo.Column < colSize-1) // sağ komşu
		{
			myNeighbours.Add(_matrixInfos[matrixInfo.Row, matrixInfo.Column+1]);
		}

		return myNeighbours;
	}

		
		public int GetIndex( int row, int column) // istenilen tile'ın liste üzerinde olan index'inin hesaplanması
		{
			int n = _matrixInfos.GetLength(1);

			int index = row * n + column;

			return index;
		}

		public MatrixInfo GetMatrixInfo(int row, int col) // istenilem matris bilgisi
		{
			return _matrixInfos[row, col];
		}
		public bool CheckMatrix() // oyunun bitip bitmediğinin kontrolü
		{
			int rows = _matrixInfos.GetLength(0);
			int cols = _matrixInfos.GetLength(1);
    
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					if (_matrixInfos[i, j].Value ==1)
					{
						return false;
					}
				}
			}
    
			return true;
		}
		

	}
	

}
