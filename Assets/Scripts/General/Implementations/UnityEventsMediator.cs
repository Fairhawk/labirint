using General.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace General.Implementations
{
    public class UnityEventsMediator : IUnityEventsMediator
    {
        private readonly IList<IUpdatable> _updatables;

        public UnityEventsMediator()
        {
            _updatables = new List<IUpdatable>();
        }

        public void Register(IUpdatable updatable)
        {
            if (!_updatables.Contains(updatable))
                _updatables.Add(updatable);
        }

        public void UnRegister(IUpdatable updatable)
        {
            if (_updatables.Contains(updatable))
                _updatables.Remove(updatable);
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].OnUpdate(deltaTime);
            }
        }

        public void Dispose()
        {
            _updatables.Clear();
        }
    }
}
