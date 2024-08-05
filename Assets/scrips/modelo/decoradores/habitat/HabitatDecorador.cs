﻿using Assets.scrips.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.modelo.decoradores.habitat
{
    public abstract class HabitatDecorador: IHabitat
    {
        protected IHabitat Habitat { get; set; }

        protected HabitatDecorador(IHabitat habitat)
        {
            Habitat = habitat;
        }

        public virtual bool PuedoMoverme(ITipoTerreno tipoTerreno)
        {
            return Habitat.PuedoMoverme(tipoTerreno);
        }

        public override string ToString()
        {
            return Habitat.ToString();
        }
    }
}