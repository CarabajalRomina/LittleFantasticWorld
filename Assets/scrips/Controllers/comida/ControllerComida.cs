using Assets.scrips.fabricas.entidades.enemigos;
using Assets.scrips.modelo.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;  

namespace Assets.scrips.Controllers.comida
{
    public class ControllerComida: Singleton<ControllerComida>
    {
        List<Comida> Comidas = new List<Comida>();
        HashSet<string> NombresComidaSeleccionados = new HashSet<string>();

        public List<Comida> COMIDAS
        {
            get { return Comidas; }
            set { Comidas = value; }
        }

        public HashSet<string> NOMBRECOMIDASELECCIONADOS
        {
            get { return NombresComidaSeleccionados; }
            set { NombresComidaSeleccionados = value; }
        }

        public bool CrearComida(string nombre, IDieta tipoDieta, int calorias)
        {
            try
            {
                Comida comida = new Comida(nombre, calorias, tipoDieta);
                if(comida != null )
                    Comidas.Add(comida);
                return true;

            }catch(Exception ex) 
            {
                Debug.LogError($"Error al crear la comida: {ex.Message}");
                return false;
            }   
        }

        public bool EditarComida(Comida comida, string nombre, IDieta tipoDieta, int calorias)
        {
            try
            {
                comida.NOMBRE = nombre;
                comida.TIPODIETA = tipoDieta;
                comida.CALORIAS = calorias;
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error al crear la comida: {e.Message}");
                return false;
            }
        }

        public bool EliminarComida(Comida comida)
        {
            try
            {
                if (COMIDAS.Contains(comida))
                {
                    COMIDAS.Remove(comida);
                    return true;
                }
                else
                {
                    Debug.Log("No se encuentra la comida");
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error al crear la comida: {e.Message}");
                return false;
            }
        }

    }
}
