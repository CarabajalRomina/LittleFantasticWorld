using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.jugador
{
    public class Jugador
    {
        int Id;
        static int GlobalCount = 0;
        Personaje PersonajeSeleccionado;
        string NombreJugador;


        public Jugador(string nombreJugador) 
        {
            Id = ++GlobalCount;
            NOMBREJUGADOR = nombreJugador;
        }

        #region PROPIEDADES
        public int ID
        {
            get { return Id; }
        }
        public Personaje PERSONAJESELECCIONADO
        {
            get { return PersonajeSeleccionado; }
            set { PersonajeSeleccionado = value; }
        }

        public string NOMBREJUGADOR
        {
            get { return NombreJugador; }
            set { NombreJugador = value;}
        }
        #endregion
    }
}
