using Assets.scrips.interfaces.fabricas.entidad;
using Assets.scrips.modelo.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.fabricas.entidades.personajes
{
    internal class FabricaPersonaje : IFabricaEntidad
    {
        string Nombre;
        IReino Reino;
        IHabitat Habitats;
        GameObject PersonajePrefab;
        int VidaMax;
        IDieta Dieta;
        int EnergiaMax;
        int PuntosAtaque;
        int PuntosDefensa;
        int RangoAtaque;


        public FabricaPersonaje(string nombre, IReino reino, IDieta dieta, IHabitat habitats, int vidaMax, int energiaMax, int puntosAtaque, int puntosDefensa, int rangoAtaque)
        {
            Nombre = nombre;
            Reino = reino;
            Habitats = habitats;
            VidaMax = vidaMax;
            Dieta = dieta;
            EnergiaMax = energiaMax;
            PuntosAtaque = puntosAtaque;
            PuntosDefensa = puntosDefensa;
            RangoAtaque = rangoAtaque;
        }


        public bool CrearEntidad(out Entidad personaje)
        {
            personaje = null;
            try
            {
                personaje = new Personaje(Nombre, Reino, Habitats, VidaMax, Dieta, PuntosAtaque, PuntosDefensa, EnergiaMax, RangoAtaque);
                return true;

            }catch (Exception ex)
            {
                Debug.LogError("Error al crear el personaje: " + ex.Message);
                return false;
            }
        }
    }
}

