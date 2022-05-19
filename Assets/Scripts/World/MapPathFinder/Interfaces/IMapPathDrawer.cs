using System.Collections.Generic;
using UnityEngine;

namespace World.MapPathFinder.Interfaces
{
    public interface IMapPathDrawer
    {
        void DrawPath(IReadOnlyList<Vector3> path);
        void Clear();
    }
}
