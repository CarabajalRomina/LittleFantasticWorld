﻿using Assets.scrips.modelo.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.interfaces.fabricas.entidad
{
    public interface IFabricaEntidad
    {
        Entidad CrearEntidad();
    }
}