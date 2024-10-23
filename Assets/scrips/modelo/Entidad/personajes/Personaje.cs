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
    int EnergiaActual;
    int EnergiaMax;
    int VidaActual;
    int VidaMax;
    int PuntosAtaque;
    int PuntosDefensa;
    int RangoAtaque;
    private bool enMovimiento = false; // Estado del movimiento
    Inventario InventarioActual;



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
        PersonajePrefab = Resources.Load<GameObject>("personajesPref/Golem");
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
            if (value > 0 || value <= VidaMax)
            {
                VidaActual = value;
            }
            else
            {
                if(value > VidaMax)
                {
                    VidaActual = VidaMax;
                }
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

    public Inventario INVENTARIOACTUAL
    {
        get { return InventarioActual; }
        set { InventarioActual = value; }
    }

    #endregion

    #region METODOS COMBATE
    public int Atacar()
    {
        var dado = Dado.TirarDado();
        return PUNTOSATAQUE + dado;
    }

    public int Defender()
    {
        var dado = Dado.TirarDado();
        return PUNTOSDEFENSA + dado;
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
        if (HABITATS.PuedoMoverme(terrenoDestino.TIPOSUBTERRENO.TIPOTERRENO))
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
                    // Asegura que la posición actual sea exactamente la posición objetivo
                    TerrenoActual = terrenoDestino;
                    InstanciaPersonaje.transform.position = terrenoDestino.POSICIONTRIDIMENSIONAL;
                    TERRENOACTUAL.AgregarEntidad(this);
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
        else
        {
            Debug.Log("no puede ir a un habitat a la que no esta adaptado...");
        }
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

    public void AumentarPuntosAtaque(int valor)
    {
        PUNTOSATAQUE += valor;
    }
    public void ReducirPuntosAtaque(int valor)
    {
        PUNTOSATAQUE -= valor;
    }

    public void AumentarPuntosDefensa(int valor)
    {
        PUNTOSDEFENSA += valor;
    }
    public void ReducirPuntosDefensa(int valor)
    {
        PUNTOSDEFENSA -= valor;
    }

    public void UsarItem(Item item)
    {
        item.Interactuar(this);
    }

    public void Dormir()
    {
        ActualizarEnergia(ENERGIAMAX);
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
            $" Rango de ataque: {RANGOATAQUE}," +
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
            ENERGIAMAX.ToString(),
            VIDAMAX.ToString(),
            PUNTOSATAQUE.ToString(),
            PUNTOSDEFENSA.ToString(),
            RangoAtaque.ToString()
            };
    }

}



