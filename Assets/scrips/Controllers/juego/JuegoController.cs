using Assets.scrips.interfaces.interactuable;
using Assets.scrips.modelo.configuraciones;
using Assets.scrips.modelo.entidad;
using UnityEngine;
using Assets.scrips.Controllers.entidad;
using System;
using Assets.scrips.Controllers.comida;
using Assets.scrips.Controllers.item;
using Assets.scrips.Controllers.jugador;
using Assets.scrips.Controllers.pelea;
using Assets.scrips.interfaces;
using Assets.scrips.modelo.pelea;


namespace Assets.scrips.Controllers.juego
{
    public class JuegoController : SingletonMonoBehaviour<JuegoController>
    {
        public static event Action<GameObject> PeleaFinalizada;
        EntidadController CntEntidad;
        ComidaController CntComida = ComidaController.GetInstancia;
        ItemController CntItem = ItemController.GetInstancia;
        JugadorController CntJugador = JugadorController.GetInstancia;
        PeleaController CntPelea;


        public void Start()
        {
            CntEntidad = EntidadController.Instancia;
        }

        public void CargarConObjTerrenosLimitrofes(Terreno terrenoActual)
        {
            foreach (Terreno terreno in terrenoActual.TERRENOSLIMITROFES)
            {
                if (terreno.TIPOSUBTERRENO.TIPOTERRENO is not Especial)
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

            if (cantidadEnemigos > 0)
            {
                for (int i = 0; i < cantidadEnemigos; i++)
                {
                    Enemigo enemigo = CntEntidad.ObtenerEnemigoAleatorio();
                    if (enemigo != null)
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

            if (cantidadComida > 0)
            {
                for (int i = 0; i < cantidadComida; i++)
                {
                    IInteractuable interactuable = CntComida.ObtenerComidaAleatoria();
                    if (interactuable != null)
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

        public void InstanciarPersonajeSeleccionadoEnTerreno(Terreno terreno)
        {
            if (CntJugador.PLAYER.PERSONAJESELECCIONADO != null)
            {
                GameObject nuevaInstancia;
                try
                {
                    DespachadorHiloPrincipal.Instancia.Enqueue(() =>
                    {
                        nuevaInstancia = ControllerEscenas.Instancia.InstanciarModelo(CntJugador.PLAYER.PERSONAJESELECCIONADO.PERSONAJEPREFAB.gameObject, terreno.POSICIONTRIDIMENSIONAL + new Vector3(0, 0.6f, 0));
                        Debug.Log("Instancia creada: " + nuevaInstancia.name);
                        if (nuevaInstancia != null)
                        {
                            terreno.CambiarEstado(new Ocupado());
                            CntJugador.PLAYER.PERSONAJESELECCIONADO.TERRENOACTUAL = terreno;
                            CntJugador.PLAYER.PERSONAJESELECCIONADO.INSTANCIAPERSONAJE = nuevaInstancia;
                            terreno.AgregarEntidad(CntJugador.PLAYER.PERSONAJESELECCIONADO);

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

        public bool InteractuarConObjeto(IInteractuable interactuable)
        {
            if (interactuable.Interactuar(CntJugador.PLAYER.PERSONAJESELECCIONADO))
            {
                CntJugador.PLAYER.PERSONAJESELECCIONADO.TERRENOACTUAL.INTERACTUABLES.Remove(interactuable);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void IniciarPelea(ICombate enemigo)
        {
            CntPelea = new PeleaController(new Pelea((ICombate)CntJugador.PLAYER.PERSONAJESELECCIONADO, enemigo));
            CntPelea.OnPeleaFinalizada += ManejarPeleaFinalizada;
        }

        private void ManejarPeleaFinalizada()
        {
            Debug.Log("Pelea Finalizada");
            //ControllerEscenas.Instancia.EliminarEscena(4);
            var ganador = CntPelea.PELEAACTUAL.ObtenerGanador();
            Debug.Log($"El ganador es: {ganador.ObtenerNombre()}");
            if(ganador is Personaje)
            {
                // logica si el personaje osea jugador gana
                EliminarPerdedorDelTerreno();
            }
            else
            {

            }
            CntPelea.PELEAACTUAL.ObtenerGanador();
            // Actualizar UI, reiniciar escena, etc.
        }

        public void EliminarPerdedorDelTerreno()
        {
          var perdedorDePelea = (Entidad)CntPelea.PELEAACTUAL.ObtenerNoGanador(CntPelea.PELEAACTUAL.PERSONAJE, CntPelea.PELEAACTUAL.ENEMIGO);
          CntEntidad.EliminarEntidad(perdedorDePelea);

        }
        public PeleaController ObtenerPeleaController()
        {
            return CntPelea;
        }
    }
}
