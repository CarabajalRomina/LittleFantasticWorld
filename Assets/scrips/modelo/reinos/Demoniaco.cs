using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.reinos
{
    public class Demoniaco : IReino
    {
        public Type ObtenerTipo()
        {
            return typeof(Demoniaco);
        }

        public override string ToString()
        {
            return "Demoniaco";
        }
    }
}
