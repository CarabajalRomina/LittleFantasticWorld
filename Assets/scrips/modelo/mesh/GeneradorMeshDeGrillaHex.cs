using UnityEngine;


[RequireComponent( typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class GeneradorMeshDeGrillaHex : MonoBehaviour
{
    [field: SerializeField] public LayerMask grillaLayer {  get; set; }
    [field: SerializeField] public Mapa  HexGrilla { get; set; }




    private void Awake()
    {
        if(HexGrilla == null) 
            HexGrilla = GetComponentInParent<Mapa>();
        if (HexGrilla == null)
            Debug.LogError("EL generador de la malla de la grilla hexagonal  no encuentra la grilla hexagonal");

    }

    private void OnEnable()
    {
        //ControllerMouseClicks.Instancia.OnIzqMouseClick += OnIzqMouseClick;
        //ControllerMouseClicks.Instancia.OnDerMouseClick += OnDerMouseClick;

    }


    public void CrearMallaHex()
    {
        CrearMallaHex(HexGrilla.ANCHO, HexGrilla.ALTO, HexGrilla.MEDIDAHEX, HexGrilla.ORIENTACION, grillaLayer);
    }

    public void BorrarMallaGrillaHex()
    {
        if (GetComponent<MeshFilter>().sharedMesh != null)
        {
            GetComponent<MeshFilter>().sharedMesh.Clear();
            GetComponent<MeshCollider>().sharedMesh.Clear();
        }
    }

    public void CrearMallaHex(int ancho, int alto, float hexMedida, OrientacionHex orientacion, LayerMask layerMask)
    {
        BorrarMallaGrillaHex();
        Vector3[] vertices = new Vector3[7 * ancho * alto];// 7 hace referencia a los 7 vertices del hexagono


        for(int z = 0; z < ancho; z++)
        {
            for (int x = 0; x < alto; x++)
            {
                Vector3 PuntoCentral = MetricasHex.PuntoCentralHex(hexMedida, x, z, orientacion);
                vertices[GetTrianguloBaseIndex(x, z, ancho)] = PuntoCentral;

                for(int y = 0; y < MetricasHex.EsquinasHex(hexMedida, orientacion).Length; y++)
                {
                   vertices[GetTrianguloBaseIndex(x,z,ancho) + y + 1] = PuntoCentral + MetricasHex.EsquinasHex(hexMedida, orientacion)[y % 6];
                }
            }
        }

        int[] triangulos = new int[3 * 6 * ancho * alto];
        for (int z = 0; z < ancho; z++)
        {
            for (int x = 0; x < alto; x++)
            {
                for (int y = 0; y < MetricasHex.EsquinasHex(hexMedida, orientacion).Length; y++)
                {
                    int esquinaIndex = y + 2 > 6 ? y + 2 -6 : y + 2;
                    int trianguloIndex = 3 * 6 * (z * ancho + x) + y * 3;
                    AsignarIndicesTriangulos(triangulos, trianguloIndex, GetTrianguloBaseIndex(x, z, ancho), esquinaIndex, y);            
                }
            }
        }

        Mesh mesh = new Mesh();
        ConfigurarMesh(mesh, vertices, triangulos);

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;


        int GrillaLayerIndex = GetLayerIndex(layerMask);
        Debug.Log(" Layer index: " + GrillaLayerIndex);

        gameObject.layer = GrillaLayerIndex;

    }
    private int GetTrianguloBaseIndex(int x, int z, int ancho)
    {
        return (z * ancho + x) * 7;
    }

    private void AsignarIndicesTriangulos(int[] triangulos, int trianguloIndex, int verticeBaseIndex, int esquinaIndex, int y)
    {
        triangulos[trianguloIndex] = verticeBaseIndex;
        triangulos[trianguloIndex + 1] = verticeBaseIndex + y + 1;
        triangulos[trianguloIndex + 2] = verticeBaseIndex + esquinaIndex;
    }

    private void ConfigurarMesh( Mesh mesh,Vector3[] vertices, int[] triangulos)
    {
        mesh.name = "Malla Hex";
        mesh.vertices = vertices;
        mesh.triangles = triangulos;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        mesh.RecalculateUVDistributionMetrics();
    }

    private int GetLayerIndex(LayerMask layerMask)
    {
        int ValorLayerMask = layerMask.value;
        Debug.Log("Layer Mask Valor: " + ValorLayerMask);

        for (int i = 0; i < 32; i++)
        {
            if(((1<< 1) & ValorLayerMask) != 0)
            {
                Debug.Log("Valor Index loop: " + i);
                return i;
            }
        }
        return 0;
    }





    #region MOUSECLICK
    private void OnIzqMouseClick(RaycastHit hit)
    {
        Debug.Log("Hit Object, click izq:" + hit.transform.name + "la posicion " + hit.point);
        float localX = hit.point.x - hit.transform.position.x;
        float localZ = hit.point.z - hit.transform.position.z;

        Debug.Log("Offset Posicion:" + MetricasHex.CoordenadaAOffset(localX, localZ, HexGrilla.MEDIDAHEX, HexGrilla.ORIENTACION));
    }

    private void OnDerMouseClick(RaycastHit hit)
    {
        float localX = hit.point.x - hit.transform.position.x;
        float localZ = hit.point.z - hit.transform.position.z;


        Vector2 ubicacion = MetricasHex.CoordenadaAOffset(localX, localZ, HexGrilla.MEDIDAHEX, HexGrilla.ORIENTACION);
        Vector3 centro = MetricasHex.PuntoCentralHex(HexGrilla.MEDIDAHEX, (int)ubicacion.x, (int)ubicacion.y, HexGrilla.ORIENTACION) + HexGrilla.transform.position;

        Debug.Log("derecho clickeado en hex:" + ubicacion);

    }

  

    private void OnDisable()
    {
        //ControllerMouseClicks.Instancia.OnIzqMouseClick -= OnIzqMouseClick;
        //ControllerMouseClicks.Instancia.OnDerMouseClick -= OnDerMouseClick;
    }
    #endregion

}
