using Assets.scrips.interfaces;
using Assets.scrips.interfaces.pelea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.modelo.pelea.acciones
{
    public class Defender : IAccionCombate
    {
        public void EjecutarAccion(ICombate atacante, ICombate objetivo)
        {
            atacante.ActivarDefensa();
            Debug.Log($"{atacante.ObtenerNombre()} se defendió, reduciendo el daño recibido de {objetivo.ObtenerNombre()}.");
        }
    }
}
