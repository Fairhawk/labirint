using System.Collections.Generic;
using UnityEngine;

namespace World.MapModel.Interfaces
{
    public interface IMap
    {
        int Widht { get; }
        int Height { get; }

        ICell GetCell(int x, int y);
        ICell[,] GetCells();
    }
}
