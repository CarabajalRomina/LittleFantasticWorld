using Assets.scrips.Controllers.habitat;
using Assets.scrips.fabricas.dietas;
using Assets.scrips.fabricas.entidades.personajes;
using Assets.scrips.fabricas.reinos;
using Assets.scrips.modelo.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.Controllers.entidad
{
    public class PersonajeController : Singleton<PersonajeController>
    {
        HabitatController cntHabitat = HabitatController.GetInstancia;
        List<Entidad> Personajes = new List<Entidad>();
        List<IReino> Reinos = new List<IReino> { 
            new FabricaAnimal().CrearReino(),
            new FabricaVegetal().CrearReino(),
            new FabricaDemoniaco().CrearReino(),
            new FabricaHumano().CrearReino(),
            new FabricaRobotico().CrearReino(),
            new FabricaMitologico().CrearReino()
        };
        List<IDieta> Dietas = new List<IDieta> {
            new FabricaCarnivoro().CrearDieta(),
            new FabricaHerbivoro().CrearDieta(),
            new FabricaOmnivoro().CrearDieta(),
            new FabricaEnergiaElectrica().CrearDieta(),
            new FabricaFotosintetico().CrearDieta()
        };

        #region PROPIEDADES
        public List<IDieta> DIETAS
        {
            get { return Dietas; }
            set { Dietas = value; }
        }

        public List<IReino> REINOS
        {
            get { return Reinos; }
            set { Reinos = value; }
        }

        public List<Entidad> PERSONAJES
        {
            get { return Personajes; }
            set { Personajes = value; }
        }

        #endregion

        #region CRUD
        public void CrearEntidad(string nombre, IReino reino, IDieta dieta, IHabitat habitat, int energiaMax, int vidaMax, int puntosAtaque, int puntosDefensa, int rangoAtaque)
        {
            PERSONAJES.Add(
                new FabricaPersonaje(
                    nombre,
                    reino,
                    dieta,
                    habitat,
                    vidaMax,
                    energiaMax,
                    puntosAtaque,
                    puntosDefensa,
                    rangoAtaque
                    ).CrearEntidad()
                );
            Debug.Log(Personajes[0]);
        }
        #endregion


    }
}
