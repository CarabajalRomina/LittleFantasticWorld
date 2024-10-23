
using UnityEngine;


public class Ocupado: IEstadoHexEstrategia
{
    public void ActivarEstado(Terreno terreno)
    {
        terreno.CambiarEstadoTerrenosLimitrofes(new Visible());
      
    }
    public void DesactivarEstado(Terreno terreno)
    {
        terreno.EliminarEntidad(terreno.GetPersonaje());
    }
}

