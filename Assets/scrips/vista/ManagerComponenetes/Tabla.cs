using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using TMPro;
using System;
using Assets.scrips.interfaces;


public class Tabla : MonoBehaviour
{
    public GameObject FilaPrefab;
    public Transform Contenedor;
    private Dictionary<int, KeyValuePair<GameObject, object>> Filas = new Dictionary<int, KeyValuePair<GameObject, object>>();
    private List<KeyValuePair<GameObject, object>> FilasSeleccionadas = new List<KeyValuePair<GameObject, object>>();
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

        Filas[nuevoIndex] = new KeyValuePair<GameObject, object>(nuevaFila, objeto);

        Button botonFila = nuevaFila.GetComponentInChildren<Button>();
        botonFila.onClick.AddListener(() => FilaSeleccionada(nuevoIndex));
    }

    public void FilaSeleccionada(int index)
    {
        if (Filas.ContainsKey(index))
        {
            FilasSeleccionadas.Add(Filas[index]);   
        }
        else
        {
            if (FilasSeleccionadas.Contains(Filas[index]))
            {
                FilasSeleccionadas.Remove(Filas[index]);
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

    public void QuitarFila(int index)
    {
        if (Filas.ContainsKey(index))
        {
            KeyValuePair<GameObject, object> filaSeleccionada = Filas[index];

            Destroy(filaSeleccionada.Key);

            Filas.Remove(index);

            if (FilasSeleccionadas.Contains(Filas[index]))
            {
                FilasSeleccionadas.Remove(Filas[index]);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró la fila con el índice: " + index);
        }
    }

}
