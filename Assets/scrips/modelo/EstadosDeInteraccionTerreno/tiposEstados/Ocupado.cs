
using UnityEngine;


public class Ocupado: IEstadoHexEstrategia
{
    public void ActivarEstado(Terreno terreno)
    {
        if(terreno.GetPersonaje() != null)
        {
            terreno.CambiarEstadoTerrenosLimitrofes(new Visible());
        }
    }
    public void DesactivarEstado(Terreno terreno)
    {
        terreno.EliminarEntidad(terreno.GetPersonaje());
    }
}

