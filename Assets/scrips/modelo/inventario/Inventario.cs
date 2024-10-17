using Assets.scrips.interfaces.interactuable;
using Assets.scrips.modelo.configuraciones;
using System.Collections.Generic;

namespace Assets.scrips.modelo.inventario
{
    public class Inventario
    {
        List<IInteractuable> Intereactuables;

        public List<IInteractuable> INTERACTUABLES
        {
            get { return Intereactuables; }
            set { Intereactuables = value; }
        }

        public Inventario()
        {
            INTERACTUABLES = new List<IInteractuable>();
        }

        public void AgregarInteractuable(IInteractuable interactuable)
        {
            if(INTERACTUABLES.Count <= ConfiguracionGeneral.CantMaxInteractuablesInventario)
            {
                Intereactuables.Add(interactuable);
            }
            //avisar que no puede recoger mas items
        }

        public void EliminarInteractuable(IInteractuable interactuable)
        {
            if(INTERACTUABLES.Contains(interactuable))
            {
                Intereactuables.Remove(interactuable);
            }
            else
            {
                //avisar que no contiene ese interactuable osea no se puede borrar porque no existe
            }
        }
    }
}
