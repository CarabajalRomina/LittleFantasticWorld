using Assets.scrips.interfaces;
using Assets.scrips.interfaces.pelea;
using UnityEngine;
namespace Assets.scrips.modelo.pelea.acciones
{
    public class Atacar : IAccionCombate
    {
        public void EjecutarAccion(ICombate atacante,ICombate objetivo)
        {
            int danio;
            if (!objetivo.SeEstaDefendiendo())
            {
                danio = atacante.Atacar();            
            }
            else
            {
                danio = atacante.Atacar() - objetivo.Defender();
            }
            objetivo.RecibirDanio(danio);
            Debug.Log($"{atacante.ObtenerNombre()} ha atacado a {objetivo.ObtenerNombre()} causando {danio} de daño.");
        }
    }
}
