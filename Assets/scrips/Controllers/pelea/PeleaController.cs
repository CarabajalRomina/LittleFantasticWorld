using Assets.scrips.Controllers.turno;
using Assets.scrips.interfaces;
using Assets.scrips.interfaces.pelea;
using Assets.scrips.modelo.pelea;
using Assets.scrips.modelo.pelea.acciones;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scrips.Controllers.pelea
{
    public class PeleaController
    {
        public event Action<bool> OnTurnoJugador; // Notifica si es el turno del jugador (true) o no (false)
        public event Action OnPeleaFinalizada;

        private Pelea PeleaActual { get; set; }
        private TurnoController CntTurno;


        public Pelea PELEAACTUAL
        {
            get { return PeleaActual; }
            set { PeleaActual = value; }
        }

        public PeleaController(Pelea pelea1vs1)
        {
            PeleaActual = pelea1vs1;

            CntTurno = new TurnoController(new List<ICombate> { pelea1vs1.PERSONAJE, pelea1vs1.ENEMIGO});
            CntTurno.OnTurnoCambiado += ManejarCambioDeTurno;

        }

        /*
        public void IniciarPelea()
        {
            Debug.Log("Inicio la pelea");

            ManejarCambioDeTurno(CntTurno.ObtenerTurnoActual());
        }*/

        public void ManejarCambioDeTurno(ICombate combatiente)
        {
            if (PeleaActual.PERSONAJE.EstaVivo() && PeleaActual.ENEMIGO.EstaVivo())
            {
                bool esTurnoDelJugador = combatiente is Personaje;
                if (esTurnoDelJugador)
                {
                    // Notificar si es el turno del jugador
                    OnTurnoJugador?.Invoke(esTurnoDelJugador);

                    // Notificar a la UI que es el turno del jugador
                    Debug.Log("Es el turno del jugador.");
                }
                else
                {
                    EjecutarTurnoEnemigo(combatiente);
                }
            }
            else
            {
                PeleaActual.Finalizar();
                OnPeleaFinalizada?.Invoke();
            }
        }

        private void EjecutarTurnoEnemigo(ICombate enemigo)
        {

            IAccionCombate accion = new Atacar();
            enemigo.EjecutarAccion(accion, PeleaActual.PERSONAJE);
            CntTurno.ACCIONREALIZADA = true;
            CntTurno.SiguienteTurno();

        }

        public void EjecutarAccionJugador(IAccionCombate accion, ICombate objetivo)
        {
            var jugador = CntTurno.ObtenerTurnoActual();
            if (jugador is Personaje)
            {
                jugador.EjecutarAccion(accion, objetivo);
                CntTurno.ACCIONREALIZADA = true;
                CntTurno.SiguienteTurno();
            }
        }


        public void InstanciarPersonajesEnEscena(Vector3 puntoAparicionPersonaje, Vector3 puntoAparicionEnemigo,Vector3 rotacionEnemigo, Vector3 escala)
        {
            var CntEscena = ControllerEscenas.Instancia;
            CntEscena.InstanciarModelo(PeleaActual.PERSONAJE.ObtenerPrefab(), puntoAparicionPersonaje, escala, Quaternion.identity);
            CntEscena.InstanciarModelo(PeleaActual.ENEMIGO.ObtenerPrefab(), puntoAparicionEnemigo ,rotacionEnemigo);
        }

        public ICombate DeterminarGanador()
        {
            return PeleaActual.ObtenerGanador();
        }

    }
}
