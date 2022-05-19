using General.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Interfaces
{
    public interface ICharacter : IUpdatable
    {
        Vector3 Position { get; }

        void SetPosition(Vector3 position);
        void SetMovePath(IReadOnlyList<Vector3> path, Action onCompleted);
    }
}
