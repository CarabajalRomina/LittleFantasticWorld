using Assets.scrips.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CntMovimiento.MoverPersonaje(terreno);
            terreno.CambiarEstado(new Ocupado());
        }

        public void DesactivarEstado(Terreno terreno)
        {
            terreno.CambiarEstado(new Visible());
        }
    }
}
