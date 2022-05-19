using World.MapModel.Interfaces;

namespace Character.Interfaces
{
    public interface ICharacterSpawner
    {
        void SpawnToRandomPoint(IMap map, ICharacter character);
    }
}
