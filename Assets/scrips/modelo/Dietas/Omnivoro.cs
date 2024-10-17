using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.dietas
{
    public class Omnivoro : IDieta
    {
        public bool PuedoComer(Comida alimento)
        {
            if (alimento != null && alimento.TIPODIETA is Carnivoro || alimento.TIPODIETA is Herbivoro) { return true; } else { return false; }
        }
        public override string ToString()
        {
            return "Omnivoro";
        }
    }
}
