using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Tilemaps.Tilemap;

public static class MetricasHex
{
    public static float RadioExterior(float medidaLado)
    {
        return medidaLado;
    }

    public static float RadioInterno(float medidaLado)
    {
        return medidaLado * 0.866025404f;
    }

    public static Vector3[] EsquinasHex(float medidaLado, OrientacionHex orientacionHex)
    {
        Vector3[] HexEsquinas = new Vector3[6];
        for (int i = 0; i < HexEsquinas.Length; i++)
        {
            HexEsquinas[i] = EsquinaHex(medidaLado, orientacionHex, i);
        }
        return HexEsquinas;
    }

    public static Vector3 EsquinaHex(float medidaLado, OrientacionHex orientacionHex, int index)
    {
        float angulo = 60f * index;
        if (orientacionHex == OrientacionHex.PointyTop)
        {
            angulo += 30f;
        }

        Vector3 esquinaHex = new Vector3(medidaLado * Mathf.Cos(angulo * Mathf.Deg2Rad),
            0f,
            medidaLado * Mathf.Sin(angulo * Mathf.Deg2Rad)
            );
        return esquinaHex;
    }

    public static Vector3 PuntoCentralHex(float medidaLado, int x, int z, OrientacionHex orientacion)
    {
        Vector3 puntoCentral;

        if (orientacion == OrientacionHex.PointyTop)
        {
            puntoCentral.x = (x + z * 0.5f - z / 2) * (RadioInterno(medidaLado) * 2f);
            puntoCentral.y = 0f;
            puntoCentral.z = z * (RadioExterior(medidaLado) * 1.5f);
        }
        else
        {
            puntoCentral.x = (x) * (RadioExterior(medidaLado) * 1.5f);
            puntoCentral.y = 0f;
            puntoCentral.z = (z + x * 0.5f - x / 2) * (RadioInterno(medidaLado) * 2f);
        }
        return puntoCentral;
    }

    // Método para calcular coordenadas cúbicas a partir de coordenadas offset
    public static Vector3 CalcularCoordenadasCubicasAPartirDeCoordOffSet(int x, int z, OrientacionHex orientation)
    {
        int col = x;
        int row = z - (x + (x & 1)) / 2;
        int y = -col - row;

        if (orientation == OrientacionHex.PointyTop)
        {
            int temp = col;
            col = row;
            row = temp;
        }

        return new Vector3(col, y, row);
    }

    //_---------------------------------------------------------------------------

    /// <summary>
    /// Convierte las cooordenadas de cubo a coordenadas de desplazamiento axial
    /// calculando el valor de s a partir de q y r
    /// </summary>
    /// <param name="coorCubo"></param>
    /// <returns></returns>
    public static Vector2 CoordDeCuboACoordAxial(Vector3 coorCubo)
    {
        return new Vector2(coorCubo.x, coorCubo.y);
    }

    public static Vector3 CoordAxialACoordCubo(Vector2 axialCoordinates)
    {
        float x = axialCoordinates.x;
        float z = axialCoordinates.y;
        float y = -x - z;

        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Convierte las coordenadas de desplazamiento a coordenadas axial
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="orientacion"></param>
    /// <returns></returns>
    public static Vector2 CoordOffsetACoordAxial(int x, int z, OrientacionHex orientacion)
    {
        if (orientacion == OrientacionHex.PointyTop)
        {
            return CoordOffSetACoordAxialPointy(x, z);
        }
        else
        {
            return CoordOffSetACoordAxialFlat(x, z);
        }
    }

    /// <summary>
    /// Convierte las coordenadas offset a coordenadas axial para la orientacion en punta
    /// </summary>
    /// <param name="col"></param>
    /// <param name="fila"></param>
    /// <returns></returns>
    public static Vector2 CoordOffSetACoordAxialPointy(int col, int fila)
    {
        /* var q = col - (fila - (fila & 1)) / 2;
         var r = fila;*/
        int q = col;
        int r = fila - (col - (col & 1)) / 2;
        return new Vector2(q, r);
    }

    /// <summary>
    /// Convierte las coordenadas offset a coordenadas axial para la orientacion  plana
    /// </summary>
    /// <param name="col"></param>
    /// <param name="fila"></param>
    /// <returns></returns>

    public static Vector2 CoordOffSetACoordAxialFlat(int col, int fila)
    {
        var q = col;
        var r = fila - (col - (col & 1)) / 2;

        return new Vector2(q, r);
    }

    /// <summary>
    /// Convierte las coordenadas axiales a coordenadas offset
    /// </summary>
    /// <param name="q"></param>
    /// <param name="r"></param>
    /// <param name="orientacion"></param>
    /// <returns></returns>
    public static Vector2 CoordAxialACoordOffset(int q, int r, OrientacionHex orientacion)
    {
        if (orientacion == OrientacionHex.PointyTop)
        {
            return CoordAxialACoordOffsetPointy(q, r);
        }
        else
        {
            return CoordAxialACoordOffsetFlat(q, r);
        }
    }

    /// <summary>
    /// Convierte las coordenadas axiales a coordenadas offset para hexágonos con orientación en punta (Pointy-Top)
    /// </summary>
    /// <param name="q"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    public static Vector2 CoordAxialACoordOffsetPointy(int q, int r)
    {
        int col = q;
        int fila = r + (q - (q & 1)) / 2;
        return new Vector2(col, fila);
    }

    /// <summary>
    /// Convierte las coordenadas axiales a coordenadas offset para hexágonos con orientación plana (Flat-Top)
    /// </summary>
    /// <param name="q"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    public static Vector2 CoordAxialACoordOffsetFlat(int q, int r)
    {
        int col = q + (r - (r & 1)) / 2;
        int fila = r;
        return new Vector2(col, fila);
    }
    public static Vector3 CoordOffsetACoordCubo(int fila, int col, OrientacionHex orientacion)
    {
        if (orientacion == OrientacionHex.PointyTop)
        {
            return CoordOffsetACoordCuboPointy(fila,col, orientacion);
        }
        else
        {
            return CoordOffsetACoordCuboFlat(fila,col,orientacion);
        }
    }

    public static Vector3 CoordOffsetACoordCuboFlat(int fila, int col, OrientacionHex orientacion)
    {
        var x = col;
        var z = fila - (col + (col & 1)) / 2;
        var y = -x - z;
        return new Vector3(x, y, z);
    }

    public static Vector3 CoordOffsetACoordCuboPointy(int fila, int col, OrientacionHex orientacion)
    {
        var x = col - (fila + (fila & 1)) / 2;
        var z = fila;
        var y = -x - z;

        return new Vector3(x,y, z);
    }

    /// <summary>
    /// Convertir coordenadas de cubo a coordenadas offset
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="orientacion"></param>
    /// <returns></returns>
    public static Vector2 CoordCuboACoordOffset(int x, int y, int z, OrientacionHex orientacion)
    {
        if (orientacion == OrientacionHex.PointyTop)
        {
            return CoordCuboACoordOffsetPointy(x, y, z);
        }
        else
        {
            return CoordCuboACoordOffsetFlat(x, y, z);
        }
    }

    public static Vector2 CoordDeCuboACoorOffset(Vector3 coordOffset, OrientacionHex orientacion)
    {
        return CoordCuboACoordOffset((int)coordOffset.x, (int)coordOffset.y, (int)coordOffset.z, orientacion);
    }

    /// <summary>
    /// Convierte coordenadas de cubo a offset segun orientacion en punta
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector2 CoordCuboACoordOffsetPointy(int x, int y, int z)
    {
        Vector2 coordOffset = new Vector2(x + (y - (y & 1)) / 2, y);
        return coordOffset;
    }

    /// <summary>
    /// Convierte coordenadas de cubo a offset segun orientacion plana
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector2 CoordCuboACoordOffsetFlat(int x, int y, int z)
    {
        Vector2 coordOffset = new Vector2(x, y + (x - (x & 1)) / 2);
        return coordOffset;
    }

    /// <summary>
    /// redondea las cordenadas cubo
    /// </summary>
    /// <param name="coordCubo"></param>
    /// <returns></returns>
    private static Vector3 RedondearCoordCubo(Vector3 coordCubo)
    {
        Vector3 CoordRedondeadas = new Vector3();
        int rx = Mathf.RoundToInt(coordCubo.x);
        int ry = Mathf.RoundToInt(coordCubo.y);
        int rz = Mathf.RoundToInt(coordCubo.z);

        float xDiff = Mathf.Abs(rx - coordCubo.x);
        float yDiff = Mathf.Abs(ry - coordCubo.y);
        float zDiff = Mathf.Abs(rz - coordCubo.z);

        if (xDiff > yDiff && xDiff > zDiff)
        {
            rx = -ry - rz;
        }
        else if (yDiff > zDiff)
        {
            ry = -rx - rz;
        }
        else
        {
            rz = -rx - ry;
        }

        CoordRedondeadas.x = rx;
        CoordRedondeadas.y = ry;
        CoordRedondeadas.z = rz;

        return CoordRedondeadas;
    }

    public static Vector2 RedondearCoordAxial(Vector2 coordAxial)
    {
        return CoordDeCuboACoordAxial(RedondearCoordCubo(CoordAxialACoordCubo(coordAxial)));
    }

    public static Vector2 DeCoordenadasACoordAxial(float x, float z, float medidaHex, OrientacionHex orientacion)
    {
        if (orientacion == OrientacionHex.PointyTop)
        {
            return CoordenadaACoordAxialPointy(x, z, medidaHex);
        }
        else
        {
            return CoordenadaACoordAxialFlat(x, z, medidaHex);
        }
    }

    public static Vector2 CoordenadaACoordAxialPointy(float x, float z, float medidaHex)
    {
        Vector2 pointyCoordenadas = new Vector2();
        pointyCoordenadas.x = (Mathf.Sqrt(3) / 3 * x - 1f / 3 * z) / medidaHex;
        pointyCoordenadas.y = (2f / 3 * z) / medidaHex;

        return RedondearCoordAxial(pointyCoordenadas);
    }

    public static Vector2 CoordenadaACoordAxialFlat(float x, float z, float medidaHex)
    {
        Vector2 flatCoordenadas = new Vector2();
        flatCoordenadas.x = (2f / 3 * x) / medidaHex;
        flatCoordenadas.y = (-1f / 3 * x + Mathf.Sqrt(3) / 3 * z) / medidaHex;

        return RedondearCoordAxial(flatCoordenadas);
    }

    public static Vector2 CoordenadaAOffset(float x, float z, float medidaHex, OrientacionHex orientacion)
    {
        return CoordDeCuboACoorOffset(CoordAxialACoordCubo(DeCoordenadasACoordAxial(x, z, medidaHex, orientacion)), orientacion);
    }

    public static Vector3 CoordenadaTridimensional(float fila, float columna, float medidaHex, OrientacionHex orientacion)
    {
        if (orientacion == OrientacionHex.PointyTop)
        {
            return CoordTridimensionalPointy(columna, fila, medidaHex);
        }
        else
        {
            return CoordTridimensionalFlat(columna, fila, medidaHex);
        }
    }

    private static Vector3 CoordTridimensionalFlat(float columna, float fila, float medidaHex)
    {
        float desplazamientoX = 0;
        float desplazamientoZ = 0;

        if (columna > 0 )
        {
            //desplazamientoX =(columna * medidaHex * (0.87f / (medidaHex * columna))) * columna;
            desplazamientoX = (columna * medidaHex * (2.61f / (medidaHex * columna))) * columna;

        }

        if (fila > 0)
        {
            //desplazamientoZ = (fila * medidaHex * (1.0041f / (medidaHex * fila))) * fila;
            desplazamientoZ = (fila * medidaHex * (3.0123f / (medidaHex * fila))) * fila;

        }

        if (columna % 2 == 1)
        {
            //desplazamientoZ += medidaHex * 0.5f * Mathf.Sqrt(3.0f);

            desplazamientoZ += medidaHex * 1.5f * Mathf.Sqrt(3.0f);
        }

        return new Vector3(desplazamientoX, 0f, desplazamientoZ);

    }

    private static Vector3 CoordTridimensionalPointy(float columna, float fila, float medidaHex)
    {
        float desplazamientoX = 0;
        float desplazamientoZ = 0;
        if(columna > 0)
        {
            //desplazamientoX = (columna * medidaHex * (1.0041f / (medidaHex * columna))) * columna;
            desplazamientoX = (columna * medidaHex * (3.0123f / (medidaHex * columna))) * columna;

        }

        if (fila > 0)
        {
            //desplazamientoZ = (fila * medidaHex * (0.87f / (medidaHex * fila))) * fila;
            desplazamientoZ = (fila * medidaHex * (2.61f / (medidaHex * fila))) * fila;
        }

        if (fila % 2 == 1)
        {
            desplazamientoX += medidaHex * Mathf.Sqrt(3f) / 2.0f;
        }

        return new Vector3(desplazamientoX, 0f, desplazamientoZ);
    }

    public static List<Vector2> ObtenerHexAdyacentes(Vector2 coordenadasAxial)
    {
        List<Vector2> HexAdyacentes = new List<Vector2>();
  
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x + 1, coordenadasAxial.y)); //der 1,0
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x + 1, coordenadasAxial.y - 1)); // arriba a la der
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x, coordenadasAxial.y - 1)); // arriba a la izq
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x - 1, coordenadasAxial.y)); //izq 0,1
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x - 1, coordenadasAxial.y + 1)); // abajo a la izq
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x, coordenadasAxial.y + 1)); // abajo a la der
          
        return HexAdyacentes;
    }

    private static List<Vector2> ObtenerHexAdyacentesPointyTop(Vector2 coordenadasAxial, List<Vector2> HexAdyacentes)
    {
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x + 1, coordenadasAxial.y)); //der
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x - 1, coordenadasAxial.y)); //izq
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x, coordenadasAxial.y + 1)); //diagonal inf der
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x, coordenadasAxial.y - 1)); // diagonal sup izq
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x + 1, coordenadasAxial.y - 1)); // diagonal sup der
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x - 1, coordenadasAxial.y + 1)); //diagonal inf izq
        return HexAdyacentes;
    }

    private static List<Vector2> ObtenerHexAdyacentesFlatTop(Vector2 coordenadasAxial, List<Vector2> HexAdyacentes)
    {
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x + 1, coordenadasAxial.y)); //der
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x - 1, coordenadasAxial.y)); //izq
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x + 1, coordenadasAxial.y + 1)); //diagonal sup der
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x - 1, coordenadasAxial.y - 1)); // diagonal inf izq
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x, coordenadasAxial.y + 1)); // abajo
        HexAdyacentes.Add(new Vector2(coordenadasAxial.x, coordenadasAxial.y - 1)); //arriba
        return HexAdyacentes;
    }
}
