using Assets.scrips.interfaces.interactuable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida: IInteractuable
{
    private int calorias;

    public int CALORIAS {  get; private set; }
    public void Interactuar(Personaje entidad)
    {
        throw new System.NotImplementedException();
    }
}
