using UnityEngine;
using World.MapModel.Data;
using World.MapModel.Enums;
using World.MapModel.Interfaces;

namespace World.MapModel.Implementations
{
    public class Cell : ICell
    {
        public Point Position { get; }
        public ECellType CellType { get; private set; }

        public Cell(int x, int y, ECellType cellType)
        {
            Position = new Point() { X = x, Y = y };
            CellType = cellType;
        }

        public void SetCellType(ECellType cellType)
        {
            CellType = cellType;
        }
    }
}
