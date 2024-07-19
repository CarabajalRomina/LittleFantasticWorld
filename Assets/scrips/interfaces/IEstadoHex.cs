using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEstadoHex 
{

    void ActivarEstado(Transform terreno);
    void DesactivarEstado(Transform terreno);

    IEstadoHex OnMouseEnter();
    IEstadoHex OnMouseExit();
    IEstadoHex Seleccionar();
    IEstadoHex Deseleccionar();
}
