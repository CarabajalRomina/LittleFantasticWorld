using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scrips.Controllers.entidad
{
    public class EntidadController : Singleton<EntidadController>
    {
        List<Entidad> EntidadList;
        List<IDieta> Dietas;
        List<IHabitat> Habitats;
        List<IReino> Reinos;

    }
}
