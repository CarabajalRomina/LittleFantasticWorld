using Assets.scrips.Controllers.entidad;
using System.Linq;


namespace Assets.scrips.Controllers
{
    public class ControllerMovimiento : SingletonMonoBehaviour<ControllerMovimiento >
    {
        Personaje PersonajeSeleccionado;
        Terreno TerrenoDestino;

        private void Start()
        {
            EntidadController CntPersonaje = EntidadController.Instancia;
            PersonajeSeleccionado = (Personaje)CntPersonaje.ENTIDADES.First();
        }

        private void Update()
        {
            if (PersonajeSeleccionado != null && TerrenoDestino != null && PersonajeSeleccionado.TERRENOACTUAL.POSICIONTRIDIMENSIONAL != TerrenoDestino.POSICIONTRIDIMENSIONAL)
            {
                // Llama al método Mover del personaje seleccionado
                PersonajeSeleccionado.MoverHacia(TerrenoDestino);  
            }
        }

        public void MoverPersonaje(Terreno terrenoDestino)
        {
            TerrenoDestino = terrenoDestino;

            if (PersonajeSeleccionado != null )
            {
                PersonajeSeleccionado.IniciarMovimiento();
            }
        }
    }
}
