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
        public bool AplicarEfecto(Personaje personaje)
        {
            if (personaje.AumentarEnergiaActual(personaje.ENERGIAACTUAL))
            {
                return true;
            }
            else return false;
        }
        public override string ToString()
        {
            return "Aumento total energia actual";
        }
    }
}
