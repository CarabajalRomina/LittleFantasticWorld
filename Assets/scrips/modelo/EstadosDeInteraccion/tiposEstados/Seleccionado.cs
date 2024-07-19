using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccionado : IEstadoHex
{
    private EstadosDeInteraccion Estado = EstadosDeInteraccion.Seleccionado;

    #region  PROPIEDADES
    public EstadosDeInteraccion ESTADO
    {
        get { return Estado; }
    }
    #endregion

 

    public void ActivarEstado(Transform prefaHex)
    {
        Debug.Log("Estoy entrando en stado seleccionado");
        throw new System.NotImplementedException();
    }

    public void DesactivarEstado(Transform prefaHex)
    {
        Debug.Log("Estoy saliendo en stado seleccionado");

        throw new System.NotImplementedException();
    }

    public IEstadoHex OnMouseEnter()
    {
        throw new System.NotImplementedException();
    }

    public IEstadoHex OnMouseExit()
    {
        throw new System.NotImplementedException();
    }

    public IEstadoHex Seleccionar()
    {
        throw new System.NotImplementedException();
    }
    public IEstadoHex Deseleccionar()
    {
        return new Visible();
    }

}
