using Assets.scrips.interfaces;
using Assets.scrips.modelo.decoradores.habitat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AereoDecorador : HabitatDecorador
{
    public AereoDecorador(IHabitat habitat) : base(habitat){}

    public override bool PuedoMoverme(MonoBehaviour tipoTerreno)
    {
        return base.PuedoMoverme(tipoTerreno) || tipoTerreno is Aire;
    }

    public override string ToString()
    {
        return base.ToString() + " - Aereo";
    }
}
