using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.reinos
{
    public class Humano : IReino
    {
        public Type ObtenerTipo()
        {
            return typeof(Humano);
        }

        public override string ToString()
        {
            return "Humano";
        }
    }
}
