using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;
using UnityEngine;

public class ControllerTipoSubTerreno
{
    private static ControllerTipoSubTerreno instancia;
    private List<TipoDeSubTerreno> TiposSubTerrenos;

    private ControllerTipoSubTerreno()
    {
        TiposSubTerrenos = GetTiposSubTerrenos();
    }

    public static ControllerTipoSubTerreno Instancia()
    {
        if (instancia == null)
        {
            instancia = new ControllerTipoSubTerreno();
        }
        return instancia;
    }

    public List<TipoDeSubTerreno> GetTiposSubTerrenos()
    {
        var tiposSub = Resources.LoadAll<TipoDeSubTerreno>("TiposSubTerrenos");
        return new List<TipoDeSubTerreno>(tiposSub);
    }

    public TipoDeSubTerreno GetNiebla()
    {
        var niebla = Resources.Load<TipoDeSubTerreno>("TiposSubTerrenos/Niebla");
        return niebla;
    }

    public List<TipoDeSubTerreno> TIPOSUBTERRENOS
    {
        get { return TiposSubTerrenos; }
    }


}
