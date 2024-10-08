using Assets.scrips;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dado 
{

    public static int TirarDado(int minCaras, int maxCaras)
    {
        return Utilidades.GenerarNumeroAleatorio(minCaras, maxCaras + 1);
    }
}
