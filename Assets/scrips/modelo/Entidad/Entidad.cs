using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.Entidad
{
    public abstract class Entidad
    {
        private int Id { get; }
        static int GlobalCount;
        private string Nombre { get; set; }
        private IReino Reino { get; set; }
        private IHabitat Habitats { get; set; }
        private int VidaActual { get; set; }
        private int VidaMax { get; set; }
        private int PuntosAtaque { get; set; }
        private int PuntosDefensa { get; set; }

        public Entidad(string nombre, IReino reino, IHabitat habitats, int vidaMax, int puntosAtaque, int puntosDefensa)
        {
            NOMBRE = nombre;
            REINO = reino;
            HABITATS = habitats;
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
        public string NOMBRE
        {
            get { return Nombre; }
            set
            {
                if (value != null)
                {
                    Nombre = value;
                }
            }
        }

        public IReino REINO
        {
            get { return Reino; }
            set
            {
                if (value != null)
                {
                    Reino = value;
                }
            }

        }


        public IHabitat HABITATS
        {
            get { return Habitats; }
            set
            {
                if (value != null)
                {
                    Habitats = value;
                }
            }
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

    }
}
