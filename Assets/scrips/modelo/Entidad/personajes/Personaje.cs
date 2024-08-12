using Assets.scrips.interfaces;
using Assets.scrips.modelo.Entidad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : Entidad, ICombate
{
    int Id;
    static int GlobalCount = 0;
    IDieta Dieta;
    int EnergiaActual;
    int EnergiaMax;
    int VidaActual;
    int VidaMax;
    int PuntosAtaque;
    int PuntosDefensa;
    int RangoAtaque;


    #region CONSTRUCTORES
    public Personaje(string nombre, IReino reino, IHabitat habitats, int vidaMax, IDieta dieta, int puntosAtaque, int puntosDefensa, int energiaMax, int rangoAtaque) : base(nombre, reino, habitats)
    {
        Id = ++GlobalCount;
        VIDAACTUAL = vidaMax;
        VIDAMAX = vidaMax;
        PUNTOSATAQUE = puntosAtaque;
        PUNTOSDEFENSA = puntosDefensa;
        DIETA = dieta;
        ENERGIAMAX = energiaMax;
        ENERGIACTUAL = energiaMax;
        RANGOATAQUE = rangoAtaque;
    }

    #endregion

    #region PROPIEDADES
    public int ID
    {
        get { return Id; }
    }
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

    public int VIDAACTUAL
    {
        get { return VidaActual; }
        set
        {
            if (value >= 0 || value <= VidaMax)
            {
                VidaActual = value;
            }
        }
    }

    public int VIDAMAX
    {
        get { return VidaMax; }
        set
        {
            if (value > 0)
            {
                VidaMax = value;
            }
        }
    }

    public int PUNTOSATAQUE
    {
        get { return PuntosAtaque; }
        set
        {
            if (value > 0)
            {
                PuntosAtaque = value;
            }
        }
    }

    public int PUNTOSDEFENSA
    {
        get { return PuntosDefensa; }
        set
        {
            if (value > 0)
            {
                PuntosDefensa = value;
            }
        }
    }

    public int ENERGIACTUAL
    {
        get { return EnergiaActual; }
        set
        {
            if (value >= 0 || value <= EnergiaMax)
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
            else
            {
                RangoAtaque = 1;
            }
        }
    }

    #endregion


    #region METODOS COMBATE
    public int Atacar()
    {
        var dado = Dado.TirarDado(1, 6);
        return PUNTOSATAQUE + dado;
    }

    public int Defender()
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

    public override string[] ObtenerValoresInstancias()
    {
        return new string[] {
            ID.ToString(),
            NOMBRE,
            REINO.ToString(),
            HABITATS.ToString(),
            DIETA.ToString(),
            ENERGIAMAX.ToString(),
            VIDAMAX.ToString(),
            PUNTOSATAQUE.ToString(),
            PUNTOSDEFENSA.ToString(),
            RangoAtaque.ToString()
            };
    }
}

