using Assets.scrips.fabricas.habitats;
using Assets.scrips.fabricas.habitats.fabricaHabitat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.scrips.Controllers.habitat
{
    public class HabitatController: Singleton<HabitatController>
    {
        FabricaHabitat FbTerrestre = new FabricaTerrestre();
        List<IHabitat> Habitats{ get; set; }

        public HabitatController()
        {
            Habitats = new List<IHabitat> { 
                FbTerrestre.CrearHabitat(),
                new FabricaAcuatico().CrearHabitat(),
                new FabricaAereo().CrearHabitat(),
                GetHabitatAcuaticaTerrestre(),
                GetHabitatAereoTerrestre(),
                GetHabitatAereoAcuaticoTerrestre()
            };
        }

        public IHabitat GetHabitatAcuaticaTerrestre()
        {
            return new AcuaticoDecorador(FbTerrestre.CrearHabitat());
        }
        public IHabitat GetHabitatAereoTerrestre()
        {
            return new AereoDecorador(FbTerrestre.CrearHabitat());
        }
        public IHabitat GetHabitatAereoAcuaticoTerrestre()
        {
            return new AereoDecorador(GetHabitatAcuaticaTerrestre());
        }
    }
}
