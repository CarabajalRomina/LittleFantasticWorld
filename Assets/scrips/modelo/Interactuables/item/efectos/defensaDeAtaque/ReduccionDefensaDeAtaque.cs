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
        public void AplicarEfecto(Personaje personaje)
        {
            
        }
        public override string ToString()
        {
            return "Reduccion defensa de ataque";
        }
    }
}
