using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ControllerCamara : MonoBehaviour
{
    [SerializeField] private GameObject objetivoCamara;
    [SerializeField] private CinemachineVirtualCamera camaraVirtual;
    [SerializeField] private float velocidadCamara = 10f;

    private Coroutine panonamicaCorrutina;

    public void CambioPanoramica(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(panonamicaCorrutina != null) 
            { 
                StopCoroutine(panonamicaCorrutina);
            }
            panonamicaCorrutina = StartCoroutine(ProcesoPanoramica(context));

        }else if (context.canceled)
        {
            if(panonamicaCorrutina != null)
            {
                StopCoroutine(panonamicaCorrutina);
                panonamicaCorrutina = null;
            }
        }
    }


    public IEnumerator ProcesoPanoramica(InputAction.CallbackContext context)
    {
        while(true)
        {
            Vector2 vectorEntrada = context.ReadValue<Vector2>();
            Debug.Log(context.phase);
            Debug.Log("Moving:" + vectorEntrada);

            Vector3 desplazamiento = new Vector3(vectorEntrada.x, 0, vectorEntrada.y);
            objetivoCamara.transform.position += desplazamiento * velocidadCamara * Time.deltaTime;
            yield return null;
        }
    }

    public void CambioDeEnfoque(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("doble tap = enfocar");
        }else if (context.canceled)
        {
            Debug.Log("1 tap = seleccionar");
        }
    }
    
}
