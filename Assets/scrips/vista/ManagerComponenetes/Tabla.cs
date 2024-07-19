using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;


public abstract class Tabla<T> : MonoBehaviour
{
    public GameObject FilaPrefab;
    public Transform Contenedor;
    private Dictionary<int, KeyValuePair<GameObject, object>> Filas = new Dictionary<int, KeyValuePair<GameObject, object>>();
    private List<KeyValuePair<GameObject, object>> FilasSeleccionadas = new List<KeyValuePair<GameObject, object>>();
    private int siguienteIndex = 0;


    public void CargarTabla(List<T> objetos)
    {
        ClearTable();
        foreach (var objeto in objetos)
        {
            AgregarFila(objeto);
        }
    }

    public void AgregarFila(T objeto)
    {
        int nuevoIndex = siguienteIndex++;

        GameObject nuevaFila = Instantiate(FilaPrefab, Contenedor);
        Text[] celdas = nuevaFila.GetComponentsInChildren<Text>();

        PropertyInfo[] propiedades = typeof(T).GetProperties();

        for (int i = 0; i < celdas.Length && i < propiedades.Length; i++)
        {
            celdas[i].text = propiedades[i].GetValue(objeto)?.ToString();
        }


        // Agregar la fila visual y los datos del animal al diccionario con su índice como clave
        Filas[nuevoIndex] = new KeyValuePair<GameObject, object>(nuevaFila, objeto);

        // Agregar un listener al botón de la fila para manejar la selección
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
        foreach (Transform child in Contenedor)
        {
            Destroy(child.gameObject);
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
