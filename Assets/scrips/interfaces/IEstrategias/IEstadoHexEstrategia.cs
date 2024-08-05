using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEstadoHexEstrategia 
{

    void ActivarEstado(Transform terreno);
    void DesactivarEstado(Transform terreno);

    IEstadoHexEstrategia OnMouseEnter();
    IEstadoHexEstrategia OnMouseExit();
    IEstadoHexEstrategia Seleccionar();
    IEstadoHexEstrategia Deseleccionar();
}
