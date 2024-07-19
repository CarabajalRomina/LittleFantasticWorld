using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dado 
{
    public static int TirarDado(int minCaras, int maxCaras)
    {
        return Random.Range(minCaras, maxCaras + 1);
    }
}
