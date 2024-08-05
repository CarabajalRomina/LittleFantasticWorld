using Assets.scrips.modelo.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.interfaces.IEstrategias
{
    public interface IEstrategiaItem
    {
        public void AplicarEfecto(Entidad entidad);
    }
}
