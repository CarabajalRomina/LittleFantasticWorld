using Assets.scrips.fabricas.dietas;
using Assets.scrips.fabricas.entidades.personajes;
using Assets.scrips.modelo.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.scrips.Controllers.entidad
{
    public class EntidadController : SingletonMonoBehaviour<EntidadController>
    {
        List<Entidad> Entidades = new List<Entidad>();
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

        public List<Entidad> ENTIDADES
        {
            get { return Entidades; }
            set { Entidades = value; }
        }

        public HashSet<string> NOMBRESSELECCIONADOS
        {
            get { return NombresSeleccionados; }
            set { NombresSeleccionados = value; }
        }
        #endregion

        #region CRUD PERSONAJE
        public bool CrearPersonaje(string nombre, IReino reino, IDieta dieta, IHabitat habitat, int energiaMax, int vidaMax, int puntosAtaque, int puntosDefensa, int rangoAtaque)
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
                Entidades.Add(personaje);
                NombresSeleccionados.Add(nombre);
                return true;
            }
            return false;       
        }

        public bool Eliminar(Personaje personaje)
        {
            try
            {
                if (ENTIDADES.Contains(personaje))
                {
                    ENTIDADES.Remove(personaje);
                    NOMBRESSELECCIONADOS.Remove(personaje.NOMBRE);
                    return true;
                }
                else{
                    Debug.Log("no se encuentra la persona");
                    return false;
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

        public Personaje BuscarPorId(int id)
        {
           Personaje personaje = ENTIDADES
            .OfType<Personaje>()
            .FirstOrDefault(p => p.ID == id);
            if(personaje != null) { return personaje; }
            else { return null; }
        }

        public List<Personaje> GetPersonajes()
        {
            return Entidades.OfType<Personaje>().ToList();
        }
        #endregion

        public GameObject InstanciarPersonaje(Personaje personaje, Vector3 posicion)
        {
          return Instantiate(personaje.PERSONAJEPREFAB, posicion, Quaternion.identity);      
        }
    }
}
