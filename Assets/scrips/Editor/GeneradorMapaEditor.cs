
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeneradorMapa))]
public class GeneradorMapaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GeneradorMapa generadorMapa = (GeneradorMapa)target;

        if (GUILayout.Button("Generar juego de ruido"))
        {
            generadorMapa.GenerarMapa();
        }

    }
}
