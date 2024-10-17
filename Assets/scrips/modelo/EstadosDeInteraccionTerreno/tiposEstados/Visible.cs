using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Visible : IEstadoHexEstrategia
{
    public void ActivarEstado(Terreno terreno)
    {
        if (terreno.TRANSFTERRENO != null && !terreno.TRANSFTERRENO.gameObject.activeSelf)
        {
            terreno.TRANSFTERRENO.gameObject.SetActive(true);
        }
    }

    public void DesactivarEstado(Terreno terreno)
    {
        if  (terreno.TRANSFTERRENO != null && !terreno.TRANSFTERRENO.gameObject.activeSelf)
        {
            terreno.TRANSFTERRENO.gameObject.SetActive(false);
        }
    }

}
