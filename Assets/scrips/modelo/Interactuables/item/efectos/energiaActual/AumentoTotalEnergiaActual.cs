using Assets.scrips.interfaces.efecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.interactuables.item.estrategias.energiaActual
{
    public class AumentoTotalEnergiaActual : IEfectoItem
    {
        public void AplicarEfecto(Personaje personaje)
        {
            personaje.AumentarEnergiaActual(personaje.ENERGIAMAX);
        }
        public override string ToString()
        {
            return "Aumento total energia actual";
        }
    }
}
