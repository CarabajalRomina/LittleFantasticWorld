using Assets.scrips.interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tierra : MonoBehaviour, ITipoTerreno
{
    public Type TipoDeTerreno()
    {
        return typeof(This);
    }
}
