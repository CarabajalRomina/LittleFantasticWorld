using Assets.scrips.modelo.Dietas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.fabricas.dietas
{
    public class FabricaOmnivoro : FabricaDieta
    {
        public override IDieta CrearDieta()
        {
            return new Omnivoro();
        }
    }
}
