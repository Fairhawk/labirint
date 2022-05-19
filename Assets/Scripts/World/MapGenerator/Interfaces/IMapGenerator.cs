using World.MapModel.Interfaces;

namespace World.MapGenerator.Interfaces
{
    public interface IMapGenerator
    {
        IMap GenerateMap(int widht, int height);
    }
}
