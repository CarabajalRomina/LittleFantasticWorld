using Assets.scrips.interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "TipoTerreno", menuName = "Tipos de SubTerreno/Tipos")]
public class TipoDeSubTerreno : ScriptableObject
{
    [SerializeField] private int Id;
    [field:SerializeField] private string Nombre { get; set; }
    [field:SerializeField] private ITipoTerreno TipoTerreno { get; set; }
    [field: SerializeField] private Transform Prefab { get; set; }

    [field: SerializeField] private Color color { get; set; }



    #region PROPIEDADES
    public int ID
    {
        get { return Id; }
    }

    public string NOMBRE
    {
        get { return Nombre; }
    }

    public ITipoTerreno TIPOTERRENO
    {
        get { return TipoTerreno; }
    }

    public Transform PREFAB
    {
        get { return Prefab; }
    }

    public Color COLOR
    {
        get { return color; }
    }
    #endregion

    public void CambiarRotacionPrefab(Vector3 nuevaRotacion)
    {
        if (Prefab != null)
        {
            Prefab.rotation = Quaternion.Euler(nuevaRotacion);
        }
        else
        {
            Debug.LogWarning("El prefab no está asignado en el TipoDeSubTerreno.");
        }
    }

    

    private void OnEnable()
    {
        Id++;
    }
}
