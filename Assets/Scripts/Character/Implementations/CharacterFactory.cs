using Character.Interfaces;
using UnityEngine;

namespace Character.Implementations
{
    public class CharacterFactory : ICharacterFactory
    {
        public ICharacter CreateCharacter(GameObject viewPrefab)
        {
            var view = GameObject.Instantiate(viewPrefab);
            return view.AddComponent<Character>();
        }
    }
}
