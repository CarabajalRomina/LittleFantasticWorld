﻿using Assets.scrips.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.modelo.estadosDeInteraccion.tiposEstados
{
    public class Seleccionado : IEstadoHexEstrategia
    {
        private ControllerMovimiento  CntMovimiento;

        public Seleccionado(ControllerMovimiento  cntMovimiento) 
        {
            CntMovimiento = cntMovimiento;
        }
        public void ActivarEstado(Terreno terreno)
        {
            if (!CntMovimiento.MoverPersonaje(terreno))
            {
                terreno.ESTADO.DesactivarEstado(terreno);
                Debug.Log("UPPS, no tiene energia para moverte a este terreno");
            }
        }

        public void DesactivarEstado(Terreno terreno)
        {
            terreno.ESTADO = new Visible();
        }
    }
}
