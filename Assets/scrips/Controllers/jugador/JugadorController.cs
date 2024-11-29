using Assets.scrips.modelo.jugador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.Controllers.jugador
{
    public class JugadorController: Singleton<JugadorController>
    {
        Jugador Player;
        HashSet<string> NombresSeleccionados = new HashSet<string>();

        #region PROPIEDADES
        public Jugador PLAYER
        {
            get { return Player; }
            set { Player = value; }
        }
        #endregion

        public bool CrearUsuario(string nombreUsurio)
        {
            if (!NombresSeleccionados.Contains(nombreUsurio))
            {
                try
                {
                    Player = new Jugador(nombreUsurio);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.ToString());
                    return false;
                }


            }
            else
            {
                return false;
            }
        }
    }
}
