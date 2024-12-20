using Assets.scrips.interfaces;

namespace Assets.scrips.modelo.pelea
{
    public class Pelea
    {

        private ICombate Personaje;
        private ICombate Enemigo;
        private bool Finalizada;

       public ICombate PERSONAJE
        {
            get { return Personaje; }
            set { Personaje = value; }
        }

        public ICombate ENEMIGO
        {
            get { return Enemigo; }
            set { Enemigo = value; }
        }

        public bool FINALIZADA
        {
            get { return Finalizada; }
            set { Finalizada = value; }
        }
        
        public Pelea(ICombate personaje, ICombate enemigo)
        {
            Personaje = personaje;
            Enemigo = enemigo;
            Finalizada = false;
        }


        public void Finalizar()
        {
            Finalizada = true;            
        }

        public ICombate ObtenerGanador()
        {
            if (!Personaje.EstaVivo()) return Enemigo;
            if (!Enemigo.EstaVivo()) return Personaje;
            return null;
        }

        public ICombate ObtenerNoGanador(ICombate personaje, ICombate enemigo)
        {
            var ganador = ObtenerGanador();

            if (ganador == null) return null; //empate

            if (ganador == personaje)
                return personaje;
            else 
                return enemigo; 
        }
    }
}
