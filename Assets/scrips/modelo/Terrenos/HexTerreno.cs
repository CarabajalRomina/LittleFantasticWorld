using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]

public class HexTerreno : MonoBehaviour
{

    public event Action OnMouseEnterAction;
    public event Action OnMouseExitAction;

    private Collider ColliderPadre;

    void Start()
    {
        ColliderPadre = GetComponent<Collider>();
        Collider[] hijosColliders = GetComponentsInChildren<Collider>();

        foreach(Collider hijoCollider in hijosColliders)
        {
            hijoCollider.enabled = false;
        }
        ColliderPadre.enabled = true;
    }

    private void OnMouseEnter()
    {
        OnMouseEnterAction?.Invoke();
    }

    private void OnMouseExit()
    {
        OnMouseExitAction?.Invoke();
    }
}
