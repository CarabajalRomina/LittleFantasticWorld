using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerEscenas : SingletonMonoBehaviour<ControllerEscenas>
{
    /// <summary>
    /// Carga una nueva escena destruyendo en la que estaba 
    /// </summary>
    /// <param name="indiceEscena"></param>

    public void CambiarEscena(int indiceEscena)
    {
        SceneManager.LoadScene(indiceEscena);
    }

    void DesactivarCamarasDeEscenaMapa()
    {
        Scene escenaActiva = SceneManager.GetActiveScene();

        // Asegurarnos de que la escena activa esté cargada
        if (!escenaActiva.isLoaded)
        {
            Debug.LogWarning("No hay ninguna escena activa cargada.");
            return;
        }

        // Obtener los objetos raíz de la escena activa
        GameObject[] objetosRaiz = escenaActiva.GetRootGameObjects();

        foreach (GameObject objeto in objetosRaiz)
        {
            // Buscar cámaras en los objetos raíz y sus hijos
            Camera camara = objeto.GetComponentInChildren<Camera>();
            if (camara != null)
            {
                camara.gameObject.SetActive(false);
                Debug.Log($"Cámara desactivada: {camara.name} en la escena activa.");
            }
        }
    }

    // Activa todas las cámaras de la escena activa
    void ActivarCamarasDeEscenaMapa()
    {
        Scene escenaActiva = SceneManager.GetActiveScene();

        // Asegurarnos de que la escena activa esté cargada
        if (!escenaActiva.isLoaded)
        {
            Debug.LogWarning("No hay ninguna escena activa cargada.");
            return;
        }

        // Obtener los objetos raíz de la escena activa
        GameObject[] objetosRaiz = escenaActiva.GetRootGameObjects();

        foreach (GameObject objeto in objetosRaiz)
        {
            // Buscar cámaras en los objetos raíz y sus hijos
            Camera camara = objeto.GetComponentInChildren<Camera>();
            if (camara != null)
            {
                camara.gameObject.SetActive(true);
                Debug.Log($"Cámara activada: {camara.name} en la escena activa.");
            }
        }
    }
    void DesactivarTodosLosUIDeMapa()
    {

        Scene escena = SceneManager.GetActiveScene();

        if (escena.isLoaded) // Verificar si la escena está cargada
        {
            foreach (GameObject objeto in escena.GetRootGameObjects())
            {
                // Buscar todos los componentes Canvas en este objeto raíz
                Canvas canvas = objeto.GetComponentInChildren<Canvas>();
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(false); // Desactivar el Canvas
                }
            }
        }
        else
        {
            Debug.LogWarning($"La escena  no está cargada.");
        }
    }

    void ActivarTodosLosUIMapa()
    {
        Scene escena = SceneManager.GetActiveScene();

        if (escena.isLoaded) // Verificar si la escena está cargada
        {
            foreach (GameObject objeto in escena.GetRootGameObjects())
            {
                // Buscar todos los componentes Canvas en este objeto raíz
                Canvas canvas = objeto.GetComponentInChildren<Canvas>();
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(true); // Desactivar el Canvas
                }
            }
        }
        else
        {
            Debug.LogWarning($"La escena  no está cargada.");
        }
    }

    void DesacticarCanvasYCamaraEnEscena()
    {

        DesactivarCamarasDeEscenaMapa();
        DesactivarTodosLosUIDeMapa();
    }

    void ActivarCanvasYCamarasEnEscena()
    {
        ActivarCamarasDeEscenaMapa();
        ActivarTodosLosUIMapa();
    }

    /// <summary>
    /// Carga una nueva escena sin destruir en la que estaba
    /// </summary>
    /// <param name="indiceEscena"></param>
    public void CargarEscenaAditiva(int indiceEscena)
    {
        SceneManager.LoadScene(indiceEscena, LoadSceneMode.Additive);
        DesacticarCanvasYCamaraEnEscena();

    }

    public void EliminarEscena(int indiceEscena)
    {
        if (SceneManager.GetSceneByBuildIndex(indiceEscena).isLoaded)
        {
            SceneManager.UnloadSceneAsync(indiceEscena);
        }
        else
        {
            Debug.LogWarning($"La escena '{indiceEscena}' no está cargada.");
        }
    }

   
    public void SalirJuego()
    {
        Application.Quit();
    }

    /// <summary>
    /// Método para instanciar un modelo con parámetros personalizados. con rotacion y sin escala personalizada
    /// </summary>

    public GameObject InstanciarModelo(GameObject prefab, Vector3 posicion, Vector3 rotacion)
    {
        if (prefab == null)
        {
            Debug.LogError("El prefab es nulo.");
            return null;
        }

        var instancia = Instantiate(prefab, posicion, Quaternion.Euler(rotacion));
        return instancia;
    }

    /// <summary>
    /// Método para instanciar un modelo, sin parametros personalizados.
    /// </summary>

    public GameObject InstanciarModelo(GameObject prefab, Vector3 posicion)
    {
        if (prefab == null)
        {
            Debug.LogError("El prefab es nulo.");
            return null;
        }

        var instancia = Instantiate(prefab, posicion, Quaternion.identity);
        return instancia;
    }

    /// <summary>
    /// Método para instanciar un modelo con parámetros personalizados. Sin rotacion y con escala personalizada
    /// </summary>

    public GameObject InstanciarModelo(GameObject prefab, Vector3 posicion, Vector3 escala, Quaternion rotacionInicial)
    {
        if (prefab == null)
        {
            Debug.LogError("El prefab es nulo.");
            return null;
        }

        var instancia = Instantiate(prefab, posicion, rotacionInicial);
        instancia.transform.localScale = escala;

        return instancia;
    }
}
