using System.Collections.Generic;
using UnityEngine;
using World.MapModel.Interfaces;

namespace World.MapGraphicsBuilder.Interfaces
{
    public interface IMapGraphicsBuilder
    {
        GameObject Build(ICell[,] cells);
    }
}
