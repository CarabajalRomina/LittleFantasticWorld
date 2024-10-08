using Assets.scrips.fabricas.reinos;
using System.Collections.Generic;

namespace Assets.scrips.Controllers.reinos
{
    public class ControllerReino: Singleton<ControllerReino>
    {
        List<IReino> Reinos = new List<IReino> {
            new FabricaAnimal().CrearReino(),
            new FabricaVegetal().CrearReino(),
            new FabricaDemoniaco().CrearReino(),
            new FabricaHumano().CrearReino(),
            new FabricaRobotico().CrearReino(),
            new FabricaMitologico().CrearReino()
        };

        public List<IReino> REINOS
        {
            get { return Reinos; }
            set { Reinos = value; }
        }

    }
}
