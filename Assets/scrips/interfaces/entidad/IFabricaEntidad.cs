using Assets.scrips.modelo.entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.interfaces.fabricas.entidad
{
    public interface IFabricaEntidad
    {
        bool CrearEntidad(out Entidad personaje);
    }
}
