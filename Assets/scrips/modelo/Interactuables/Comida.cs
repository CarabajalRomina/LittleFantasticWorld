using Assets.scrips.interfaces;
using Assets.scrips.interfaces.interactuable;
using Assets.scrips.interfaces.posicionable;
using Assets.scrips.modelo.entidad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida: IInteractuable, IDescribible, IPosicionable
{
    int Id;
    static int GlobalCount = 0;
    string Nombre;
    int Calorias;
    IDieta TipoDieta;
    Terreno TerrenoActual;

    #region PROPIEDADES
    public int ID
    {
        get { return Id; }
    }
    public string NOMBRE
    {
        get { return Nombre; }
        set { Nombre = value; }
    }
    public int CALORIAS
    {
        get { return Calorias; }
        set { Calorias = value; }
    }
    public IDieta TIPODIETA
    {
        get { return TipoDieta;}
        set { TipoDieta = value; }
    }
    public Terreno TERRENOACTUAL
    {
        get { return TerrenoActual; }
        set { TerrenoActual = value; }
    }

    #endregion

    public Comida(string nombre,int calorias, IDieta tipoDieta)
    {
        Id = ++GlobalCount;
        NOMBRE = nombre;
        CALORIAS = calorias;
        TIPODIETA = tipoDieta;
    }
  
    public override string ToString()
    {
        return $"" +
            $" Comida: " +
            $" Id: {ID}," +
            $" Nombre: {NOMBRE}," +
            $" Calorias: {CALORIAS}," +
            $" Dieta: {TIPODIETA},";
    }

    public bool Interactuar(Personaje personaje)
    {
        if (personaje.Comer(this))
        {
            return true;
        }
        else return false;
    }

    public string[] ObtenerValoresInstancias()
    {
        return new string[] {
            ID.ToString(),
            NOMBRE,
            TIPODIETA.ToString(),
            CALORIAS.ToString(),
            };
    }

    public void EstablecerPosicion(Terreno terrenoDestino)
    {
        TerrenoActual = terrenoDestino;
        TerrenoActual.AgregarInteractuable(this);
    }

    public string GetNombre()
    {
        return NOMBRE;
    }
}
