using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oculto : IEstadoHex
{
    private EstadosDeInteraccion Estado = EstadosDeInteraccion.Oculto;

    #region  PROPIEDADES
    public EstadosDeInteraccion ESTADO
    {
        get { return Estado; }
    }
    #endregion

   

    public void ActivarEstado(Transform prefaHex)
    {
        if (prefaHex != null && !prefaHex.gameObject.activeSelf)
        {
            prefaHex.gameObject.SetActive(false);
        }
    }

    public void DesactivarEstado(Transform prefaHex)
    {
        if (prefaHex != null && !prefaHex.gameObject.activeSelf)
        {
            prefaHex.gameObject.SetActive(true);
        }
    }

    public IEstadoHex OnMouseEnter()
    {
        return this;
    }

    public IEstadoHex OnMouseExit()
    {
        return this;
    }

    public IEstadoHex Seleccionar()
    {
        throw new System.NotImplementedException();
    }
    public IEstadoHex Deseleccionar()
    {
        throw new System.NotImplementedException();
    }
}
