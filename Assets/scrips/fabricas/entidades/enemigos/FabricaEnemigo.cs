using Assets.scrips.interfaces.fabricas.entidad;
using Assets.scrips.modelo.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.fabricas.entidades.enemigos
{
    internal class FabricaEnemigo : IFabricaEntidad
    {
        string Nombre { get; set; }
        IReino Reino { get; set; }
        IHabitat Habitats { get; set; }
        Transform PersonajePrefab { get; set; }
        int VidaMax { get; set; }
        int PuntosAtaque { get; set; }
        int PuntosDefensa { get; set; }

        public FabricaEnemigo(string nombre, IReino reino, IHabitat habitats, Transform personajePrefab, int vidaMax, int puntosAtaque, int puntosDefensa)
        {
            Nombre = nombre;
            Reino = reino;
            Habitats = habitats;
            PersonajePrefab = personajePrefab;
            VidaMax = vidaMax;
            PuntosAtaque = puntosAtaque;
            PuntosDefensa = puntosDefensa;
        }

        public Entidad CrearEntidad()
        {
            return new Enemigo(Nombre, Reino, Habitats, VidaMax, PuntosAtaque, PuntosDefensa, PersonajePrefab);
        }
    }
}
