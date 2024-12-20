using Assets.scrips.interfaces;
using Assets.scrips.interfaces.pelea;
using UnityEngine;
namespace Assets.scrips.modelo.pelea.acciones
{
    public class Atacar : IAccionCombate
    {
        public void EjecutarAccion(ICombate atacante, ICombate objetivo)
        {
            var danio = atacante.Atacar();
            objetivo.RecibirDanio(danio);
            Debug.Log($"{atacante.ObtenerNombre()} ha atacado a {objetivo.ObtenerNombre()} causando {danio} de daño.");

        }
    }
}
