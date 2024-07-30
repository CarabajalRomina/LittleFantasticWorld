using Assets.scrips.modelo.Dietas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.fabricas.dietas
{
    public class FabricaCarnivoro : FabricaDieta
    {
        public override IDieta CrearDieta()
        {
            return new Carnivoro();
        }
    }
}
