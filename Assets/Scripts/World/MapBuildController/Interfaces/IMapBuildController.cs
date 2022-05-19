using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.MapModel.Interfaces;

namespace World.MapBuildController.Interfaces
{
    public interface IMapBuildController : IDisposable
    {
        event Action<IMap> OnMapBuilded;

        void BuildControlls();
        void BuildMap();
    }
}