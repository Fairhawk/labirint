using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Character.Interfaces
{
    public interface ICharacterFactory
    {
        ICharacter CreateCharacter(GameObject viewPrefab);
    }
}
