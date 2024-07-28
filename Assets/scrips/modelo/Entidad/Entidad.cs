﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.modelo.Entidad
{
    public abstract class Entidad
    {
        private string Nombre { get; set; }
        private IReino Reino { get; set; }
        private IHabitat Habitats { get; set; }
        Transform PersonajePrefab { get; set; }


        public Entidad(string nombre, IReino reino, IHabitat habitats, Transform personajePrefab)
        {
            NOMBRE = nombre;
            REINO = reino;
            HABITATS = habitats;
            PERSONAJEPREFAB = personajePrefab;
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

        public Transform PERSONAJEPREFAB
        {
            get { return PersonajePrefab; }
            set
            {
                if (value != null)
                {
                    PersonajePrefab = value;
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
