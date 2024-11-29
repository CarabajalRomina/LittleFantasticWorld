using Assets.scrips.modelo.entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.interfaces.interactuable
{
    public interface IInteractuable
    {
        public bool Interactuar(Personaje entidad);
        public string GetNombre();

    }
}
