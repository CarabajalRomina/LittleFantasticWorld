using Assets.scrips.interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

namespace Assets.scrips.Controllers.turno
{
    public class TurnoController
    {
        private List<ICombate> Participantes;
        private int IndiceTurnoActual;
        private bool AccionRealizada = false;


        // Evento para notificar el cambio de turno a los suscriptores/ espectadores
        public event Action<ICombate> OnTurnoCambiado;

        public bool ACCIONREALIZADA
        {
            get { return AccionRealizada; }
            set { AccionRealizada = value; }
        }


        public TurnoController(List<ICombate> participantes)
        {
            Participantes = participantes;
            IndiceTurnoActual = 0;
        }

        public void SiguienteTurno()
        {
            if(AccionRealizada)
            {
                IndiceTurnoActual = (IndiceTurnoActual + 1) % Participantes.Count;
                AccionRealizada = false;
                OnTurnoCambiado?.Invoke(Participantes[IndiceTurnoActual]);
            }
        }
       
        public ICombate ObtenerTurnoActual()
        {
            return Participantes[IndiceTurnoActual];
        }
    }
}
