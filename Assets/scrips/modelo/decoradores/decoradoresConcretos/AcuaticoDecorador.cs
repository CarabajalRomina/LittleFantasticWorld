using Assets.scrips.interfaces;
using Assets.scrips.modelo.decoradores.habitat;
using Assets.scrips.modelo.Habitats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcuaticoDecorador : HabitatDecorador
{
    public AcuaticoDecorador(IHabitat habitat) : base(habitat){}

    public override bool PuedoMoverme(ITipoTerreno tipoTerreno)
    {
        return Habitat.PuedoMoverme(tipoTerreno) || tipoTerreno is Agua;
    }

    public override string ToString()
    {
        return base.ToString() + " - Acuatico";
    }
}
