using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : IEstadoHexEstrategia
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

    public IEstadoHexEstrategia OnMouseEnter()
    {
        return new Resaltado();
    }

    public IEstadoHexEstrategia OnMouseExit()
    {
        throw new System.NotImplementedException();
    }

    public IEstadoHexEstrategia Seleccionar()
    {
        return new Seleccionado();
    }

    public IEstadoHexEstrategia Deseleccionar()
    {
        throw new System.NotImplementedException();
    }
}
