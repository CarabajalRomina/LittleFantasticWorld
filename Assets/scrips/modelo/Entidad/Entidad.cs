using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.modelo.Entidad
{
    public abstract class Entidad
    {
        protected string Nombre { get; set; }
        protected IReino Reino { get; set; }
        protected IHabitat Habitats { get; set; }


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


        #endregion


        public override string ToString()
        {
            return $"Nombre: {NOMBRE}" +
                $"Reino: {REINO}" +
                $"Habitats: {HABITATS}";
        }
    }
}
