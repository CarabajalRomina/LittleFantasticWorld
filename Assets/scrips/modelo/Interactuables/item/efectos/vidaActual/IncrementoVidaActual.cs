using Assets.scrips.interfaces.efecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.interactuables.item.estrategias
{
    public class IncrementoVidaActual : IEfectoItem
    {
        public bool AplicarEfecto(Personaje personaje)
        {
            if (personaje.AumentarVidaActual(20))
            {
                return true;
            }else return false;
        }

        public override string ToString()
        {
            return "Incremento vida actual";
        }
    }
}
