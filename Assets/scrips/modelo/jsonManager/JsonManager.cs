using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;


namespace Assets.scrips.modelo.jsonManager
{
    public static class JsonManager
    {
        // Método para leer datos desde un archivo JSON y deserializar a un objeto de tipo T
        public static T LeerDesdeArchivoJson<T>(string rutaArchivo)
        {
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    string jsonData = File.ReadAllText(rutaArchivo);
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }
                else
                {
                    Debug.LogWarning($"El archivo {rutaArchivo} no existe.");
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error al leer JSON: {ex.Message}");
                return default(T);
            }
        }

        // Método para guardar datos en un archivo JSON desde un objeto de tipo T
        public static void GuardarEnArchivoJson<T>(string rutaArchivo, T objeto)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(objeto, Formatting.Indented);
                File.WriteAllText(rutaArchivo, jsonData);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error al guardar JSON: {ex.Message}");
            }
        }
    }
}

