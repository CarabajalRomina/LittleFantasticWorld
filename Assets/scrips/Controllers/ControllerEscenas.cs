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

        // Asegurarnos de que la escena activa est� cargada
        if (!escenaActiva.isLoaded)
        {
            Debug.LogWarning("No hay ninguna escena activa cargada.");
            return;
        }

        // Obtener los objetos ra�z de la escena activa
        GameObject[] objetosRaiz = escenaActiva.GetRootGameObjects();

        foreach (GameObject objeto in objetosRaiz)
        {
            // Buscar c�maras en los objetos ra�z y sus hijos
            Camera camara = objeto.GetComponentInChildren<Camera>();
            if (camara != null)
            {
                camara.gameObject.SetActive(false);
                Debug.Log($"C�mara desactivada: {camara.name} en la escena activa.");
            }
        }
    }

    // Activa todas las c�maras de la escena activa
    void ActivarCamarasDeEscenaMapa()
    {
        Scene escenaActiva = SceneManager.GetActiveScene();

        // Asegurarnos de que la escena activa est� cargada
        if (!escenaActiva.isLoaded)
        {
            Debug.LogWarning("No hay ninguna escena activa cargada.");
            return;
        }

        // Obtener los objetos ra�z de la escena activa
        GameObject[] objetosRaiz = escenaActiva.GetRootGameObjects();

        foreach (GameObject objeto in objetosRaiz)
        {
            // Buscar c�maras en los objetos ra�z y sus hijos
            Camera camara = objeto.GetComponentInChildren<Camera>();
            if (camara != null)
            {
                camara.gameObject.SetActive(true);
                Debug.Log($"C�mara activada: {camara.name} en la escena activa.");
            }
        }
    }
    void DesactivarTodosLosUIDeMapa()
    {

        Scene escena = SceneManager.GetActiveScene();

        if (escena.isLoaded) // Verificar si la escena est� cargada
        {
            foreach (GameObject objeto in escena.GetRootGameObjects())
            {
                // Buscar todos los componentes Canvas en este objeto ra�z
                Canvas canvas = objeto.GetComponentInChildren<Canvas>();
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(false); // Desactivar el Canvas
                }
            }
        }
        else
        {
            Debug.LogWarning($"La escena  no est� cargada.");
        }
    }

    void ActivarTodosLosUIMapa()
    {
        Scene escena = SceneManager.GetActiveScene();

        if (escena.isLoaded) // Verificar si la escena est� cargada
        {
            foreach (GameObject objeto in escena.GetRootGameObjects())
            {
                // Buscar todos los componentes Canvas en este objeto ra�z
                Canvas canvas = objeto.GetComponentInChildren<Canvas>();
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(true); // Desactivar el Canvas
                }
            }
        }
        else
        {
            Debug.LogWarning($"La escena  no est� cargada.");
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
            Debug.LogWarning($"La escena '{indiceEscena}' no est� cargada.");
        }
    }

   
    public void SalirJuego()
    {
        Application.Quit();
    }

    /// <summary>
    /// M�todo para instanciar un modelo con par�metros personalizados. con rotacion y sin escala personalizada
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
    /// M�todo para instanciar un modelo, sin parametros personalizados.
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
    /// M�todo para instanciar un modelo con par�metros personalizados. Sin rotacion y con escala personalizada
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
