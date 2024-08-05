using Assets.scrips.fabricas.reinos.fabricaReino;
using Assets.scrips.modelo.reinos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.fabricas.reinos
{
    public class FabricaHumano : FabricaReino
    {
        public override IReino CrearReino()
        {
            return new Humano();
        }
    }
}
