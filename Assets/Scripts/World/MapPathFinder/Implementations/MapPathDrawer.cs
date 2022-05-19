using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using World.MapPathFinder.Interfaces;

namespace World.MapPathFinder.Implementations
{
    public class MapPathDrawer : IMapPathDrawer
    {
        private readonly LineRenderer _lineRenderer;

        public MapPathDrawer()
        {
            _lineRenderer = new GameObject("PathDrawer").AddComponent<LineRenderer>();
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.widthMultiplier = 0.1f;
        }

        public void DrawPath(IReadOnlyList<Vector3> path)
        {
            if (path != null)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.positionCount = path.Count;
                _lineRenderer.SetPositions(path.ToArray());
            }
        }

        public void Clear()
        {
            _lineRenderer.enabled = false;
        }
    }
}
