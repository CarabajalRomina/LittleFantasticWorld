using Assets.scrips.interfaces;
using Assets.scrips.modelo.Entidad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje: Entidad, IEntidad
{
    int Id { get;}
    private static int GlobalCount;
    string Nombre { get; set; }
    IReino Reino { get; set; }
    IDieta Dieta;
    IHabitat Habitats { get; set; }
    int EnergiaActual;
    int EnergiaMax;
    int VidaActual { get; set; }
    int VidaMax { get; set; }
    int PuntosAtaque { get; set; }
    int PuntosDefensa { get; set; }
    int RangoAtaque;

    #region CONSTRUCTORES


    public Personaje(string nombre, IReino reino, IHabitat habitats, int vidaMax, int puntosAtaque, int puntosDefensa, IDieta dieta, int energiaMax, int rangoAtaque) : base(nombre, reino, habitats, vidaMax, puntosAtaque, puntosDefensa)
    {
        Id = ++GlobalCount;
        NOMBRE = nombre;
        REINO = reino;
        DIETA = dieta;
        HABITATS = habitats;
        ENERGIAMAX = energiaMax;
        ENERGIACTUAL = energiaMax;
        VIDAMAX = vidaMax;
        VIDAACTUAL = vidaMax;
        PUNTOSATAQUE = puntosAtaque;
        PUNTOSDEFENSA = puntosDefensa;
        RANGOATAQUE = rangoAtaque;
    }
    #endregion

    #region PROPIEDADES
    
    public IDieta DIETA
    {
        get { return Dieta; }
        set
        {
            if (value != null)
            {
                Dieta = value;
            }
        }
    }

    

    public int ENERGIACTUAL
    {
        get { return EnergiaActual; }
        set
        {
            if (value >= 0 && value <= EnergiaMax)
            {
                EnergiaActual = value;
            }
        }
    }

    public int ENERGIAMAX
    {
        get { return EnergiaMax; }
        set
        {
            if (value > 0)
            {
                EnergiaMax = value;
            }
        }
    }

    public int RANGOATAQUE
    {
        get { return RangoAtaque; }
        set
        {
            if (value >= 0 && value <= 1)
            {
                RangoAtaque = value;
            }
        }
    }

    #endregion


    #region METODOS ATAQUE Y DEFENSA
    public int Atacar()
    {
        var dado = Dado.TirarDado(1, 6);
        return PUNTOSATAQUE + dado;
    }

    public int DefenderDeAtaque()
    {
        var dado = Dado.TirarDado(1, 6);
        return PUNTOSDEFENSA + dado;
    }

    #endregion

    public void Comer(Comida alimento)
    {
        if (!Dieta.PuedoComer(alimento))
        {
            //  Console.WriteLine("no puedes comer este alimento");
        }
        else
        {
            ActualizarEnergia(alimento.CALORIAS);
            //Console.WriteLine("puedes comer este alimento");
        }
    }

    private void ActualizarEnergia(int valor)
    {
        if (valor > 0 && ENERGIACTUAL + valor >= ENERGIAMAX)
        {
            ENERGIACTUAL = ENERGIAMAX;
        }
        else ENERGIACTUAL = +valor;
    }

    public override string ToString()
    {
        return $"" +
            $" Personaje: " +
            $" Id: {ID}," +
            $" Nombre: {NOMBRE}," +
            $" Reino: {REINO}," +
            $" Dieta: {DIETA}," +
            $" Habitats: {HABITATS}," +
            $" Energia maxima: {ENERGIAMAX}," +
            $" Energia actual: {ENERGIACTUAL}," +
            $" Vida maxima: {VIDAMAX}," +
            $" Vida actual: {VIDAACTUAL}," +
            $" Puntos de ataque: {PUNTOSATAQUE}," +
            $" Puntos de defensa: {PUNTOSDEFENSA}," +
            $" Rango de ataque: {RANGOATAQUE}";
    }

    public void AumentarEnergiaActual(int valor)
    {
        ENERGIACTUAL += valor;
    }

    public void AumentarVidaActual(int valor)
    {
        VIDAACTUAL += valor;
    }

    public void ReducirEnergiaActual(int valor)
    {
        ENERGIACTUAL -= valor;
    }

    public void ReducirVidaActual(int valor)
    {
        VIDAACTUAL -= valor;
    }

    public void UsarItem(Item item)
    {
        item.Interactuar(this);
    }

    public void Mover()
    {

    }
    public void Dormir()
    {
        ActualizarEnergia(ENERGIAMAX);
    }
}

