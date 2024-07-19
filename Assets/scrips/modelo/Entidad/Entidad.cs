using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entidad
{
    int Id;
    private static int GlobalCount;
    string Nombre;
    IReino Reino;
    IDieta Dieta;
    IHabitat Habitats;
    int EnergiaActual;
    int EnergiaMax;
    int VidaActual;
    int VidaMax;
    int PuntosAtaque;
    int PuntosDefensa;
    int RangoAtaque;

    #region CONSTRUCTORES
    public Entidad() { }
    public Entidad(string nombre, IReino reino, IDieta dieta, IHabitat habitats, int puntosAtaque, int puntosDefensa, int rangoAtaque)
    {
        NOMBRE = nombre;
        REINO = reino;
        DIETA = dieta;
        HABITATS = habitats;
        PUNTOSATAQUE = puntosAtaque;
        PUNTOSDEFENSA = puntosDefensa;
        RANGOATAQUE = rangoAtaque;
    }

    public Entidad(string nombre, IReino reino, IDieta dieta, IHabitat habitats, int energiaMax, int vidaMax, int puntosAtaque, int puntosDefensa, int rangoAtaque)
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
    public int ID
    {
        get { return Id; }
    }
    public string NOMBRE
    {
        get { return Nombre; }
        set
        {
            if (value != null)
            {
                Nombre = value;
            }
        }
    }

    public IReino REINO
    {
        get { return Reino; }
        set
        {
            if (value != null)
            {
                Reino = value;
            }
        }

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

    public IHabitat HABITATS
    {
        get { return Habitats; }
        set
        {
            if (value != null)
            {
                Habitats = value;
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
    public int VIDAACTUAL
    {
        get { return VidaActual; }
        set
        {
            if (value >= 0 && value <= VidaMax)
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
    public int AtarcarPersonaje()
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
            $" Entidad: " +
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

