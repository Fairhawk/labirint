using UnityEngine;
using World.MapModel.Data;
using World.MapModel.Enums;

namespace World.MapModel.Interfaces
{
    public interface ICell
    {
        Point Position { get; }
        ECellType CellType { get; }

        void SetCellType(ECellType cellType);
    }
}
