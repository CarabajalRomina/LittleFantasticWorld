using Assets.scrips.Controllers.comida;
using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.item;
using Assets.scrips.fabricas.dietas;
using Assets.scrips.fabricas.entidades.enemigos;
using Assets.scrips.fabricas.entidades.personajes;
using Assets.scrips.fabricas.habitats;
using Assets.scrips.fabricas.reinos;
using Assets.scrips.interfaces.efecto;
using Assets.scrips.modelo.dietas;
using Assets.scrips.modelo.interactuables.item.estrategias;
using Assets.scrips.modelo.interactuables.item.estrategias.energiaActual;
using Assets.scrips.modelo.interactuables.item.estrategias.puntosAtaque;
using UnityEngine;

namespace Assets.scrips.modelo.preCargaDeObjetos
{
    public class PreCargaObjetos:MonoBehaviour
    {
        EntidadController CntEntidad;
        ComidaController CntComida = ComidaController.GetInstancia;
        ItemController CntItem = ItemController.GetInstancia;

        private void Start()
        {
            CntEntidad = EntidadController.Instancia;
            CargarListEntidades();
            CargarListComidas();
            CargarListItems();
        }

        private void CargarListEntidades()
        {
            var elfa = new FabricaPersonaje("Elfina", new FabricaMitologico().CrearReino(), new FabricaOmnivoro().CrearDieta(), new FabricaTerrestre().CrearHabitat(), 100, 100, 20, 15, 1).CrearEntidad();
            CntEntidad.ENTIDADES.Add((elfa));
            if (CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/Elfa") !=  null)
            {
                elfa.PERSONAJEPREFAB = CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/Elfa");

            }

            var elBoss = new FabricaPersonaje("Aldor", new FabricaHumano().CrearReino(), new FabricaOmnivoro().CrearDieta(), new FabricaTerrestre().CrearHabitat(), 100, 100, 20, 15, 1).CrearEntidad();
            CntEntidad.ENTIDADES.Add(elBoss);
            if (CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/The Boss") != null)
            {
                elBoss.PERSONAJEPREFAB = CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/The Boss");

            }

            var hugo = new FabricaPersonaje("Hugo el mutante", new FabricaDemoniaco().CrearReino(), new FabricaCarnivoro().CrearDieta(), new FabricaTerrestre().CrearHabitat(), 100, 100, 20, 15, 1).CrearEntidad();
            CntEntidad.ENTIDADES.Add(hugo);
            if (CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/Hugo") != null)
            {
                hugo.PERSONAJEPREFAB = CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/Hugo");

            }


            var demonio = new FabricaEnemigo("Demonio de fuego", new FabricaDemoniaco().CrearReino(), new FabricaTerrestre().CrearHabitat(), 60, 25, 5).CrearEntidad();
            CntEntidad.ENTIDADES.Add(demonio);
            if (CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/LoboMutadoRosado"))
            {
                demonio.PERSONAJEPREFAB = CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/LoboMutadoRosado");
            }

            var golemLodo = new FabricaEnemigo("Golem de Lodo", new FabricaMitologico().CrearReino(), new FabricaTerrestre().CrearHabitat(), 100, 20, 10).CrearEntidad();
            CntEntidad.ENTIDADES.Add(golemLodo);
            if (CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/GolemLodo"))
            {
                golemLodo.PERSONAJEPREFAB = CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/GolemLodo");
            }

            var zombie = new FabricaEnemigo("Rick el Zombie", new FabricaDemoniaco().CrearReino(), new FabricaTerrestre().CrearHabitat(), 70, 10, 12).CrearEntidad();
            CntEntidad.ENTIDADES.Add(zombie);
            if (CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/ZombieCartoonRojo"))
            {
                zombie.PERSONAJEPREFAB = CntEntidad.BuscarPrefabPersonaje("Prefabs/PersonajesPref/ZombieCartoonRojo");
            }
        }

        public void CargarListItems()
        {
            CntItem.ITEMS.Add(new Item("Poción de Curación", new IncrementoVidaActual(), "Restaura 20 puntos de vida al instante"));
            CntItem.ITEMS.Add(new Item("Espada del Guerrero", new IncrementoPuntosAtaque(), " Aumenta tu ataque en 15 puntos durante la próxima batalla."));
            CntItem.ITEMS.Add(new Item("Escudo de Hierro", new IncrementoDefensaAtaque(), "Aumenta tu defensa en 10 puntos temporalmente."));
            CntItem.ITEMS.Add(new Item("Poción de Energía", new AumentoTotalEnergiaActual(), " Restaura 30 puntos de energía, suficiente para realizar 3 acciones."));
            CntItem.ITEMS.Add(new Item("Veneno Mortal", new ReduccionVidaActual(), " Reduce la vida del enemigo en 15 puntos"));
        }

        public void CargarListComidas()
        {
            CntComida.COMIDAS.Add(new Comida("Carne asada", 30, new FabricaCarnivoro().CrearDieta()));
            CntComida.COMIDAS.Add(new Comida("Pescado Fresco", 25, new FabricaCarnivoro().CrearDieta()));
            CntComida.COMIDAS.Add(new Comida("Ensalada", 15, new FabricaHerbivoro().CrearDieta()));
            CntComida.COMIDAS.Add(new Comida("Frutos silvestres", 12, new FabricaHerbivoro().CrearDieta()));
            CntComida.COMIDAS.Add(new Comida("Setas", 30, new FabricaHerbivoro().CrearDieta()));
            CntComida.COMIDAS.Add(new Comida("Bateria Energetica", 100, new FabricaEnergiaElectrica().CrearDieta()));
            CntComida.COMIDAS.Add(new Comida("Galleta Energia", 15, new FabricaEnergiaElectrica().CrearDieta()));
            CntComida.COMIDAS.Add(new Comida("Alga verde", 40, new FabricaFotosintetico().CrearDieta()));
            CntComida.COMIDAS.Add(new Comida("Rayo de sol", 10, new FabricaFotosintetico().CrearDieta()));
        }

    }
}
