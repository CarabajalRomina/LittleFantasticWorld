using Assets.scrips.interfaces.efecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.interactuables.item.estrategias.defensaDeAtaque
{
    public class ReduccionDefensaDeAtaque : IEfectoItem
    {
        public bool AplicarEfecto(Personaje personaje)
        {
            if (personaje.ReducirPuntosDefensa(20))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public override string ToString()
        {
            return "Reduccion defensa";
        }
    }
}
