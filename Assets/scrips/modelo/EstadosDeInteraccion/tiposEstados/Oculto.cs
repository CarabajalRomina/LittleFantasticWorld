using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oculto : IEstadoHexEstrategia
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

    public IEstadoHexEstrategia OnMouseEnter()
    {
        return this;
    }

    public IEstadoHexEstrategia OnMouseExit()
    {
        return this;
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
