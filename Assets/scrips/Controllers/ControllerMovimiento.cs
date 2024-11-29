using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.juego;
using Assets.scrips.Controllers.jugador;
using Assets.scrips.modelo.configuraciones;
using System.Linq;
using UnityEngine;


namespace Assets.scrips.Controllers
{
    public class ControllerMovimiento : SingletonMonoBehaviour<ControllerMovimiento>
    {
        JugadorController CntJugador;
        Terreno TerrenoDestino;
        JuegoController CntJuego;
        Personaje PersonajeSeleccionado;

        private void Start()
        {
            EntidadController CntPersonaje = EntidadController.Instancia;
            CntJuego = JuegoController.Instancia;
            CntJugador = JugadorController.GetInstancia;
            PersonajeSeleccionado = CntJugador.PLAYER.PERSONAJESELECCIONADO;
        }

        public Personaje PERSONAJESELECCIONADO
        {
            get { return PersonajeSeleccionado; }
        }

        private void Update()
        {
            if(TerrenoDestino != null && PersonajeSeleccionado.TERRENOACTUAL.POSICIONTRIDIMENSIONAL != TerrenoDestino.POSICIONTRIDIMENSIONAL)
            {
                // Llama al método Mover del personaje seleccionado
                PersonajeSeleccionado.MoverHacia(TerrenoDestino);
            }
        }

        public bool MoverPersonaje(Terreno terrenoDestino)
        {
            TerrenoDestino = terrenoDestino;
            if (PersonajeSeleccionado != null)
            {
                if (PersonajeSeleccionado.TERRENOACTUAL.TERRENOSLIMITROFES.Contains(TerrenoDestino))
                {
                    if (PersonajeSeleccionado.HABITATS.PuedoMoverme(terrenoDestino.TIPOSUBTERRENO.TIPOTERRENO))
                    {
                        PersonajeSeleccionado.IniciarMovimiento();
                        if (CntJuego != null)
                        {
                            CntJuego.CargarConObjTerrenosLimitrofes(TerrenoDestino);
                        }

                        return true;
                    }
                    else
                    {
                        terrenoDestino.ESTADO.DesactivarEstado(TerrenoDestino);
                        Debug.Log("no puede ir a un habitat a la que no esta adaptado...");
                        return false;
                    }
                }
                else { return false; }
            }
            else { return false; }
        }
    }
}
