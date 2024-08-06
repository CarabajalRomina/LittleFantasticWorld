using Assets.scrips.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.modelo.Entidad
{
    public class Enemigo : Entidad, ICombate
    {
        private int Id;
        static int GlobalCount = 0;
        private int VidaActual { get; set; }
        private int VidaMax { get; set; }
        private int PuntosAtaque { get; set; }
        private int PuntosDefensa { get; set; }


        public Enemigo(string nombre, IReino reino, IHabitat habitats, int vidaMax, int puntosAtaque, int puntosDefensa) :base( nombre, reino, habitats) 
        {
            Id = ++GlobalCount;
            VIDAACTUAL = vidaMax;
            VIDAMAX = vidaMax;
            PUNTOSATAQUE = puntosAtaque;
            PUNTOSDEFENSA = puntosDefensa;
        }



        #region PROPIEDADES 
        public int ID
        {
            get { return Id; }
        }
        public int VIDAACTUAL
        {
            get { return VidaActual; }
            set
            {
                if (value >= 0 && value <= VidaMax)
                {
                    VidaActual = value;
                }
            }
        }

        public int VIDAMAX
        {
            get { return VidaMax; }
            set
            {
                if (value > 0)
                {
                    VidaMax = value;
                }
            }
        }

        public int PUNTOSATAQUE
        {
            get { return PuntosAtaque; }
            set
            {
                if (value > 0)
                {
                    PuntosAtaque = value;
                }
            }
        }

        public int PUNTOSDEFENSA
        {
            get { return PuntosDefensa; }
            set
            {
                if (value > 0)
                {
                    PuntosDefensa = value;
                }
            }
        }
        #endregion


        #region METODOS COMBATE

        public int Atacar()
        {
            var dado = Dado.TirarDado(1, 6);
            return PUNTOSATAQUE + dado;
        }

        public int Defender()
        {
            var dado = Dado.TirarDado(1, 6);
            return PUNTOSDEFENSA + dado;
        }

        #endregion

    }
}
