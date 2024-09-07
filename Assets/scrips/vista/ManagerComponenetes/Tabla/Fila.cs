using Assets.scrips.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Fila
{
    int Index;
    GameObject FilaPrefab;
    object Objeto;
    EstadoBoton EstadoFila;

    public Fila(int index, GameObject filaPrefab, object objeto)
    {
        INDEX = index;
        FILAPREFAB = filaPrefab;
        OBJETO = objeto;
        EstadoFila = new EstadoBoton(); 
    }

    #region PROPIEDADES
    public int INDEX
    {
        get { return Index; } 
        set { Index = value; }
    }

    public GameObject FILAPREFAB
    {
        get { return FilaPrefab; }
        set { FilaPrefab = value; }
    }

    public object OBJETO
    {
        get { return Objeto; }
        set { Objeto = value; }
    }

    public EstadoBoton ESTADOFILA
    {
        get { return EstadoFila; }
        set { EstadoFila = value; }
    }
    #endregion


    public void EliminarFila()
    {
       Object.Destroy(FilaPrefab);
    }
}
