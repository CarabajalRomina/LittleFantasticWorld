using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.juego;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Mapa : MonoBehaviour
{
    [SerializeField] private OrientacionHex Orientacion;
    [SerializeField] private int Alto;
    [SerializeField] private int Ancho;
    [SerializeField] private float MedidaHex;
    [SerializeField] private int MedidaDelLote;
    [SerializeField] private List<Terreno> Terrenos = new List<Terreno>();
    private GeneradorMapa GeneradorMapas;
    
    private Task<List<Terreno>> GenerarTerrenosTask;

    private Vector3 OrigenGrid;

    [SerializeField] private List<Vector2> CeldasVisiblesDefault = new List<Vector2>();
    [SerializeField] private int RangoCeldasVisiblesDefault = 1;

    //informacion de carga del juego- EVENTOS
    public event System.Action InfoMapaGenerada;
    public event System.Action<float> CeldaLoteGenerado;
    public event System.Action CeldasInstanciasGeneradas;

    JuegoController CntJuego;
    #region PROPIEDADES

    public OrientacionHex ORIENTACION
    {
        get { return Orientacion; }
        set
        {
            if (value == OrientacionHex.FlatTop || value == OrientacionHex.PointyTop)
            {
                Orientacion = value;
            }
        }
    }
    public int ALTO
    {
        get { return Alto; }
        set
        {
            if (value > 0)
            {
                Alto = value;
            }
            else
            {
                Alto = 2;
            }
        }
    }
    public int ANCHO
    {
        get { return Ancho; }
        set
        {
            if (value > 0)
            {
                Ancho = value;
            }
            else
            {
                Ancho = 2;
            }
        }
    }
    public float MEDIDAHEX
    {
        get { return MedidaHex; }
        set
        {
            if (value > 0)
            {
                MedidaHex = value;
            }
            else
            {
                MedidaHex = 1;
            }
        }
    }
    public int MEDIDADELLOTE
    {
        get { return MedidaDelLote; }
        set
        {
            if (value > 0)
            {
                MedidaDelLote = value;
            }
            else
            {
                MedidaDelLote = 1;
            }
        }
    }
    public List<Terreno> TERRENOS
    {
        get { return Terrenos; }
        set { Terrenos = value; }
    }
    public List<Vector2> CELDASVISIBLESDEFAULT
    {
        get { return CeldasVisiblesDefault; }
        set { CeldasVisiblesDefault = value; }
    }
    public int RANGOCELDASVISIBLESDEFAULT
    {
        get { return RangoCeldasVisiblesDefault; }
        set { RangoCeldasVisiblesDefault = value; }
    }

    public Vector3 ORIGENGRID
    {
        get { return OrigenGrid; }
    }
    #endregion

    private void Awake()
    {
        OrigenGrid = transform.position;
        CntJuego = JuegoController.Instancia;
        GeneradorMapas = FindObjectOfType<GeneradorMapa>();
    }

    private void OnEnable()
    {
        CeldasInstanciasGeneradas += PonerCeldasDefaultYLimitrofesVisibles;
        CeldaLoteGenerado += ProcesoDeCargaMapa;
        if (GeneradorMapas != null)
        {
            GeneradorMapas.AlGenerarseMapaDeTerreno += GenerarTerrenos;
        }
    }

    private void OnDisable()
    {
        if (GeneradorMapas != null)
        {
            GeneradorMapas.AlGenerarseMapaDeTerreno -= GenerarTerrenos;
        }
        if (GenerarTerrenosTask != null && GenerarTerrenosTask.Status == TaskStatus.Running)
        {
            GenerarTerrenosTask.Dispose();
        }
        CeldasInstanciasGeneradas -= PonerCeldasDefaultYLimitrofesVisibles;
        CeldaLoteGenerado -= ProcesoDeCargaMapa;
    }

    private void ProcesoDeCargaMapa( float progreso)
    {
        Debug.Log("Progreso de carga: " + Mathf.Round( progreso * 100) +'%');
    }
  
    private void GenerarTerrenos(TipoDeSubTerreno[,] mapaDeTerrenos)
    {
        BorrarTerrenos();
        GenerarTerrenosTask = Task.Run(() => GenerarDatosTerrenos(mapaDeTerrenos));
        GenerarTerrenosTask.ContinueWith(task =>
        {
            TERRENOS = task.Result;
            DespachadorHiloPrincipal.Instancia.Enqueue(() => StartCoroutine(InstanciarTerrenos(TERRENOS)));
        
        });
    }

   private void CargarTerrenosDefaultConObjetos()
    {
        foreach (var coordenada in CeldasVisiblesDefault)
        {
            var terreno = BuscarTerrenoPorCoordenadasOffset(coordenada);
            if(terreno != null)
            {
                DespachadorHiloPrincipal.Instancia.Enqueue(() => CargarTerrenosLimitrofesConObjetos(terreno));
                DespachadorHiloPrincipal.Instancia.Enqueue(() => InstanciarPersonajeEnMapa(terreno));
                

            }
        }
    }

    private void BorrarTerrenos()
    {
        for (int i = 0; i < TERRENOS.Count; i++)
        {
            TERRENOS[i].BorrarTerreno();
        }
        TERRENOS.Clear();
    }

    private List<Terreno> GenerarDatosTerrenos(TipoDeSubTerreno[,] mapaDeTerrenos)
   {
        List<Terreno> terrenosList = new List<Terreno>();

        for (int fila = 0; fila < Alto; fila++)
        {
            for (int col = 0; col < Ancho; col++)
            {
                int flippedX = ANCHO - col - 1;
                int flippedY = Alto - fila - 1;

                Terreno terreno = new Terreno();
                terreno.GRILLAHEX = this;
                terreno.SetCoordenadas(new Vector2(col,fila), ORIENTACION, MEDIDAHEX);
                terreno.TIPOSUBTERRENO = mapaDeTerrenos[flippedX, flippedY];
                //terreno.InicializarEstado(new Oculto());

                terrenosList.Add(terreno);
            }
        }
        AsignarTerrenosLimitrofes(terrenosList);
        return terrenosList;
    }

    private IEnumerator InstanciarTerrenos(List<Terreno> terrenos)
    {
        int CantLotes = 0;
        int LotesTotales = Mathf.CeilToInt(terrenos.Count / MEDIDADELLOTE);

        for (int i = 0; i < TERRENOS.Count; i++)
        {
            TERRENOS[i].CrearInstanciaTerrenoPrefab(TERRENOS[i].COORDENADASOFFSET);
            if (i % MEDIDADELLOTE == 0 && i != 0)
            {
                CantLotes++;
                CeldaLoteGenerado?.Invoke((float)CantLotes / LotesTotales);
                yield return null;
            }
        }
        CeldasInstanciasGeneradas?.Invoke();
        InfoMapaGenerada?.Invoke(); 
        Debug.Log("--------------------------------TERMINO DE CARGAR EL MAPA----------------------------------------");
        CargarTerrenosDefaultConObjetos();
    }

    private void PonerCeldasDefaultYLimitrofesVisibles()
    {
        Queue<Terreno> terrenoQueue = new Queue<Terreno>();
        HacerVisibleCeldasDefault(terrenoQueue);

        int rangoActual = 0;

        while(terrenoQueue.Count > 0 && rangoActual < RangoCeldasVisiblesDefault)
        {
            for(int i = 0;i < terrenoQueue.Count; i++)
            {
                (terrenoQueue.Dequeue()).CambiarEstadoTerrenosLimitrofes(new Visible());
            }
            rangoActual++;
        }
    }

    private void HacerVisibleCeldasDefault(Queue<Terreno> terrenoQueue)
    {
        foreach (Vector2 coordenadas in CeldasVisiblesDefault)
        {
            Terreno terreno = Terrenos.Find(c => c.COORDENADASOFFSET == coordenadas);
            if (terreno != null)
            {
                terreno.CambiarEstado(new Visible());
                terrenoQueue.Enqueue(terreno);
            }
        }
    }

    public void AsignarTerrenosLimitrofes(List<Terreno> terrenos)
    {
        foreach (Terreno terreno in terrenos)
        {
            List<Terreno> terrenosLimitrofesActuales = new List<Terreno>();
            Vector2 coordenadasAxialesActuales = terreno.COORDENADASAXIAL;

            List<Vector2> coordenadasAxialesLimitrofes = MetricasHex.ObtenerHexAdyacentes(coordenadasAxialesActuales);

            foreach (Vector2 coordenadaAxial in coordenadasAxialesLimitrofes)
            {
                Terreno terrenoActual = terrenos.Find(c => c.COORDENADASAXIAL == coordenadaAxial);

                if (terrenoActual != null)
                {
                    terrenosLimitrofesActuales.Add(terrenoActual);
                }
            }
            terreno.TERRENOSLIMITROFES = terrenosLimitrofesActuales;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (int z = 0; z < Alto; z++)
        {
            for (int x = 0; x < Ancho; x++)
            {
                Vector3 puntoCentral = MetricasHex.PuntoCentralHex(MedidaHex, x, z, Orientacion) + transform.position;
                for (int s = 0 ; s < MetricasHex.EsquinasHex(MedidaHex,Orientacion).Length; s++)
                {
                    Gizmos.DrawLine(
                        puntoCentral + MetricasHex.EsquinasHex(MedidaHex, Orientacion)[s % 6],
                        puntoCentral + MetricasHex.EsquinasHex(MedidaHex, Orientacion)[(s + 1) % 6]
                        );
                }
            }
        }
    }

    public Terreno BuscarTerrenoPorPosicionTridimencional(Vector3 posicion)
    {
        return Terrenos.Find(c => c.POSICIONTRIDIMENSIONAL == posicion);
    }

    public Terreno BuscarTerrenoPorCoordenadasOffset(Vector2 coordenadas)
    {
        return Terrenos.Find(c => c.COORDENADASOFFSET == coordenadas);
    }

    public Terreno BuscarTerrenoPorCoordenadasAxial(Vector2 coordenadas)
    {
        return Terrenos.Find(c => c.COORDENADASAXIAL == coordenadas);
    }

    public List<Terreno> TerrenosNoHabitables()
    {
        return Terrenos.Where(t => !(t.TIPOSUBTERRENO.TIPOTERRENO is Especial)).ToList();
    }

    private void CargarTerrenosLimitrofesConObjetos(Terreno terreno)
    {
        CntJuego.CargarConObjTerrenosLimitrofes(terreno);
    }

    private void InstanciarPersonajeEnMapa(Terreno terreno)
    {
        CntJuego.InstanciarPersonajeSeleccionadoEnTerreno(terreno);
    }

}

public enum OrientacionHex
{
    FlatTop,
    PointyTop
}

