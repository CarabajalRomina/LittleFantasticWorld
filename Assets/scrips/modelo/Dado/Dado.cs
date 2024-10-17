using Assets.scrips;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dado 
{
    static int MinCaras = 1;
    static int MaxCaras = 6;

    public static int TirarDado()
    {
        return Utilidades.GenerarNumeroAleatorio(MinCaras, MaxCaras + 1);
    }
}
