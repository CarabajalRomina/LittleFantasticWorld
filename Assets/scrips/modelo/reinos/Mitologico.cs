using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.reinos
{
    public class Mitologico : IReino
    {
        public Type ObtenerTipo()
        {
            return typeof(Mitologico);
        }

        public override string ToString()
        {
            return "Mitologico";
        }
    }
}
