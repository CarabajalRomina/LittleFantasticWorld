using Assets.scrips.fabricas.habitats.fabricaHabitat;
using Assets.scrips.modelo.habitats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.fabricas.habitats
{
    public class FabricaTerrestre : FabricaHabitat
    {
        public override IHabitat CrearHabitat()
        {
            return new Terrestre();
        }
    }
}
