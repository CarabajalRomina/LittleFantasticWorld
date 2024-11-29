using Assets.scrips.modelo.entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.interfaces.efecto
{
    public interface IEfectoItem
    {
        public bool AplicarEfecto(Personaje personaje);
    }
}
