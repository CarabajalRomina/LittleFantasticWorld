using Assets.scrips.interfaces.interactuable;
using Assets.scrips.modelo.configuraciones;
using Assets.scrips.Controllers;
using Assets.scrips.modelo.entidad;
using UnityEngine;
using Assets.scrips.Controllers.entidad;
using System.Linq;
using System;
using System.Collections;
using Assets.scrips.Controllers.comida;
using Assets.scrips.Controllers.item;


namespace Assets.scrips.Controllers.mapa
{
    public class MapaController: SingletonMonoBehaviour<MapaController>
    {
        EntidadController CntEntidad;
        ComidaController CntComida = ComidaController.GetInstancia;
        ItemController CntItem = ItemController.GetInstancia;
        Personaje personajeSeleccionado;
        public void Start()
        {
            CntEntidad = EntidadController.Instancia;
            personajeSeleccionado = CntEntidad.GetPersonajes().First();
        }

        public void CargarConObjTerrenosLimitrofes(Terreno terrenoActual)
        {
            foreach (Terreno terreno in terrenoActual.TERRENOSLIMITROFES)
            {
                if(terreno.TIPOSUBTERRENO.TIPOTERRENO is not Especial)
                {
                    // Verifica si el terreno no está cargado
                    if (terreno.ENTIDADES.Count == 0)
                    {
                        AsignarEnemigosProcedural(terreno); // Cargar enemigos solo si el terreno no está cargado

                        if (terreno.INTERACTUABLES.Count == 0)
                        {
                            AsignarComidaProcedural(terreno); // Cargar interactuable solo si el terreno no está cargado
                            AsignarItemProcedural(terreno);
                        }
                        else { Debug.Log("Ya esta cargado con interactuable"); }
                    }
                    else { Debug.Log("Ya esta cargado con entidades"); }
                }       
            }
        }

        private void AsignarEnemigosProcedural(Terreno terreno)
        {
            int cantidadEnemigos = Utilidades.GenerarNumeroAleatorio(0, ConfiguracionGeneral.CantMaxEnemigosXTerreno + 1);
            
            if(cantidadEnemigos > 0)
            {
                for (int i = 0; i < cantidadEnemigos; i++)
                {
                    Enemigo enemigo = CntEntidad.ObtenerEnemigoAleatorio();
                    if(enemigo != null)
                    {
                        terreno.AgregarEntidad(enemigo);
                    }
                    else { Debug.Log("no existe un enemigo aleatorio"); }          
                }
            }  
        }

        private void AsignarComidaProcedural(Terreno terreno)
        {
            int cantidadComida = Utilidades.GenerarNumeroAleatorio(0, ConfiguracionGeneral.CantidadMaxComidaXTerreno + 1);

            if(cantidadComida > 0)
            {
                for (int i = 0; i < cantidadComida; i++)
                {
                    IInteractuable interactuable = CntComida.ObtenerComidaAleatoria();
                    if(interactuable != null)
                    {
                        terreno.AgregarInteractuable(interactuable);
                    }
                }
            }
           
        }

        private void AsignarItemProcedural(Terreno terreno)
        {
            int cantidadItem = Utilidades.GenerarNumeroAleatorio(0, ConfiguracionGeneral.CantidadMaxItemsXTerreno + 1);

            if (cantidadItem > 0)
            {
                for (int i = 0; i < cantidadItem; i++)
                {
                    IInteractuable interactuable = CntItem.ObtenerItemAleatorio();
                    if (interactuable != null)
                    {
                        terreno.AgregarInteractuable(interactuable);
                    }
                }
            }
        }


        public void InstanciarObjetos3D(Terreno terreno)
        {
            if(personajeSeleccionado != null)
            {
                GameObject per = personajeSeleccionado.PERSONAJEPREFAB.gameObject;
                GameObject nuevaInstancia;
                try
                {
                    DespachadorHiloPrincipal.Instancia.Enqueue(() => {
                        nuevaInstancia = Instantiate(per, terreno.POSICIONTRIDIMENSIONAL, Quaternion.identity);
                        Debug.Log("Instancia creada: " + nuevaInstancia.name);
                        if (nuevaInstancia != null)
                        {
                            terreno.CambiarEstado(new Ocupado());
                            personajeSeleccionado.TERRENOACTUAL = terreno;
                            personajeSeleccionado.INSTANCIAPERSONAJE = nuevaInstancia;
                            terreno.AgregarEntidad(personajeSeleccionado);


                            Debug.Log("Se instancio el personaje correctamente");
                        }
                    });             
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }       
            }
        }
    }
}
