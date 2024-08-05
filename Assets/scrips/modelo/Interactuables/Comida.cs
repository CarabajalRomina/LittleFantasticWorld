using Assets.scrips.interfaces.interactuable;
using Assets.scrips.modelo.Entidad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida: IInteractuable
{
    int Id;
    static int GlobalCount = 0;
    string Nombre;
    int Calorias;
    IDieta TipoDieta;
    


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

    public void Interactuar(Personaje entidad)
    {
        entidad.Comer(this);
    }
}
