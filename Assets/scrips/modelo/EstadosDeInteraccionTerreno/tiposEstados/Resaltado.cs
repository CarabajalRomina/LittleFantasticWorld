using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resaltado : IEstadoHexEstrategia
{

    public void ActivarEstado(Terreno terreno)
    {
        LeanTween.scale(terreno.TRANSFTERRENO.gameObject, Vector3.one * 3.6f, 0.2f).
       setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(terreno.TRANSFTERRENO.gameObject, .05f, 0.2f).setEase(
            LeanTweenType.linear);
    }

    public void DesactivarEstado(Terreno terreno)
    {
        LeanTween.scale(terreno.TRANSFTERRENO.gameObject, new Vector3(3,3,3), 0.2f).setEase(
            LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(terreno.TRANSFTERRENO.gameObject, 0f, 0.2f).setEase(
            LeanTweenType.easeOutBack);
    }
}
