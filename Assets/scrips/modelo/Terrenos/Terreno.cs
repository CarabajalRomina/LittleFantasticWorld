using Assets.scrips.Controllers;
using Assets.scrips.interfaces.interactuable;
using Assets.scrips.modelo.configuraciones;
using Assets.scrips.modelo.entidad;
using Assets.scrips.modelo.estadosDeInteraccion.tiposEstados;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Terreno 
{
    [SerializeField] private Mapa GrillaHex;
    [SerializeField] private TipoDeSubTerreno TipoSubTerreno;
    [SerializeField] private Vector2 CoordenadasOffset;
    [SerializeField] private Vector3 PosicionTridimensinal;
    [SerializeField] private Vector2 CoordenadasAxial;
    private List<Terreno> TerrenosLimitrofes;
    [SerializeField] private Transform TransfTerreno;
    [SerializeField] private IEstadoHexEstrategia Estado;
    List<Entidad> Entidades =  new List<Entidad>();
    List<IInteractuable> Interactuables =  new List<IInteractuable>();

    #region PROPIEDADES

    public Mapa GRILLAHEX
    {
        get { return GrillaHex; }
        set { GrillaHex = value; }
    }

    public TipoDeSubTerreno TIPOSUBTERRENO
    {
        get { return TipoSubTerreno; }
        set { TipoSubTerreno = value; }
    }

    public Vector2 COORDENADASOFFSET
    {
        get { return CoordenadasOffset; }
        set { CoordenadasOffset = value; }
    }

    public Vector3 POSICIONTRIDIMENSIONAL
    {
        get { return PosicionTridimensinal; }
        set { PosicionTridimensinal = value; }
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

    public List<Entidad> ENTIDADES
    {
        get { return Entidades; }
        set { Entidades = value; }
    }

    public List<IInteractuable> INTERACTUABLES
    {
        get { return Interactuables;}
        set { Interactuables = value; }
    }

    #endregion

    public void SetCoordenadas(Vector2 vector, OrientacionHex orientacion, float medidaHex)
    {
        COORDENADASOFFSET = vector;
        COORDENADASAXIAL = MetricasHex.CoordOffsetACoordAxial((int)vector.y, (int)vector.x, orientacion);
        POSICIONTRIDIMENSIONAL = MetricasHex.CoordenadaTridimensional(vector.y, vector.x, medidaHex, orientacion) + GRILLAHEX.ORIGENGRID;
    }
    public void CrearInstanciaTerrenoPrefab(Vector2 coordenadas)
    {
        if (TIPOSUBTERRENO.PREFAB != null)
        {
            Quaternion rotacionPrefab = Quaternion.identity;

            if (GRILLAHEX.ORIENTACION == OrientacionHex.FlatTop)
            {
                rotacionPrefab = Quaternion.Euler(0, 30, 0);
            }

            GameObject nuevoTerreno = GameObject.Instantiate(TIPOSUBTERRENO.PREFAB.gameObject, PosicionTridimensinal, rotacionPrefab);
            TRANSFTERRENO = nuevoTerreno.transform;     
            InicializarEstado(new Oculto());


            HexTerreno hexTerreno = TRANSFTERRENO.gameObject.GetComponentInChildren<HexTerreno>();
            hexTerreno.OnMouseEnterAction += OnMouseEnter;
            hexTerreno.OnMouseExitAction += OnMouseExit;
            hexTerreno.OnMouseClickAction += OnMouseDown;
            //nuevoTerreno.SetActive(false);
            nuevoTerreno.name = $"Terreno_{TIPOSUBTERRENO.NOMBRE}";
        }
    }

    public void BorrarTerreno()
    {
        if (TRANSFTERRENO != null)
        {
            GameObject.Destroy(TRANSFTERRENO.gameObject);
        }
        TIPOSUBTERRENO = null;
        TerrenosLimitrofes = null;
        GRILLAHEX = null;
    }

    #region MANEJO ESTADOS DE TERRENO

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
        if(nuevoEstado != Estado) 
        {
            if (Estado != null)
                ESTADO.DesactivarEstado(this);
            Estado = nuevoEstado;
            AplicarEstado();
        }
    }

    public void OnMouseEnter()
    {
        if(ESTADO is Visible && !(TIPOSUBTERRENO.TIPOTERRENO is Especial) && !(Estado is Ocupado))
        {
            CambiarEstado(new Resaltado());
            if(ENTIDADES.Count > 0)
            {
                foreach (var entidad in this.ENTIDADES)
                {
                    Debug.Log(entidad.ToString());
                }
            }
            if(INTERACTUABLES.Count > 0)
            {
                foreach (var interactuavle in INTERACTUABLES)
                {
                    Debug.Log(interactuavle.ToString());
                }
            }
            
        }
    }

    public void OnMouseExit()
    {
        if (ESTADO is Resaltado)
        {
            CambiarEstado(new Visible());
        }
    }

    public void OnMouseDown()
    {
       ControllerMovimiento  cntMovimientoController = ControllerMovimiento .Instancia;
       if (!(TIPOSUBTERRENO.TIPOTERRENO is Especial) && !(Estado is Ocupado) && cntMovimientoController != null)
       {
            CambiarEstado(new Seleccionado(cntMovimientoController));
       }
    }

    void AplicarEstado()
    {
        Estado.ActivarEstado(this);
    }

    public void CambiarEstadoTerrenosLimitrofes(IEstadoHexEstrategia estado)
    {
        foreach (var terreno in TERRENOSLIMITROFES)
        {
            if (terreno.Estado != estado)
                terreno.CambiarEstado(estado);
        }
    }

    #endregion

    public void AgregarEntidad(Entidad entidad)
    {
        if ((entidad is Personaje && CantPersonajes() >= ConfiguracionGeneral.CantMaxPersonajesXTerreno) && Entidades.Contains(entidad))
        {
            Debug.Log("Ya existe un personaje-entidad en el terreno");
        }
        else
        {
            try
            {
                Entidades.Add(entidad);

            }catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    public void EliminarEntidad(Entidad entidad)
    {
        if (Entidades.Contains(entidad))
        {
            Entidades.Remove(entidad);
        }
    }

    public void AgregarInteractuable(IInteractuable interactuable)
    {
        var cantMaxComida = ConfiguracionGeneral.CantidadMaxComidaXTerreno;
        var cantMaxItems = ConfiguracionGeneral.CantidadMaxItemsXTerreno;
        if (Interactuables.Count > (cantMaxComida + cantMaxItems)  || Interactuables.Contains(interactuable))
        {
            Debug.Log("Ya existen interactuables en el terreno");
        }
        else
        {
            Interactuables.Add(interactuable);
        }
    }

    public void EliminarInteractuable(IInteractuable interactuable)
    {
        if (Interactuables.Contains(interactuable))
        {
            Interactuables.Remove(interactuable);
        }
        else { Debug.Log("No existe este interactuable en el terreno"); }
    }

    public Personaje GetPersonaje()
    {
        return Entidades.OfType<Personaje>().FirstOrDefault();
    }

    public List<Enemigo> GetEnemigos()
    {
        return Entidades.OfType<Enemigo>().ToList();
    }

    int CantPersonajes()
    {
        return Entidades.OfType<Personaje>().Count();
    }

    int CantEnemigos()
    {
        return Entidades.OfType<Enemigo>().Count();
    }

}



