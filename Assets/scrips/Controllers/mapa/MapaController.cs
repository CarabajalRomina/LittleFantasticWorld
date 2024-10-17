using Assets.scrips.interfaces.interactuable;
using Assets.scrips.modelo.configuraciones;
using Assets.scrips.modelo.entidad;
using UnityEngine;


namespace Assets.scrips.Controllers.mapa
{
    public class MapaController: Singleton<MapaController>
    {
        public void CargarConObjTerrenosLimitrofes(Terreno terrenoActual)
        {
            foreach (Terreno terreno in terrenoActual.TERRENOSLIMITROFES)
            {
                // Verifica si el terreno no está cargado
                if (terreno.ENTIDADES == null) 
                {
                    AsignarEnemigosProcedural(terreno); // Cargar enemigos solo si el terreno no está cargado

                    if (terreno.INTERACTUABLES == null)
                    {
                        AsignarInteractuablesProcedural(terreno); // Cargar interactuable solo si el terreno no está cargado
                    
                    }
                    else{Debug.Log("Ya esta cargado con interactuable");}                  
                }
                else { Debug.Log("Ya esta cargado con entidades"); }
            }
        }

        private void AsignarEnemigosProcedural(Terreno terreno)
        {
            int cantidadEnemigos = Utilidades.GenerarNumeroAleatorio(0, ConfiguracionGeneral.CantMaxEnemigosXTerreno + 1);
            
            if(cantidadEnemigos > 0)
            {
                for (int i = 0; i < cantidadEnemigos; i++)
                {
                    //Enemigo enemigo = ctrlEnemigos.ObtenerEnemigoAleatorio();
                   // terreno.AgregarEntidad(enemigo);
                }
            }  
        }

        private void AsignarInteractuablesProcedural(Terreno terreno)
        {
            int cantidadInteractuables = Utilidades.GenerarNumeroAleatorio(0, ConfiguracionGeneral.CantidadMaxInteractuablesXTerreno + 1);

            if(cantidadInteractuables > 0)
            {
                for (int i = 0; i < cantidadInteractuables; i++)
                {
                   // IInteractuable interactuable = ctrlInteractuables.ObtenerInteractuableAleatorio();
                    //terreno.AñadirInteractuable(interactuable);
                }
            }
           
        }








    }
}
