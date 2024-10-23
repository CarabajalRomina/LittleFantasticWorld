using Assets.scrips.Controllers.entidad;
using System.Linq;
using UnityEngine;


namespace Assets.scrips.Controllers
{
    public class ControllerMovimiento : SingletonMonoBehaviour<ControllerMovimiento>
    {
        Personaje PersonajeSeleccionado { get; set; }
        Terreno TerrenoDestino;

        private void Start()
        {
            EntidadController CntPersonaje = EntidadController.Instancia;
            PersonajeSeleccionado = (Personaje)CntPersonaje.GetPersonajes().FirstOrDefault();
        }

        private void Update()
        {
            if (PersonajeSeleccionado != null && TerrenoDestino != null && PersonajeSeleccionado.TERRENOACTUAL.POSICIONTRIDIMENSIONAL != TerrenoDestino.POSICIONTRIDIMENSIONAL)
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
                        return true;
                    }
                    else
                    {
                        terrenoDestino.ESTADO.DesactivarEstado(TerrenoDestino);
                        Debug.Log("no puede ir a un habitat a la que no esta adaptado...");
                        return false;
                    }
                }
                else{ return false;}
            }
            else{ return false;}
        }
    }
}
