using Assets.scrips.Controllers.habitat;
using Assets.scrips.fabricas.dietas;
using Assets.scrips.fabricas.entidades.personajes;
using Assets.scrips.fabricas.reinos;
using Assets.scrips.modelo.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        HashSet<string> NombresSeleccionados = new HashSet<string>();

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

        public HashSet<string> NOMBRESSELECCIONADOS
        {
            get { return NombresSeleccionados; }
            set { NombresSeleccionados = value; }
        }
        #endregion

        #region CRUD
        public bool CrearEntidad(string nombre, IReino reino, IDieta dieta, IHabitat habitat, int energiaMax, int vidaMax, int puntosAtaque, int puntosDefensa, int rangoAtaque)
        {
            Entidad personaje;

            if(new FabricaPersonaje(
                nombre,
                reino,
                dieta,
                habitat,
                vidaMax,
                energiaMax,
                puntosAtaque,
                puntosDefensa,
                rangoAtaque).CrearEntidad(out personaje))
            {
                Personajes.Add(personaje);
                NombresSeleccionados.Add(nombre);
                return true;
            }
            return false;       
        }

        public bool Eliminar(Entidad personaje)
        {
            try
            {
                if (PERSONAJES.Contains(personaje))
                {
                    PERSONAJES.Remove(personaje);
                    NOMBRESSELECCIONADOS.Remove(personaje.NOMBRE);
                    return true;
                }
                else{
                    return false;
                    Debug.Log("no se encuentra la persona"); 
                }
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool EditarEntidad(Personaje personaje, string nombre, IReino reino, IDieta dieta, IHabitat habitat, int energiaMax, int vidaMax, int puntosAtaque, int puntosDefensa, int rangoAtaque)
        {
            try
            {
                personaje.NOMBRE = nombre;
                personaje.REINO = reino;
                personaje.DIETA = dieta;
                personaje.HABITATS = habitat;
                personaje.VIDAMAX = vidaMax;
                personaje.ENERGIAMAX = energiaMax;
                personaje.PUNTOSATAQUE = puntosAtaque;
                personaje.PUNTOSDEFENSA = puntosDefensa;
                personaje.RANGOATAQUE = rangoAtaque;
                return true;
            }catch(Exception e) { return false; }    
        }

        public Entidad BuscarPorId(int id)
        {
           Personaje personaje = PERSONAJES
            .OfType<Personaje>()
            .FirstOrDefault(p => p.ID == id);
            if(personaje != null) { return personaje; }
            else { return null; }
        }


        #endregion
    }
}
