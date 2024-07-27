using Assets.scrips.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.Entidad
{
    public class Enemigo : Entidad, IEntidad
    {
        public Enemigo(string nombre, IReino reino, IHabitat habitats, int vidaMax, int puntosAtaque, int puntosDefensa) :base( nombre, reino, habitats, vidaMax, puntosAtaque, puntosDefensa) { }
        #region METODOS ATAQUE Y DEFENSA
        public int Atacar()
        {
            var dado = Dado.TirarDado(1, 6);
            return PUNTOSATAQUE + dado;
        }

        public int DefenderDeAtaque()
        {
            var dado = Dado.TirarDado(1, 6);
            return PUNTOSDEFENSA + dado;
        }

        #endregion

    }
}
