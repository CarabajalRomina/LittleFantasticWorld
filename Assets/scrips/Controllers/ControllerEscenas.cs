using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerEscenas : SingletonMonoBehaviour<ControllerEscenas>
{
   public void CambiarEscena(int indiceEscena)
    {
        SceneManager.LoadScene(indiceEscena);
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

}
