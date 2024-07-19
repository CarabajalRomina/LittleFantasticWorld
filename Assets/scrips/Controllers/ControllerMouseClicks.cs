using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ControllerMouseClicks: SingletonMonoBehaviour<ControllerMouseClicks>
{

    public Action<RaycastHit> OnIzqMouseClick;
    public Action<RaycastHit> OnDerMouseClick;
    public Action<RaycastHit> OnMedioMouseClick;

    public event Action OnMouseEnterAction;
    public event Action OnMouseExitAction;

    Vector3 posicionHexagono;


   
    private void OnMouseEnter()
    {
        OnMouseEnterAction?.Invoke();
        Debug.Log("posicion hexagono"+ posicionHexagono.x + posicionHexagono.y + posicionHexagono.z);
    }

    private void OnMouseExit()
    {
        OnMouseExitAction?.Invoke();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChequearMouseClick(0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            ChequearMouseClick(1);
        }

        if (Input.GetMouseButtonDown(2))
        {
            ChequearMouseClick(2);
        }
    }

    void ChequearMouseClick(int btnMouse)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(btnMouse == 0)
            {
                OnIzqMouseClick?.Invoke(hit);
                return;
            }
            else if (btnMouse == 1)
            {
                OnDerMouseClick?.Invoke(hit);
            }
            else if (btnMouse == 2)
            {
                OnMedioMouseClick?.Invoke(hit);
            }
        }
    }

}
