
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeneradorMapa))]
public class GeneradorMapaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GeneradorMapa generadorMapa = (GeneradorMapa)target;

        if (GUILayout.Button("Generar mapa de ruido"))
        {
            generadorMapa.GenerarMapa();
        }

    }
}
