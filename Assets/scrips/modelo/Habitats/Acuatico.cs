using Assets.scrips.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.Habitats
{
    public class Acuatico: IHabitat
    {
        public bool PuedoMoverme(ITipoTerreno tipoTerreno)
        {
            return tipoTerreno is Agua;
        }

        public override string ToString()
        {
            return "Acuatico";
        }
    }
}
