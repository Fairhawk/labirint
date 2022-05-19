using System.Linq;
using Character.Interfaces;
using UnityEngine;
using World.MapModel.Interfaces;

namespace Character.Implementations
{
    public class CharacterSpawner : ICharacterSpawner
    {
        public void SpawnToRandomPoint(IMap map, ICharacter character)
        {
            var randomCell = GetRandomCell(map);
            var position = new Vector3(randomCell.Position.Y, 1f, randomCell.Position.X);
            character.SetPosition(position);
            character.SetMovePath(null, null);
        }

        private ICell GetRandomCell(IMap map)
        {
            while(true)
            {
                var rows = map.GetCells().GetUpperBound(0) + 1;
                var columns = map.GetCells().Length / rows;

                var randomRow = Random.Range(0, rows);
                var randomColumn = Random.Range(0, columns);
                var randomCell = map.GetCell(randomRow, randomColumn);

                if (randomCell.CellType == World.MapModel.Enums.ECellType.Empty)
                    return randomCell;
            }
        }
    }
}
