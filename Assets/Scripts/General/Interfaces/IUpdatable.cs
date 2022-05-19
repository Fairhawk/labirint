using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Interfaces
{
    public interface IUpdatable
    {
        void OnUpdate(float deltaTime);
    }
}
