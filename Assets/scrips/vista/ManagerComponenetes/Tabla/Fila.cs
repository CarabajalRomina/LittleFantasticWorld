using Assets.scrips.interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fila
{
    int Index;
    GameObject FilaPrefab;
    object Objeto;

    public Fila(int index, GameObject filaPrefab, object objeto)
    {
        INDEX = index;
        FILAPREFAB = filaPrefab;
        OBJETO = objeto;
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
    #endregion
}
