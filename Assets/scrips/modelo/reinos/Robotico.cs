using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.reinos
{
    public class Robotico : IReino
    {
        public Type ObtenerTipo()
        {
            return typeof(Robotico);
        }

        public override string ToString()
        {
            return "Robotico";
        }
    }
}
