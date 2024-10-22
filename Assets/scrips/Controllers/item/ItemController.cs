using Assets.scrips.interfaces.efecto;
using Assets.scrips.interfaces.interactuable;
using Assets.scrips.modelo.interactuables.item.estrategias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scrips.Controllers.item
{
    public class ItemController: Singleton<ItemController>
    {
        List<Item> Items = new List<Item>();
        List<IEfectoItem> EfectosItem = new List<IEfectoItem> { new IncrementoDefensaAtaque(), new IncrementoEnergiaActual(), new IncrementoVidaActual() };
        HashSet<string> NombresItemsSeleccionados = new HashSet<string>();

        public List<Item> ITEMS
        {
            get { return Items; }
            set { Items = value; }
        }

        public List<IEfectoItem> EFECTOSITEM
        {
            get { return EfectosItem; }
            set { EfectosItem = value; }
        }
        public HashSet<string> NOMBREITEMSELECCIONADOS
        {
            get { return NombresItemsSeleccionados; }
            set { NombresItemsSeleccionados = value; }
        }


        public bool CrearItem(string nombre, IEfectoItem efecto, string descripcion)
        {
            try
            {
                Item item = new Item(nombre, efecto, descripcion);
                if (item != null)
                    Items.Add(item);
                return true;

            }
            catch (Exception ex)
            {
                Debug.LogError($"Error al crear el item: {ex.Message}");
                return false;
            }
        }

        public bool EditarItem(Item item, string nombre, IEfectoItem efecto, string descripcion)
        {
            try
            {
                item.NOMBRE = nombre;
                item.EFECTO = efecto;
                item.DESCRIPCION = descripcion;
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error al editar el item: {e.Message}");
                return false;
            }
        }

        public bool EliminarItem(Item item)
        {
            try
            {
                if (Items.Contains(item))
                {
                    if(item.TERRENOACTUAL != null) 
                        item.TERRENOACTUAL.EliminarInteractuable(item);
                    Items.Remove(item);
                    return true;   
                }
                else
                {
                    Debug.Log("No se encuentra el item");
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error al eliminar el item: {e.Message}");
                return false;
            }
        }

        public IInteractuable ObtenerItemAleatorio()
        {
            if(Items.Count > 0)
            {
                var numRandom = Utilidades.GenerarNumeroAleatorio(0, Items.Count);
                if(numRandom != null)
                {
                    return Items[numRandom];
                }
                else
                {
                    Debug.Log("el numero random es  null");
                    return null;
                }
            }
            else
            {
                Debug.Log("no hay items ");
                return null;
            }
        }
    }
}
