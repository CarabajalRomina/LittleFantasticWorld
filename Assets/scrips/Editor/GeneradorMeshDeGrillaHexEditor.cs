using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeneradorMeshDeGrillaHex))]

public class GeneradorMeshDeGrillaHexEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GeneradorMeshDeGrillaHex generadorMeshDeGrillaHex = (GeneradorMeshDeGrillaHex)target;
        
        if(GUILayout.Button("Generar malla hex"))
        {
            generadorMeshDeGrillaHex.CrearMallaHex();
        }

        if(GUILayout.Button("Borrar malla hex"))
        {
            generadorMeshDeGrillaHex.BorrarMallaGrillaHex();
        }
    }

}
