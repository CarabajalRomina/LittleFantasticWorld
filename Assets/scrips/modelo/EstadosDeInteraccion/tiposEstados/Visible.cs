using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting.Antlr3.Runtime;
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
        return new Visible();
    }

    public IEstadoHexEstrategia Seleccionar()
    {
        return new Seleccionado();
    }

    public IEstadoHexEstrategia Deseleccionar()
    {
        return new Visible();
    }
}
