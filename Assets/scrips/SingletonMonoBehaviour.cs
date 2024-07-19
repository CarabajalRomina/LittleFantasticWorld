using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instancia;
    private static readonly object _BloqueoInstancia = new object();
    private static bool _quitting = false;


    public static T Instancia
    {
        get
        {
            lock (_BloqueoInstancia)
            {
                if (_instancia == null && !_quitting)
                {
                    _instancia = FindObjectOfType<T>();
                    if (_instancia == null)
                    {
                        GameObject go = new GameObject(typeof(T).ToString());
                        _instancia = go.AddComponent<T>();

                        DontDestroyOnLoad(_instancia.gameObject);
                    }

                }
                return _instancia;
            }
        }

    }

    protected virtual void Awake()
    {
        Init();
        if (_instancia != null && _instancia != this) {
            Destroy(gameObject);
            throw new System.Exception(string.Format("instancia a {0} ya existe, borrando {1}", GetType().FullName, ToString()));
        }
        else
        {
            _instancia = gameObject.GetComponent<T>();
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _quitting = true;
    }

    protected virtual void Init() { }

}
