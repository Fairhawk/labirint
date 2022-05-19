using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using World.MapModel.Data;
using World.MapModel.Interfaces;
using World.MapPathFinder.Interfaces;

namespace World.MapPathFinder.Implementations
{
    internal class PathNode
    {
        public Point Position { get; set; }
        public int PathLengthFromStart { get; set; }
        public PathNode CameFrom { get; set; }
        public int HeuristicEstimatePathLength { get; set; }
        public int EstimateFullPathLength => PathLengthFromStart + HeuristicEstimatePathLength;
    }

    public class AStarPathFinder : IMapPathFinder
    {
        public IReadOnlyList<Vector3> FindPath(IMap map, Point start, Point end)
        {
            Debug.DrawLine(new Vector3(start.X, 1f, start.Y), new Vector3(end.X, 1f, end.Y), Color.green, 1f);

            var closedSet = new List<PathNode>();
            var openSet = new List<PathNode>();
            var startNode = new PathNode()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, end)
            };

            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                var currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).FirstOrDefault();

                if (currentNode.Position == end)
                {
                    var path = GetPathForNode(currentNode).Select(p => new Vector3(p.X, 1f, p.Y)).ToList();
                    path.RemoveAt(0);
                    return path;
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (var neighbourNode in GetNeighbours(currentNode, end, map))
                {
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;

                    var openNode = openSet.FirstOrDefault(node => node.Position == neighbourNode.Position);
                    if (openNode == null)
                    {
                        openSet.Add(neighbourNode);
                        Debug.DrawLine(new Vector3(currentNode.Position.X, 1f, currentNode.Position.Y), new Vector3(neighbourNode.Position.X, 1f, neighbourNode.Position.Y), Color.red, 1f);
                    }
                    else if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
            return null;
        }

        private IReadOnlyList<PathNode> GetNeighbours(PathNode pathNode, Point end, IMap map)
        {
            var cells = map.GetCells();
            var result = new List<PathNode>();

            var neighbourPoints = new Point[]
            {
                new Point(pathNode.Position.X + 1, pathNode.Position.Y),
                new Point(pathNode.Position.X - 1, pathNode.Position.Y),
                new Point(pathNode.Position.X, pathNode.Position.Y + 1),
                new Point(pathNode.Position.X, pathNode.Position.Y - 1)
            };

            var rows = cells.GetUpperBound(0) + 1;
            var columns = cells.Length / rows;

            foreach (var point in neighbourPoints)
            {
                if (point.X < 0 || point.X >= rows)
                    continue;
                if (point.Y < 0 || point.Y >= columns)
                    continue;
                if (cells[point.X, point.Y].CellType == MapModel.Enums.ECellType.Wall)
                    continue;

                var neighbourNode = new PathNode()
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart + GetDistanceBetweenNeighbours(),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, end)
                };
                result.Add(neighbourNode);
            }
            return result;
        }

        private IReadOnlyList<Point> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }

        private int GetDistanceBetweenNeighbours()
        {
            return 1;
        }

        private int GetHeuristicPathLength(Point from, Point to)
        {
            return Mathf.Abs(from.X - to.X) + Mathf.Abs(from.Y - to.Y);
        }
    }
}
