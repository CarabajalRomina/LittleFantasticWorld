using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resaltado : IEstadoHex
{
    private EstadosDeInteraccion Estado = EstadosDeInteraccion.Resaltado;

    #region  PROPIEDADES
    public EstadosDeInteraccion ESTADO
    {
        get { return Estado; }
    }
    #endregion

  

    public void ActivarEstado(Transform prefaHex)
    {
        LeanTween.scale(prefaHex.gameObject, Vector3.one * 1.2f, 0.2f).
            setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(prefaHex.gameObject, .2f, 0.2f).setEase(
            LeanTweenType.linear);
       // ControllerCamara.Instantiate.SeleccionarAccion += terreno.OnSeleccionar;
    }

    public void DesactivarEstado(Transform prefaHex)
    {
        LeanTween.scale(prefaHex.gameObject, Vector3.one, 0.2f).setEase(
            LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(prefaHex.gameObject, 0f, 0.2f).setEase(
            LeanTweenType.easeOutBack);
        // ControllerCamara.Instantiate.SeleccionarAccion -= terreno.OnSeleccionar;

    }

    public IEstadoHex OnMouseEnter()
    {
        return this;
    }

    public IEstadoHex OnMouseExit()
    {
        return new Visible();
    }

    public IEstadoHex Seleccionar()
    {
        return new Seleccionado();
    }
    public IEstadoHex Deseleccionar()
    {
        return new Visible();
    }

}
