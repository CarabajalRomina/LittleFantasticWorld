using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EstadoBoton 
{
    private bool FueSeleccionado;
    public Color ColorNormal= Color.red;
    public Color ColorSeleccionado = Color.cyan;

    public EstadoBoton() { FUESELECCIONADO = false; }

    public void CambiarEstadoBoton(Button btn, Image img)
    {
        FueSeleccionado = !FueSeleccionado;
        //CambiarColorBoton(btn,img);
    }

    void CambiarColorBoton(Button btn, Image img)
    {
        ColorBlock colorBlock = btn.colors;
        
       /* if (FUESELECCIONADO)
        {
            //colorBlock.selectedColor = ColorSeleccionado;
            //colorBlock.normalColor = ColorSeleccionado;//cyan
            //colorBlock.pressedColor = ColorSeleccionado;
            //colorBlock.highlightedColor = ColorSeleccionado;
            //btn.colors = colorBlock;
            Debug.Log($"Color cambiado en true a: {btn.colors}");
        }
        else
        {
            colorBlock.normalColor = ColorNormal;//verde
            //colorBlock.selectedColor = ColorNormal;//rojo
            //colorBlock.pressedColor = ColorNormal;//gris
            //colorBlock.highlightedColor = ColorNormal; // negro
            Debug.Log($"Color cambiado en falso  a: {btn.colors}");
        }
        btn.colors = colorBlock;
       */
    }

    public bool FUESELECCIONADO
    {
        get { return FueSeleccionado; }
        set {  FueSeleccionado = value;}
    }
}
