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
        public void AplicarEfecto(Personaje personaje)
        {
            personaje.AumentarVidaActual(20);
        }

        public override string ToString()
        {
            return "Incremento vida actual";
        }
    }
}
