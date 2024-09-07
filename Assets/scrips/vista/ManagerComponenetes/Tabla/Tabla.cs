using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using TMPro;
using System;
using Assets.scrips.interfaces;
using UnityEditor.EditorTools;


public class Tabla : MonoBehaviour
{
    public GameObject FilaPrefab;
    public Transform Contenedor;

    public List<Fila> Filas = new List<Fila>();
    public HashSet<Fila> FilasSeleccionadas = new HashSet<Fila>();
    private int siguienteIndex;

 
    public void CargarTabla<T>(List<T> objetos) where T : IDescribible
    {
        ClearTable();
        foreach (var objeto in objetos)
        {
            AgregarFila(objeto);
        }
    }

    public void AgregarFila<T>(T objeto) where T : IDescribible
    {
        int nuevoIndex = siguienteIndex++;

        GameObject nuevaFila = Instantiate(FilaPrefab, Contenedor);
        TMP_Text[] celdas = nuevaFila.GetComponentsInChildren<TMP_Text>();

        string[] propiedades = objeto.ObtenerValoresInstancias();

        for (int i = 0; i < celdas.Length && i < propiedades.Length; i++)
        {
            celdas[i].text = propiedades[i];
        }
        Fila fila = new Fila(nuevoIndex, nuevaFila, objeto);
        Filas.Add(fila);

        Button botonFila = nuevaFila.GetComponentInChildren<Button>();
        botonFila.onClick.AddListener(() => FilaSeleccionada(fila));
    }

    public void ActualizarFila<T>(Fila fila, T objeto) where T : IDescribible
    {
        TMP_Text[] celdas = fila.FILAPREFAB.GetComponentsInChildren<TMP_Text>();

        string[] propiedades = objeto.ObtenerValoresInstancias();

        for (int i = 0; i < celdas.Length && i < propiedades.Length; i++)
        {
            celdas[i].text = propiedades[i];
        }

        fila.FILAPREFAB = fila.FILAPREFAB;
        fila.OBJETO = objeto;
        FilaSeleccionada(fila);
    }
        
    public void FilaSeleccionada(Fila fila)
    {
        var btnFila = FilaPrefab.GetComponentInChildren<Button>();

        if (btnFila != null)
        {
            fila.ESTADOFILA.CambiarEstadoBoton(btnFila, btnFila.GetComponent<Image>());
            if (fila.ESTADOFILA.FUESELECCIONADO)
            {
                FilasSeleccionadas.Add(fila);
            }
            else
            {
                FilasSeleccionadas.Remove(fila);
            }

            if (FilasSeleccionadas.Count == 0)
            {
                Debug.Log("NO HAY FILAS CARGADAS");
            }
            else
            {
                foreach (var item in FilasSeleccionadas)
                {
                    Debug.Log(item.INDEX);
                }
            }
               
        }
    }  
    
    public void ClearTable()
    {
        if (Filas.Count > 0)
        {
            FilasSeleccionadas.Clear();
            foreach (var fila in Filas)
            {
                fila.EliminarFila();
            }
            Filas.Clear();
            siguienteIndex = 0;
        }
        for (int i = 1; i < Contenedor.childCount; i++)  // Empieza desde el segundo hijo (índice 1)
        {
            Transform child = Contenedor.GetChild(i);
            Destroy(child.gameObject);  // Destruye los hijos excepto el primero.
        }
    }

    public void QuitarFila(Fila fila)
    {
        if (Filas.Contains(fila))
        {
            fila.EliminarFila();
            Filas.Remove(fila);
        }
        else
        {
            Debug.LogWarning("No se encontró la fila con el índice: " + fila.INDEX);
        }
    }

    public void HabilitarODeshabilitarInteractividadTabla()
    {
        foreach (Transform child in Contenedor)
        {
            Button boton = child.GetComponentInChildren<Button>();
            if (boton != null)
            {
                boton.interactable = !boton.interactable;
            }
        }
    }
}
