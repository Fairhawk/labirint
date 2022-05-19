using System.Collections.Generic;
using UnityEngine;
using World.MapGenerator.Interfaces;
using World.MapModel.Enums;
using World.MapModel.Implementations;
using World.MapModel.Interfaces;

namespace World.MapGenerator.Implementations
{
    public class MapGenerator : IMapGenerator
    {
        // Значение инкапсулированы в константы, т.к являются уникальными для данного алгоритма.
        private const float FillPercent = 0.4f;
        private const int MinIslandSize = 3;
        private const int MaxIslandSize = 6;

        public IMap GenerateMap(int widht, int height)
        {
            var cells = new ICell[widht, height];
            var count = (int)(Mathf.Max(widht, height) * FillPercent);

            Fill(cells, 0, 0, widht, height, ECellType.Wall);
            var islandCenters = FillIslands(cells, count: count, minSize: MinIslandSize, maxSize: MaxIslandSize);
            FillTransitions(cells, islandCenters);
            CreateHoles(cells, islandCenters);

            return new Map(widht, height, cells);
        }

        private IReadOnlyList<Vector2> FillIslands(ICell[,] cells, int count, int minSize, int maxSize)
        {
            var centers = new List<Vector2>();
            var rows = cells.GetUpperBound(0) + 1;
            var columns = cells.Length / rows;

            for (int i = 0; i < count; i++)
            {
                var islandWidth = Random.Range(minSize, maxSize);
                var islandHeiht = Random.Range(minSize, maxSize);

                var x = Random.Range(0, rows - islandWidth);
                var y = Random.Range(0, columns - islandHeiht);

                Fill(cells, x, y, islandWidth, islandHeiht, ECellType.Empty);

                var centerCellX = x + islandWidth / 2;
                var centerCellY = y + islandHeiht / 2;
                centers.Add(new Vector2(centerCellX, centerCellY));
            }

            return centers;
        }

        private void FillTransitions(ICell[,] cells, IReadOnlyList<Vector2> islandCenters)
        {
            for (int i = 0; i < islandCenters.Count - 1; i++)
            {
                var current = islandCenters[i];
                var next = islandCenters[i + 1];

                CreateHorizontalTransiction(cells, current, next);
                CreateVerticalTransiction(cells, current, next);
            }
        }

        private void CreateHorizontalTransiction(ICell[,] cells, Vector2 startPoint, Vector2 endPoint)
        {
            var yPosition = (int)endPoint.y;
            var xStartPosition = (int)Mathf.Min(startPoint.x, endPoint.x);
            var xEndPosition = (int)Mathf.Max(startPoint.x, endPoint.x);

            Fill(cells, xStartPosition, yPosition, xEndPosition - xStartPosition + 1, 1, ECellType.Empty);
        }

        private void CreateVerticalTransiction(ICell[,] cells, Vector2 startPoint, Vector2 endPoint)
        {
            var xPosition = (int)startPoint.x;
            var yStartPosition = (int)Mathf.Min(startPoint.y, endPoint.y);
            var yEndPosition = (int)Mathf.Max(startPoint.y, endPoint.y);

            Fill(cells, xPosition, yStartPosition, 1, yEndPosition - yStartPosition + 1, ECellType.Empty);
        }

        private void CreateHoles(ICell[,] cells, IReadOnlyList<Vector2> islandCenters)
        {
            foreach (var center in islandCenters)
            {
                cells[(int)center.x, (int)center.y].SetCellType(ECellType.Wall);
            }
        }

        private void Fill(ICell[,] cells, int x, int y, int width, int height, ECellType cellType)
        {
            for (var row = y; row < y + height; row++)
            {
                for (var column = x; column < x + width; column++)
                {
                    if (cells[column, row] == null)
                        cells[column, row] = new Cell(row, column, cellType);
                    else
                        cells[column, row].SetCellType(cellType);
                }
            }
        }
    }
}
