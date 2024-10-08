using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

