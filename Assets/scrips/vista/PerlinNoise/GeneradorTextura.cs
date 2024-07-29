using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneradorTextura
{
    public static Texture2D TexturaDesdeMapaDeColores(Color[] mapaDeColores, int ancho, int alto)
    {
        Texture2D textura = new Texture2D(ancho,alto);

        textura.filterMode = FilterMode.Point;
        textura.wrapMode = TextureWrapMode.Clamp;

        textura.SetPixels(mapaDeColores);

        textura.Apply();

        return textura;
    }

    public static Texture2D TexturaDesdeMapaDeAltura(float[,] mapaDeAltura)
    {
        int ancho = mapaDeAltura.GetLength(0);
        int alto = mapaDeAltura.GetLength(1);

        Color[] mapaColores = new Color[ancho * alto];

        for(int y = 0; y < alto; y++)
        {
            for(int x = 0; x < ancho; x++)
            {
                mapaColores[y * ancho + x] = Color.Lerp(Color.black, Color.white, mapaDeAltura[x, y]);
            }
        }
        return TexturaDesdeMapaDeColores(mapaColores, ancho, alto);
    }
}
