using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GeneradorMapa : MonoBehaviour
{
    [SerializeField] private Mapa Mapa;
    [SerializeField] private int Ancho = 256;
    [SerializeField] private int Alto = 256;

    [Tooltip(
        "La escala del mapa de ruido. Cuanto mayor sea la escala, más ampliado estará el mapa de ruido." +
        "Cuanto más baja sea la escala, más se alejará el mapa de ruido" +
        "La escala debe ser mayor que 0."
        )]
    [SerializeField]  private float EscalaNoise = 0.5f;

    [Tooltip(
      "El número de capas de ruido a generar. Más capas significan más detalles en el mapa de ruido."
        )]
    [SerializeField] private int Octavas = 6;
    [Range(0, 1)]

    [Tooltip(
        "El cambio de amplitud entre octavas.La amplitud de cada octava se multiplica por este valor"
        )]
    [SerializeField] private float Persistencia = 0.5f;

    [Tooltip(
        "El cambio de frecuencia entre octavas. La frecuencia de cada octava se multiplica por este valor"
        )]
    [SerializeField] private float Lacunaridad = 2f;

    [Tooltip("La semilla utilizada para generar el mapa de ruido")]
    [SerializeField] private int Seed = 0;

    [Tooltip("El desplazamiento del mapa de ruido")]
    [SerializeField] private Vector2 Offset = Vector2.zero; 

    [Tooltip("Si se actualiza o no automáticamente el mapa de ruido cuando se cambia un valor.")]
    [SerializeField] private bool AutoUpdate = true;

    [Tooltip("Si se debe utilizar o no la información de ancho y alto de la cuadrícula hexagonal para generar el mapa de ruido.")]
    [SerializeField] private bool UsarHexGrid = true;

    [Tooltip("Si se genera o no el mapa de ruido al inicio.")]
    [SerializeField] private bool GenerarMapaAlIniciar = true;
    [SerializeField] private bool GeneracionEnHilos = true; 

    [SerializeField] private List<AlturaDelTerreno> Biomas = new List<AlturaDelTerreno>();
    


    private float[,] MapaNoise {  get; set; }
    private TipoDeSubTerreno[,] MapaDelTerreno { get; set; }
    private Color[] ColorMap { get; set; }

    //EVENTOS

    public event Action<float[,]> AlGenerarseMapaDeRuido;
    public event Action<TipoDeSubTerreno[,]> AlGenerarseMapaDeTerreno;
    public event Action<Color[], int, int> AlGenerarseColorMapa;



    private void Awake()
    {
        Mapa = GetComponent<Mapa>();
    }

    private void Start()
    {
        if (GenerarMapaAlIniciar)
        {
            GenerarMapa();
        }
    }


    public void GenerarMapa()
    {
        if (UsarHexGrid && (Mapa != null))
        {
            Ancho = Mapa.ANCHO;
            Alto = Mapa.ALTO;
        }

        ValidarConfiguracion();

        StartCoroutine(GenerarMapaCorrutina());
    }

    private IEnumerator GenerarMapaCorrutina()
    {
        MapaNoise = null;
        MapaDelTerreno = null;
        ColorMap = null;

        if(Application.isPlaying && GeneracionEnHilos)
        {
            Task task = Task.Run(() =>
            {
                MapaNoise = Noise.GenerarNoiseMapa(Ancho, Alto, EscalaNoise, Seed, Octavas, Persistencia, Lacunaridad, Offset);
                MapaDelTerreno = AsignarTipoDeTerreno(MapaNoise);
                ColorMap = GenerarColoresDeTerreno(MapaDelTerreno);

            }).ContinueWith(task =>
            {
                if(task.Exception != null)
                {
                    Debug.LogError(task.Exception);
                }
            });

            while (!task.IsCompleted)
            {
                yield return null;
            }
        }
        else
        {
            MapaNoise = Noise.GenerarNoiseMapa(Ancho,Alto, EscalaNoise,Seed, Octavas,Persistencia, Lacunaridad, Offset);
            MapaDelTerreno = AsignarTipoDeTerreno(MapaNoise);
            ColorMap = GenerarColoresDeTerreno(MapaDelTerreno);
        }


        AlGenerarseMapaDeRuido?.Invoke(MapaNoise);
        AlGenerarseColorMapa?.Invoke(ColorMap, Ancho, Alto);
        AlGenerarseMapaDeTerreno?.Invoke(MapaDelTerreno);
        
        yield return null;
    }


    private void ValidarConfiguracion()
    {
        Octavas = Mathf.Max(Octavas, 0);
        Lacunaridad = Mathf.Max(Lacunaridad , 1);
        Persistencia = Mathf.Clamp01(Persistencia);
        EscalaNoise = Mathf.Max(EscalaNoise, 0.0001f);

        Ancho = Mathf.Max(Ancho, 1);
        Alto =  Mathf.Max(Alto, 1);
    }


    private TipoDeSubTerreno[,] AsignarTipoDeTerreno(float[,] mapaNoise)
    {
        TipoDeSubTerreno[,] mapaDeTerreno = new TipoDeSubTerreno[Ancho, Alto];

        for (int y = 0; y < Alto; y++)
        {
            for(int x = 0; x < Ancho; x++)
            {
                float AlturaActual = mapaNoise[x,y];


                for (int i = 0; i < Biomas.Count; i++)
                {
                    if (AlturaActual <= Biomas[i].altura)
                    {
                        mapaDeTerreno[x,y] = Biomas[i].tipoDeSubTerreno;
                        break;
                    }
                }

                if(AlturaActual > Biomas.Last().altura)
                {
                    mapaDeTerreno[x, y] = Biomas.Last().tipoDeSubTerreno;                 
                }

            }
        }

        return mapaDeTerreno;
    }

    private Color[] GenerarColoresDeTerreno(TipoDeSubTerreno[,] mapaTerrenos)
    {
        Color[] ColorMap = new Color[Ancho * Alto];

        for(int y = 0; y < Alto; y++)
        {
            for(int x = 0; x < Ancho; x++)
            {
                ColorMap[y * Ancho + x] = mapaTerrenos[x, y].COLOR;
            }
        }
        return ColorMap;
    }

    private void OnValidate()
    {
        ValidarConfiguracion();
    }

}

[System.Serializable]
public struct AlturaDelTerreno 
{
    public float altura;
    public TipoDeSubTerreno tipoDeSubTerreno;
}

