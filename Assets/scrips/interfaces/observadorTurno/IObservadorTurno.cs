using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.interfaces.observadorTurno
{
    //para que los observadores reacciones a los cambios de turno
    public interface IObservadorTurno
    {
        void NotificarCambioTurno(bool esTurnoDelJugador);
    }
}
