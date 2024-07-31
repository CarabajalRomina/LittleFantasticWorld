using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.reinos
{
    public class Animal : IReino
    {
        public Type ObtenerTipo()
        {
            return typeof(Animal);
        }
        
        public override string  ToString()
        {
            return "Animal";
        }
    }
}
