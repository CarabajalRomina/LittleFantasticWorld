using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.reinos
{
    public class Vegetal : IReino
    {
        public Type ObtenerTipo()
        {
            return typeof(Vegetal);
        }

        public override string ToString()
        {
            return "Vegetal";
        }
    }
}
