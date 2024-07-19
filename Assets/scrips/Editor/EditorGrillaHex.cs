using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Mapa))]
public class EditorGrillaHex : Editor
{
    private void OnSceneGUI()
    {
        Mapa hexGrilla = (Mapa)target;

        for(int z = 0; z < hexGrilla.ANCHO; z++)
        {
            for(int x = 0; x < hexGrilla.ALTO; x++) 
            {
                Vector3 PuntoCentral = MetricasHex.PuntoCentralHex(hexGrilla.MEDIDAHEX, x, z, hexGrilla.ORIENTACION);
                int centroX = x;
                int centroZ = z;

                Vector3 coordCubicas = MetricasHex.CoordOffsetACoordCubo(centroX, centroZ, hexGrilla.ORIENTACION);

                Handles.Label((PuntoCentral + hexGrilla.transform.position ) + Vector3.forward * 0.5f, $"[{centroX}, {centroZ}]");
                Handles.Label((PuntoCentral + hexGrilla.transform.position - new Vector3(0, .5f, 0)), $"({coordCubicas.x}, {coordCubicas.y}, {coordCubicas.z})");

            }
        }
    }
}
