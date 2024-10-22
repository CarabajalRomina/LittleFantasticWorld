using Assets.scrips.interfaces.efecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.interactuables.item.estrategias
{
    public class ReduccionVidaActual : IEfectoItem
    {
        public void AplicarEfecto(Personaje personaje)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "Reduccion Vida Actual";
        }
    }
}
