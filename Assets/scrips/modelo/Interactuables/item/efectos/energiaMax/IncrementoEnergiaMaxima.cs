using Assets.scrips.interfaces.efecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.interactuables.item.estrategias
{
    public class IncrementoEnergiaMaxima : IEfectoItem
    {
        public bool AplicarEfecto(Personaje personaje)
        {
            return true;
        }
        public override string ToString()
        {
            return "Incremento de energia max";
        }
    }
}
