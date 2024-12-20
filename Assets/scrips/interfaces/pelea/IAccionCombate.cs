using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.interfaces.pelea
{
    public interface IAccionCombate
    {
        public void EjecutarAccion(ICombate atacante, ICombate objetivo);
    }
}
