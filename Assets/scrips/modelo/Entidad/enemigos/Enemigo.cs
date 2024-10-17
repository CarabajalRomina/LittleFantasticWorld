using Assets.scrips.interfaces;

namespace Assets.scrips.modelo.entidad
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
            var dado = Dado.TirarDado();
            return PUNTOSATAQUE + dado;
        }

        public int Defender()
        {
            var dado = Dado.TirarDado();
            return PUNTOSDEFENSA + dado;
        }

        #endregion

        public override string[] ObtenerValoresInstancias()
        {
            return new string[] {
            ID.ToString(),
            NOMBRE,
            REINO.ToString(),
            HABITATS.ToString(),
            VIDAMAX.ToString(),
            PUNTOSATAQUE.ToString(),
            PUNTOSDEFENSA.ToString(),
            };
        }

    }
}
