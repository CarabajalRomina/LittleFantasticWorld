using Assets.scrips.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.modelo.habitats
{
    public class Aereo: IHabitat
    {
        public bool PuedoMoverme(MonoBehaviour tipoTerreno)
        {
            return tipoTerreno is Aire;
        }

        public override string ToString()
        {
            return "Aereo";
        }
    }
}
