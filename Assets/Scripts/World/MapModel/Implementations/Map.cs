using World.MapModel.Interfaces;

namespace World.MapModel.Implementations
{
    public class Map : IMap
    {
        public int Widht { get; }
        public int Height { get; }
        private readonly ICell[,] _cells;

        public Map(int widht, int height, ICell[,] cells)
        {
            Widht = widht;
            Height = height;

            _cells = cells;
        }

        public ICell GetCell(int x, int y)
        {
            return _cells[x, y];
        }

        public ICell[,] GetCells()
        {
            return _cells;
        }
    }
}
