using System;

namespace General.Interfaces
{
    public interface IUnityEventsMediator : IUpdatable, IDisposable
    {
        void Register(IUpdatable updatable);
        void UnRegister(IUpdatable updatable);
    }
}
