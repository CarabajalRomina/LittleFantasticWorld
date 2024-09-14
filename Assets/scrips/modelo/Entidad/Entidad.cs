using Assets.scrips.interfaces;
using Assets.scrips.interfaces.Posicionable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.modelo.Entidad
{
    public abstract class Entidad: IDescribible, IPosicionable
    {
        protected string Nombre;
        protected IReino Reino;
        protected IHabitat Habitats; 

        protected Vector3 CoordenadaAxial = new Vector3(0, 0.5f, 0);

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

        public Vector3 COORDAXIAL
        {
            get { return CoordenadaAxial; }
            set
            {
                if (value != null)
                {
                    CoordenadaAxial = value;
                }
                else
                {
                    Debug.Log("el valor es null");
                    CoordenadaAxial = new Vector3(0, 0, 0);
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
        public void EstablecerPosicion(Vector2 coordenadaInicial)
        {
            CoordenadaAxial = coordenadaInicial;
        }
    }
}
