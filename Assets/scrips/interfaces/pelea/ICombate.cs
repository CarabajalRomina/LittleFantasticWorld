using Assets.scrips.interfaces.pelea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.scrips.interfaces
{
    public interface ICombate
    {
        int Atacar();
        int Defender();
        void ActivarDefensa();
        string ObtenerNombre();
        int ObtenerVidaActual();
        int ObtenerVidaMaxima();
        bool ActualizarVidaActual(int value);
        bool EstaVivo();
        void RecibirDanio(int danio);
        void EjecutarAccion(IAccionCombate accion, ICombate objetivo);
        bool SeEstaDefendiendo();
        GameObject ObtenerPrefab();

    }
}
