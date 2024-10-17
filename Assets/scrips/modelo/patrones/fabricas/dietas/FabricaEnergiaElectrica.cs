using Assets.scrips.modelo.dietas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.fabricas.dietas
{
    public class FabricaEnergiaElectrica : FabricaDieta
    {
        public override IDieta CrearDieta()
        {
            return new EnergiaElectrica();
        }
    }
}
