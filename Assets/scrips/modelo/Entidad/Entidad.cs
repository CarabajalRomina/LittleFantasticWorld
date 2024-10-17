using Assets.scrips.interfaces;
using Assets.scrips.interfaces.posicionable;
using UnityEngine;

namespace Assets.scrips.modelo.entidad
{
    public abstract class Entidad: IDescribible, IPosicionable
    {
        protected string Nombre;
        protected IReino Reino;
        protected IHabitat Habitats;
        // protected Vector3 PosicionActual;
        protected Terreno TerrenoActual;
        protected GameObject PersonajePrefab;
        protected GameObject InstanciaPersonaje;


        protected Entidad(string nombre, IReino reino, IHabitat habitats)
        {
            NOMBRE = nombre;
            REINO = reino;
            HABITATS = habitats;
        }

        #region PROPIEDADES
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

        public Terreno TERRENOACTUAL
        {
            get { return TerrenoActual; }
            set
            {
                if (value != null)
                {
                    TerrenoActual = value;
                }
                else
                {
                    Debug.Log("el valor es null");
                }
            }
        }

        public GameObject PERSONAJEPREFAB
        {
            get { return PersonajePrefab; }
            set
            {
                if (value != null)
                {
                    PersonajePrefab = value;
                }
                else
                {
                    Debug.Log("el valor es null");
                    PersonajePrefab = null;
                }
            }
        }

        public GameObject INSTANCIAPERSONAJE
        {
            get { return PersonajePrefab; }
            set
            {
                if (value != null)
                {
                    InstanciaPersonaje = value;
                }
                else
                {
                    Debug.Log("el valor es null");
                    InstanciaPersonaje = null;
                }
            }
        }

        #endregion

        public override string ToString()
        {
            return $"Nombre: {NOMBRE}" +
                $"Reino: {REINO}" +
                $"Habitats: {HABITATS}";
        }
        public abstract string[] ObtenerValoresInstancias();
        public void EstablecerPosicion(Terreno terrenoDestino)
        {
            TERRENOACTUAL = terrenoDestino;
            TERRENOACTUAL.AgregarEntidad(this);
        }
    }
}
