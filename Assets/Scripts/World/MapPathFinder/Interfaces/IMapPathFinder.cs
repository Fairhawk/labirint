using System.Collections.Generic;
using UnityEngine;
using World.MapModel.Data;
using World.MapModel.Interfaces;

namespace World.MapPathFinder.Interfaces
{
    public interface IMapPathFinder
    {
        IReadOnlyList<Vector3> FindPath(IMap map, Point start, Point end);
    }
}
