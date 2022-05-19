using System;
using System.Collections.Generic;
using UnityEngine;
using World.MapGraphicsBuilder.Interfaces;
using World.MapModel.Interfaces;

namespace World.MapGraphicsBuilder.Implementations
{
    class MapGraphicsBuilder : IMapGraphicsBuilder
    {
        private GameObject _cubePrefab;

        public MapGraphicsBuilder(GameObject cubePrefab)
        {
            _cubePrefab = cubePrefab;
        }

        public GameObject Build(ICell[,] cells)
        {
            var parent = new GameObject("_MAP");
            parent.transform.position = Vector3.zero;

            foreach (var cell in cells)
            {
                if (cell.CellType == MapModel.Enums.ECellType.Empty)
                {
                    var view = GameObject.Instantiate(_cubePrefab);
                    view.transform.SetParent(parent.transform);
                    view.transform.position = new Vector3(cell.Position.Y, 0f, cell.Position.X);
                    view.transform.localScale = Vector3.one;
                }
            }

            return parent;
        }
    }
}
