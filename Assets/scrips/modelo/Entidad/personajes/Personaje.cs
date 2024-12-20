using Assets.scrips.interfaces;
using Assets.scrips.interfaces.movible;
using Assets.scrips.interfaces.pelea;
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
    public int VidaActual { get; set; }
    public int VidaMax { get; set; }

    int PuntosAtaque { get; set; }
    int PuntosDefensa { get; set; }
    int RangoAtaque { get; set; }
    private bool enMovimiento = false; // Estado del movimiento
    Inventario InventarioActual;

    private bool EstaDefendiendo; // Indica si el personaje está en modo defensa

    // Definir el evento que se disparará cuando el personaje termine de moverse
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

    public int VIDAACTUAL
    {
        get { return VidaActual; }
    }

    public int VIDAMAX
    {
        get { return VidaMax; }
        set { VidaMax = value; }
    }

    public bool ESTADEFENDIENDO
    {
        get { return EstaDefendiendo; }
        set { EstaDefendiendo = value; }

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
                VidaActual = 0;
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
    public int ObtenerVidaActual()
    {
        return VIDAACTUAL;
    }

    public int ObtenerVidaMaxima()
    {
        return VIDAMAX;
    }

    public bool ActualizarVidaActual(int value)
    {
        return SetVidaActual(value);
    }

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
    public void ActivarDefensa()
    {
        EstaDefendiendo = true;
    }

    public bool SeEstaDefendiendo()
    {
        return EstaDefendiendo;
    }
    public string ObtenerNombre()
    {
        return NOMBRE;
    }

    public bool EstaVivo()
    {
        if (VidaActual > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RecibirDanio(int danio)
    {
        ReducirVidaActual(danio);
    }

    public GameObject ObtenerPrefab()
    {
        return PERSONAJEPREFAB;
    }

    #endregion

    #region MOVIMIENTO
    public void IniciarMovimiento()
    {
        if (!enMovimiento) // Solo inicia el movimiento si no está ya moviéndose
        {
            enMovimiento = true; // Cambia el estado a moviéndose
        }
    }
    public void MoverHacia(Terreno terrenoDestino)
    {
        if (enMovimiento)
        {
            //Mueve el personaje suavemente hacia la posición objetivo
            var llegadaTerrenoDestino = Vector3.Lerp(
                TerrenoActual.POSICIONTRIDIMENSIONAL,
                terrenoDestino.POSICIONTRIDIMENSIONAL,
                ConfiguracionGeneral.VelocidadMovimientoPersonaje * Time.deltaTime * 10
                );
            // Comprueba si ha llegado a la posición objetivo
            if (Vector3.Distance(llegadaTerrenoDestino, terrenoDestino.POSICIONTRIDIMENSIONAL) < 0.1f)
            {
                TERRENOACTUAL.EliminarEntidad(this);
                // Asegura que la posición actual sea exactamente la posición objetivo
                TerrenoActual = terrenoDestino;
                InstanciaPersonaje.transform.position = terrenoDestino.POSICIONTRIDIMENSIONAL + new Vector3(0, 0.6f, 0);
                TERRENOACTUAL.AgregarEntidad(this);
                TERRENOACTUAL.CambiarEstado(new Ocupado());
                ReducirEnergiaActual(ConfiguracionGeneral.CostoMinimoDeAccion);
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
        else SetEnergiaActual(EnergiaActual += valor);
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

            return SetEnergiaActual(ENERGIAMAX);
        }
    }
    public bool ReducirEnergiaActual(int valor)
    {
        if ((EnergiaActual - valor) >= 0)
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
        var vidaActual = VidaActual -= valor;
        if (SetVidaActual(vidaActual))
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

    public void EjecutarAccion(IAccionCombate accion, ICombate objetivo)
    {
        accion.EjecutarAccion(this, objetivo);
    }

  
}



