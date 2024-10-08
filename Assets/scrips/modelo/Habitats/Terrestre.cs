using Assets.scrips.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.modelo.Habitats
{
    public class Terrestre: IHabitat
    {
        public bool PuedoMoverme(MonoBehaviour tipoTerreno)
        {
            return tipoTerreno is Tierra;
        }

        public override string ToString()
        {
            return "Terrestre";
        }
    }
}
