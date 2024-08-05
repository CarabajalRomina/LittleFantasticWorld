using Assets.scrips.interfaces.IEstrategias;
using Assets.scrips.interfaces.interactuable;
using Assets.scrips.modelo.Entidad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IInteractuable
{
    int Id;
    static int GlobalCount;
    string Nombre;
    IEstrategiaItem Estrategias;

    public Item(string nombre, IEstrategiaItem estrategias)
    {
        Id = ++GlobalCount;
        NOMBRE = nombre;
        ESTRATEGIAS = estrategias;
    }

    #region PROPIEDADES
    public int ID
    {
        get { return Id; }
    }
    public string NOMBRE
    {
        get { return Nombre; }
        set
        {
            if (value != null)
            {
                Nombre = value;
            }
        }
    }

    public IEstrategiaItem ESTRATEGIAS
    {
        get { return Estrategias; }
        set
        {
            if (value != null)
            {
                Estrategias = value;
            }
        }

    }


    #endregion

    public void Interactuar(Personaje entidad)
    {
        Estrategias.AplicarEfecto(entidad);
    }
}
