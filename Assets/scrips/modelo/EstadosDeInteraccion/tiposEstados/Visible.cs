using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : IEstadoHex
{
    private EstadosDeInteraccion Estado = EstadosDeInteraccion.Visible;

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
            prefaHex.gameObject.SetActive(true);
        }
    }

    public void DesactivarEstado(Transform prefaHex)
    {
        if  (prefaHex != null && !prefaHex.gameObject.activeSelf)
        {
            prefaHex.gameObject.SetActive(false);
        }
    }

    public IEstadoHex OnMouseEnter()
    {
        return new Resaltado();
    }

    public IEstadoHex OnMouseExit()
    {
        throw new System.NotImplementedException();
    }

    public IEstadoHex Seleccionar()
    {
        return new Seleccionado();
    }

    public IEstadoHex Deseleccionar()
    {
        throw new System.NotImplementedException();
    }
}
