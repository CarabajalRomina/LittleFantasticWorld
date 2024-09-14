using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Terreno 
{
    [SerializeField] private Mapa GrillaHex;
    [SerializeField] private TipoDeSubTerreno TipoTerreno;
    [SerializeField] private Vector2 CoordenadasOffset;
    [SerializeField] private Vector3 CoordenadasDeCubo;
    [SerializeField] private Vector2 CoordenadasAxial;
    private List<Terreno> TerrenosLimitrofes;
    [SerializeField] private Transform TransfTerreno;
    [SerializeField] private IEstadoHexEstrategia Estado;

    #region PROPIEDADES

    public Mapa GRILLAHEX
    {
        get { return GrillaHex; }
        set { GrillaHex = value; }
    }

    public TipoDeSubTerreno TIPOTERRENO
    {
        get { return TipoTerreno; }
        set { TipoTerreno = value; }
    }

    public Vector2 COORDENADASOFFSET
    {
        get { return CoordenadasOffset; }
        set { CoordenadasOffset = value; }
    }

    public Vector3 COORDENADASDECUBO
    {
        get { return CoordenadasDeCubo; }
        set { CoordenadasDeCubo = value; }
    }

    public Vector2 COORDENADASAXIAL
    {
        get { return CoordenadasAxial;}
        set {  CoordenadasAxial = value;}
    }

    public List<Terreno> TERRENOSLIMITROFES
    {
        get { return TerrenosLimitrofes; }
        set { TerrenosLimitrofes = value; }
    }

    public Transform TRANSFTERRENO
    {
        get { return TransfTerreno; }
        set { TransfTerreno = value; }
    }

    public IEstadoHexEstrategia ESTADO
    {
        get { return Estado; }
        set { Estado = value; }
    }
    #endregion

    public void SetCoordenadas(Vector2 vector, OrientacionHex orientacion)
    {
        COORDENADASOFFSET = vector;
        COORDENADASAXIAL = MetricasHex.CoordOffsetACoordAxial((int)vector.y,(int)vector.x, orientacion);
        COORDENADASDECUBO = MetricasHex.CoordOffsetACoordCubo((int)vector.x, (int)vector.y,orientacion);
    }

    public void CrearTerreno(Vector2 coordenadas) 
    {
        if (TIPOTERRENO.PREFAB != null)
        {
            var posicionTerreno = MetricasHex.CoordenadaTridimensional(coordenadas.y, coordenadas.x, GRILLAHEX.MEDIDAHEX, GRILLAHEX.ORIENTACION) +  GRILLAHEX.transform.position;

            Quaternion rotacionPrefab = Quaternion.identity;

            if (GRILLAHEX.ORIENTACION == OrientacionHex.FlatTop)
            {
                rotacionPrefab = Quaternion.Euler(0, 30, 0);     
            }

            GameObject nuevoTerreno = GameObject.Instantiate(TIPOTERRENO.PREFAB.gameObject, posicionTerreno, rotacionPrefab);
            TRANSFTERRENO = nuevoTerreno.transform;

            HexTerreno hexTerreno = TRANSFTERRENO.gameObject.GetComponentInChildren<HexTerreno>();
            hexTerreno.OnMouseEnterAction += OnMouseEnter;
            hexTerreno.OnMouseExitAction += OnMouseExit;
            //nuevoTerreno.SetActive(false);
            nuevoTerreno.name = $"Terreno_{TIPOTERRENO.NOMBRE}";
        }
    }

    public void BorrarTerreno()
    {
        if (TRANSFTERRENO != null)
        {
            GameObject.Destroy(TRANSFTERRENO.gameObject);
        }
        TIPOTERRENO = null;
        TerrenosLimitrofes = null;
        GRILLAHEX = null;
    }

    public void InicializarEstado(IEstadoHexEstrategia estadoInicial = null)
    {
        if(estadoInicial == null)
        {
            CambiarEstado(new Visible());
        }
        else
        {
            CambiarEstado(estadoInicial);
        }
    }

    public void CambiarEstado(IEstadoHexEstrategia nuevoEstado)
    {
        if(nuevoEstado != null && nuevoEstado != Estado ) 
        {
            if(Estado != null)
                Estado.DesactivarEstado(this.TRANSFTERRENO);
            Estado = nuevoEstado;
            aplicarEstado();
        }
    }

    public void OnMouseEnter()
    {
        CambiarEstado(Estado.OnMouseEnter());
    }

    public void OnMouseExit()
    {
        CambiarEstado(Estado.OnMouseExit());
    }

    public void Seleccionar()
    {
       CambiarEstado(Estado.Seleccionar());
    }

    public void Deseleccionar()
    {
        CambiarEstado(Estado.Deseleccionar());
    }

    void aplicarEstado()
    {
        Estado.ActivarEstado(this.TRANSFTERRENO);
    }

}



