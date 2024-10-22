using Assets.scrips.interfaces.fabricas.entidad;
using Assets.scrips.modelo.entidad;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scrips.fabricas.entidades.enemigos
{
    internal class FabricaEnemigo : IFabricaEntidad
    {
        string Nombre;
        IReino Reino;
        IHabitat Habitats;
        Transform PersonajePrefab;
        int VidaMax;
        int PuntosAtaque;
        int PuntosDefensa;


        public FabricaEnemigo(string nombre, IReino reino, IHabitat habitats, int vidaMax, int puntosAtaque, int puntosDefensa)
        {
            Nombre = nombre;
            Reino = reino;
            Habitats = habitats;
            //PersonajePrefab = personajePrefab;
            VidaMax = vidaMax;
            PuntosAtaque = puntosAtaque;
            PuntosDefensa = puntosDefensa;
        }

        public bool CrearEntidad(out Entidad entidad )
        {
            entidad = null;
            try
            {
                entidad = new Enemigo(Nombre, Reino, Habitats, VidaMax, PuntosAtaque, PuntosDefensa);
                return true;
            }catch (Exception ex)
            {
                Debug.LogError("Error al crear el personaje: " + ex.Message);
                return false;
            }
        }


        public Entidad CrearEntidad()
        {
            try
            {
                return new Enemigo(Nombre, Reino, Habitats, VidaMax, PuntosAtaque, PuntosDefensa);
               
            }
            catch (Exception ex)
            {
                Debug.LogError("Error al crear el personaje: " + ex.Message);
                return null;
            }
        }
    }
}

