using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resaltado : IEstadoHexEstrategia
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

    public IEstadoHexEstrategia OnMouseEnter()
    {
        return this;
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
