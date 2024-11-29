using Assets.scrips.interfaces;
using Assets.scrips.interfaces.movible;
using Assets.scrips.modelo.configuraciones;
using Assets.scrips.modelo.entidad;
using Assets.scrips.modelo.inventario;
using UnityEngine;

public class Personaje : Entidad, ICombate, IMovible
{
    int Id;
    static int GlobalCount = 0;
    IDieta Dieta;
    int EnergiaActual { get; set; }
    int EnergiaMax { get; set; }
    int VidaActual { get; set; }
    int VidaMax { get; set; }
    int PuntosAtaque { get; set; }
    int PuntosDefensa { get; set; }
    int RangoAtaque { get; set; }
    private bool enMovimiento = false; // Estado del movimiento
    Inventario InventarioActual;

    // Definir el evento que se disparar� cuando el personaje termine de moverse
    public event System.Action<Terreno> OnMovimientoCompletado;



    #region CONSTRUCTORES
    public Personaje(string nombre, IReino reino, IHabitat habitats, int vidaMax, IDieta dieta, int puntosAtaque, int puntosDefensa, int energiaMax, int rangoAtaque) : base(nombre, reino, habitats)
    {
        Id = ++GlobalCount;
        SetVidaActual(vidaMax);
        SetVidaMax(vidaMax);
        SetPuntosAtaque(puntosAtaque);
        SetPuntosDefensa(puntosDefensa);
        DIETA = dieta;
        SetEnergiaMax(energiaMax);
        SetEnergiaActual(energiaMax);
        SetRangoAtaque(rangoAtaque);
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
        set { Dieta = value; }
    }
    public int VIDAACTUAL
    {
        get { return VidaActual; }
    }

    public int VIDAMAX
    {
        get { return VidaMax; }
    }

    public int ENERGIAACTUAL
    {
        get { return EnergiaActual; }
    }
    public int ENERGIAMAX
    {
        get { return EnergiaMax; }
    }
    public int PUNTOSATAQUE
    {
        get { return PuntosAtaque; }

    }

    public int PUNTOSDEFENSA
    {
        get { return PuntosDefensa; }

    }

    public int RANGOATAQUE
    {
        get { return RangoAtaque; }
    }

    public Inventario INVENTARIOACTUAL
    {
        get { return InventarioActual; }
        set { InventarioActual = value; }
    }

    #endregion
    #region SETTER
    public bool SetVidaActual(int value)
    {
        if (value > 0 || value <= VidaMax)
        {
            VidaActual = value;
            return true;
        }
        else
        {
            if (value > VidaMax)
            {
                VidaActual = VidaMax;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool SetVidaMax(int value)
    {
        if (value > 0)
        {
            VidaMax = value;
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool SetPuntosAtaque(int value)
    {
        if (value > 0)
        {
            PuntosAtaque = value;
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool SetPuntosDefensa(int value)
    {
        if (value > 0)
        {
            PuntosDefensa = value;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool SetEnergiaActual(int value)
    {

        if (value >= 0 || value <= EnergiaMax)
        {
            EnergiaActual = value;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool SetEnergiaMax(int value)
    {
        if (value > 0)
        {
            EnergiaMax = value;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool SetRangoAtaque(int value)
    {
        if (value >= 0 && value <= 1)
        {
            RangoAtaque = value;
            return true;
        }
        else
        {
            RangoAtaque = 1;
            return true;
        }
    }

    #endregion

    #region METODOS COMBATE
    public int Atacar()
    {
        var dado = Dado.TirarDado();
        return PuntosAtaque + dado;
    }

    public int Defender()
    {
        var dado = Dado.TirarDado();
        return PuntosDefensa + dado;
    }

    #endregion

    #region MOVIMIENTO
    public void IniciarMovimiento()
    {
        if (!enMovimiento) // Solo inicia el movimiento si no est� ya movi�ndose
        {
            enMovimiento = true; // Cambia el estado a movi�ndose
        }
    }
    public void MoverHacia(Terreno terrenoDestino)
    {
        if (enMovimiento)
        {
            //Mueve el personaje suavemente hacia la posici�n objetivo
            var llegadaTerrenoDestino = Vector3.Lerp(
                TerrenoActual.POSICIONTRIDIMENSIONAL,
                terrenoDestino.POSICIONTRIDIMENSIONAL,
                ConfiguracionGeneral.VelocidadMovimientoPersonaje * Time.deltaTime * 10
                );
            // Comprueba si ha llegado a la posici�n objetivo
            if (Vector3.Distance(llegadaTerrenoDestino, terrenoDestino.POSICIONTRIDIMENSIONAL) < 0.1f)
            {
                ReducirEnergiaActual(ConfiguracionGeneral.PuntosEnergiaBase);
                TERRENOACTUAL.EliminarEntidad(this);
                // Asegura que la posici�n actual sea exactamente la posici�n objetivo
                TerrenoActual = terrenoDestino;
                InstanciaPersonaje.transform.position = terrenoDestino.POSICIONTRIDIMENSIONAL + new Vector3(0, 0.6f, 0);
                TERRENOACTUAL.AgregarEntidad(this);
                TERRENOACTUAL.CambiarEstado(new Ocupado());
                ReducirEnergiaActual(ConfiguracionGeneral.PuntosEnergiaBase);
                // Disparar el evento indicando que el movimiento ha finalizado
                OnMovimientoCompletado?.Invoke(TerrenoActual);
                enMovimiento = false; // Resetea el estado de movimiento

            }
            else
            {
                Debug.Log("no se llego a destino");
            }
        }
        else
        {
            Debug.Log("no se inicio el movimiento");
        }

    }
    #endregion


    public bool Comer(Comida alimento)
    {
        if (!Dieta.PuedoComer(alimento))
        {
            Debug.Log("no puedes comer este alimento");
            return false;
        }
        else
        {
            ActualizarEnergia(alimento.CALORIAS);
            Debug.Log("puedes comer este alimento");
            return true;
        }
    }

    private void ActualizarEnergia(int valor)
    {
        if (valor > 0 && EnergiaActual + valor >= EnergiaMax)
        {
            SetEnergiaActual(EnergiaMax);
        }
        else SetEnergiaActual(EnergiaActual = +valor);
    }

    public bool AumentarEnergiaActual(int valor)
    {
        if ((EnergiaActual += valor) < ENERGIAMAX)
        {
            if (SetEnergiaActual(EnergiaActual += valor))
            {
                return true;
            }
            else return false;
        }
        else
        {
            SetEnergiaActual(ENERGIAMAX);
            return true;
        }
    }
    public bool ReducirEnergiaActual(int valor)
    {
        if((EnergiaActual -= valor) >= 0)
        {
            if (SetEnergiaActual(EnergiaActual -= valor))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            SetEnergiaActual(0);
            return true;
        }
       
    }


    public bool AumentarVidaActual(int valor)
    {
        if (SetVidaActual(VidaActual += valor))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ReducirVidaActual(int valor)
    {
        if (SetVidaActual(VidaActual -= valor))
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    public bool AumentarPuntosAtaque(int valor)
    {
        if (SetPuntosAtaque(PuntosAtaque += valor))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ReducirPuntosAtaque(int valor)
    {
        if (SetPuntosAtaque(PuntosAtaque -= valor))
        {
            return true;
        }
        else return false;
    }

    public bool AumentarPuntosDefensa(int valor)
    {
        if (SetPuntosDefensa(PuntosAtaque += valor))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool ReducirPuntosDefensa(int valor)
    {
        if (SetPuntosDefensa(PuntosDefensa -= valor))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UsarItem(Item item)
    {
        if (item.Interactuar(this))
        {
            Debug.Log("SE INTERACTUO CON EL ITEM CORRECTAMENTE");

            return true;
        }
        else
        {
            Debug.Log("NO SE PUEDE INTERACTUAR CON EL ITEM");
            return false;
        }

    }

    public void Dormir()
    {
        ActualizarEnergia(EnergiaMax);
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
            $" Energia maxima: {EnergiaMax}," +
            $" Energia actual: {EnergiaActual}," +
            $" Vida maxima: {VidaMax}," +
            $" Vida actual: {VidaActual}," +
            $" Puntos de ataque: {PuntosAtaque}," +
            $" Puntos de defensa: {PuntosDefensa}," +
            $" Rango de ataque: {RangoAtaque}," +
            $" prefab: {PERSONAJEPREFAB}";

    }

    public override string[] ObtenerValoresInstancias()
    {
        return new string[] {
            ID.ToString(),
            NOMBRE,
            REINO.ToString(),
            HABITATS.ToString(),
            DIETA.ToString(),
            EnergiaMax.ToString(),
            VidaMax.ToString(),
            PuntosAtaque.ToString(),
            PuntosDefensa.ToString(),
            RangoAtaque.ToString()
            };
    }


}



