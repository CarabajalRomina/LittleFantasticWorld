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
        Transform PersonajePrefab;
        int VidaMax;
        IDieta Dieta;
        int EnergiaMax;
        int PuntosAtaque;
        int PuntosDefensa;
        int RangoAtaque;

        public FabricaPersonaje(string nombre, IReino reino, IHabitat habitats, Transform personajePrefab, int vidaMax,IDieta dieta, int energiaMax, int puntosAtaque, int puntosDefensa, int rangoAtaque)
        {
            Nombre = nombre;
            Reino = reino;
            Habitats = habitats;
            PersonajePrefab = personajePrefab;
            VidaMax = vidaMax;
            Dieta = dieta;
            EnergiaMax = energiaMax;
            PuntosAtaque = puntosAtaque;
            PuntosDefensa = puntosDefensa;
            RangoAtaque = rangoAtaque;
        }


        public Entidad CrearEntidad()
        {
            return new Personaje(Nombre, Reino, Habitats, VidaMax, Dieta, PuntosAtaque, PuntosDefensa, EnergiaMax,RangoAtaque, PersonajePrefab);
        }
    }
}
