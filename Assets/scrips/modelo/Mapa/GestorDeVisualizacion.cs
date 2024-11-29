using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorDeVisualizacion : MonoBehaviour
{
    
    public enum ModoRepresentacion
    {
        MapaNoise,
        MapaDeColor
    }

    public Renderer rendererTextura;
    public GeneradorMapa generadorMapa;
    public ModoRepresentacion modoRepresentacion;



    private void Awake()
    {
        if (generadorMapa == null)
        {
            generadorMapa = FindObjectOfType<GeneradorMapa>();
        }
    }

    private void OnEnable()
    {
        SubscribirseALosEventos();
    }

    private void OnDisable()
    {
        DesuscribirseALosEventos();
    }

    private void GenerarTexturaYDibujar(Color[] mapaColores, int ancho, int alto)
    {
        if (modoRepresentacion != ModoRepresentacion.MapaDeColor) { return; }
        Texture2D textura = GeneradorTextura.TexturaDesdeMapaDeColores(mapaColores, ancho, alto);
        DibujarTextura(textura);
    }

    private void GenerarTexturaYDibujar(float[,] mapaNoise)
    {
        if (modoRepresentacion != ModoRepresentacion.MapaNoise) { return; }
        Texture2D textura = GeneradorTextura.TexturaDesdeMapaDeAltura(mapaNoise);
        DibujarTextura(textura);
    }

    public void DibujarTextura(Texture2D textura)
    {
        int ancho = textura.width;
        int alto = textura.height;

        rendererTextura.sharedMaterial.mainTexture = textura;
        rendererTextura.transform.localScale = new Vector3(ancho, 1, alto);
    }

    public void SubscribirseALosEventos()
    {
        DesuscribirseALosEventos();

        if (generadorMapa == null)
        {
            generadorMapa = FindObjectOfType<GeneradorMapa>();
            if (generadorMapa == null)
            {
                throw new System.Exception("no se encontro generador de juego");
            }
        }
        generadorMapa.AlGenerarseColorMapa += GenerarTexturaYDibujar;
        generadorMapa.AlGenerarseMapaDeRuido += GenerarTexturaYDibujar;
    }

    public void DesuscribirseALosEventos()
    {
        generadorMapa.AlGenerarseColorMapa -= GenerarTexturaYDibujar;
        generadorMapa.AlGenerarseMapaDeRuido -= GenerarTexturaYDibujar;
    }
}


