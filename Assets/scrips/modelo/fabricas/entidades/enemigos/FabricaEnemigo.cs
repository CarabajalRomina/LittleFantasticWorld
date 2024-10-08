using Assets.scrips.interfaces.fabricas.entidad;
using Assets.scrips.modelo.Entidad;
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
        HashSet<string> NombresUtilizados = new HashSet<string>();


        public FabricaEnemigo(string nombre, IReino reino, IHabitat habitats, int vidaMax, int energiaMax, int puntosAtaque, int puntosDefensa)
        {
            Nombre = nombre;
            Reino = reino;
            Habitats = habitats;
            //PersonajePrefab = personajePrefab;
            VidaMax = vidaMax;
            PuntosAtaque = puntosAtaque;
            PuntosDefensa = puntosDefensa;
        }

        public bool CrearEntidad(out modelo.Entidad.Entidad entidad )
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
    }
}

