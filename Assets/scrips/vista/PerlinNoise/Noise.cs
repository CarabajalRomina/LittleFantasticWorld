using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public static class Noise 
{
   public static float[,] GenerarNoiseMapa(int anchoMapa, int altoMapa, float escala, int seed,
       int octavas, float persistencia, float lacunaridad, Vector2 coordOffset)
    {
        System.Random NumSeed = new System.Random(seed);

        Vector2[] octavaOffsets = new Vector2[octavas];

        for (int i = 0; i < octavas; i++)
        {
            float offsetX = NumSeed.Next(-100000, 100000) + coordOffset.x;
            float offsetY = NumSeed.Next(-100000, 100000) + coordOffset.y;

            octavaOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if(escala <= 0)
        {
            escala = 0.0001f;
        }

        float AltoMaxNoise = float.MinValue;
        float AltoMinNoise = float.MaxValue;

        float MedioDelAncho = anchoMapa / 2f;
        float MedioDelAlto = altoMapa / 2f;

        float[,] noiseMapa = new float[anchoMapa, altoMapa];

        for(int y = 0; y < altoMapa; y++)
        {
            for(int x = 0; x < anchoMapa; x++)
            {
                float Amplitud = 1;
                float Frecuencia = 1;
                float AltoNoise = 0;

                for( int i = 0; i < octavas; i++)
                {

                    // se multiplica las coordenadas x e y por la escala para hacer el ruido mas pronunciado 
                    // cuanto mayor sea la escala mayor mas ampliado sera el ruido, tambien multiplicamos la frecuencia 
                    //para hacer que el ruido escale con cada octava (cuanto mas alta la frecuencia mas se alejara el ruido)
                    // y las coordenadas offset son para que el ruido sea diferente para cada octava
                    float MuestraEnX = (x - MedioDelAncho) / escala * Frecuencia + octavaOffsets[i].x;
                    float MuestraEnY = (y - MedioDelAlto) / escala * Frecuencia + octavaOffsets[i].y;

                    // El valor perlin noise devuelve un valor entre 0 y 1 por lo que lo multiplicamos
                    //por 2 y restamos 1 para obtener un valor entre -1 y 1
                    float ValorPerlin = Mathf.PerlinNoise(MuestraEnX, MuestraEnY) * 2 - 1;

                    // Suma el valor de perlin a la altura del ruido para esta octava(la amplitud se multiplica por p
                    //valor perlin para controlar qué tan pronunciada es cada octava)
                    AltoNoise += ValorPerlin * Amplitud;

                    // Multiplicamos la amplitud por la persistencia para que cada octava sea menos pronunciada que la última
                    Amplitud *= persistencia;

                    // Multiplicamos la frecuencia por la lacunaridad para hacer que cada octava esté más alejada que la anterior
                    Frecuencia *= lacunaridad;
                
                }

                if (AltoNoise > AltoMaxNoise)
                    AltoMaxNoise = AltoNoise;
                if (AltoNoise < AltoMinNoise)
                    AltoMinNoise = AltoNoise;

                noiseMapa[x, y] = Mathf.InverseLerp(AltoMinNoise, AltoMaxNoise, AltoNoise);
            }
           
        }

        return noiseMapa; 

    }
}
