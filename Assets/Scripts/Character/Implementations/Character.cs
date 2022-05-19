using System;
using System.Collections.Generic;
using Character.Interfaces;
using UnityEngine;

namespace Character.Implementations
{
    public class Character : MonoBehaviour, ICharacter
    {
        private const float MoveSpeed = 5f;

        public Vector3 Position => transform.position;

        private List<Vector3> _path;
        private Action _onCompleted;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetMovePath(IReadOnlyList<Vector3> path, Action onCompleted)
        {
            _onCompleted = onCompleted;
            if (path != null && path.Count > 0)
                _path = new List<Vector3>(path);
            else
                _path = null;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_path != null && _path.Count > 0)
            {
                var targetPosition = _path[0];
                var position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed * deltaTime);
                position.y = 1f;

                transform.position = position;

                if (transform.position == targetPosition)
                {
                    _path.RemoveAt(0);
                    if (_path.Count == 0)
                        _onCompleted?.Invoke();
                }
            }
        }
    }
}
