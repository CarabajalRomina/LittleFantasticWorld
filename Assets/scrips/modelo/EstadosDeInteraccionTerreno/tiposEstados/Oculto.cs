using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oculto : IEstadoHexEstrategia
{
    public void ActivarEstado(Terreno terreno)
    {
        if (terreno.TRANSFTERRENO != null && terreno.TRANSFTERRENO.gameObject.activeSelf)
        {
            terreno.TRANSFTERRENO.gameObject.SetActive(false);
        }
    }

    public void DesactivarEstado(Terreno terreno)
    {
        if (terreno.TRANSFTERRENO != null && !terreno.TRANSFTERRENO.gameObject.activeSelf)
        {
            terreno.TRANSFTERRENO.gameObject.SetActive(true);
        }
    }

    public IEstadoHexEstrategia Seleccionar()
    {
        throw new System.NotImplementedException();
    }
    public IEstadoHexEstrategia Deseleccionar()
    {
        throw new System.NotImplementedException();
    }
}
