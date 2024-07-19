using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespachadorHiloPrincipal : SingletonMonoBehaviour<DespachadorHiloPrincipal>
{
    private readonly Queue<System.Action> _ejecutarQueue = new Queue<System.Action>();


    public void Enqueue(System.Action action)
    {
        lock (_ejecutarQueue)
        {
            _ejecutarQueue.Enqueue(action);
        }
    }

    private void Update()
    {
        while( _ejecutarQueue.Count > 0 )
        {
            _ejecutarQueue.Dequeue().Invoke();
        }
    }
}
