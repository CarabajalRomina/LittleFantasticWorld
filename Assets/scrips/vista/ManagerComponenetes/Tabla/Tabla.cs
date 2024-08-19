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
    private int siguienteIndex = 0;

 
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
        
        Filas.Add( new Fila(nuevoIndex,nuevaFila,objeto));

        Button botonFila = nuevaFila.GetComponentInChildren<Button>();
        botonFila.onClick.AddListener(() => FilaSeleccionada(Filas[nuevoIndex]));
    }

    public void FilaSeleccionada(Fila fila)
    {
        if (FilasSeleccionadas.Contains(fila))
        {
            FilasSeleccionadas.Add(fila);   
        }
        else
        {
            if (FilasSeleccionadas.Contains(fila))
            {
                FilasSeleccionadas.Remove(fila);
            }
        }
    }
    public void ClearTable()
    {
        Filas.Clear();
        FilasSeleccionadas.Clear();

        for(var i = 1; i < Contenedor.childCount; i++)
        {
           Destroy(Contenedor.GetChild(i).transform.gameObject);
        }

    }

    public void QuitarFila(Fila fila)
    {
        if (Filas.Contains(fila))
        {
            Destroy(fila.FILAPREFAB);

            Filas.Remove(fila);

            if (FilasSeleccionadas.Contains(fila))
            {
                FilasSeleccionadas.Remove(fila);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró la fila con el índice: " + fila.INDEX);
        }
    }

}
