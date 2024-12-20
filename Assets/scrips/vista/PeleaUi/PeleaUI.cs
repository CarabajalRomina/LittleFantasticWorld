using Assets.scrips.Controllers.juego;
using Assets.scrips.Controllers.pelea;
using Assets.scrips.Controllers.turno;
using Assets.scrips.interfaces;
using Assets.scrips.interfaces.observadorTurno;
using Assets.scrips.interfaces.pelea;
using Assets.scrips.modelo.entidad;
using Assets.scrips.modelo.pelea;
using Assets.scrips.modelo.pelea.acciones;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PeleaUI : MonoBehaviour
{
    #region COMPONENETES UI

    public Slider BarraVidaPersonaje;
    public Slider BarraVidaEnemigo;

    public TextMeshProUGUI txtVidaPersonaje;
    public TextMeshProUGUI txtVidaEnemigo;

    public TextMeshProUGUI txtNombrePersonaje;
    public TextMeshProUGUI txtNombreEnemigo;

    public Button btnAtacar;
    public Button btnDefender;
    public Button btnHuir;

    public GameObject CartelPeleaFinalizada;

    #endregion

    Vector3 PuntoAparicionPersonaje = new Vector3(850, 412, -584);
    Vector3 PuntoAparicionEnemigo = new Vector3(887, 428, 1794);
    Vector3 RotacionEnemigo = new Vector3(0, 190, 0);
    Vector3 EscalaPersonaje = new Vector3(200, 200, 200);
    GameObject InstanciaEnemigo;
    GameObject InstanciaPersonaje;
    JuegoController CntJuego;
    PeleaController CntPelea;


    // Start is called before the first frame update
    void Start()
    {
        CntJuego = JuegoController.Instancia;
        CntPelea = CntJuego.ObtenerPeleaController();

        btnAtacar.onClick.AddListener(() => EjecutarAccionJugador(new Atacar(), CntPelea.PELEAACTUAL.ENEMIGO));
        btnDefender.onClick.AddListener(() => EjecutarAccionJugador(new Defender(), CntPelea.PELEAACTUAL.ENEMIGO));

        CntPelea.OnTurnoJugador += ActualizarUIParaTurno;
        CntPelea.OnPeleaFinalizada += MostrarDatosPeleaFinalizada;

        InstanciarPersonajesEnEscena();
        CargarDatosUI();
        //btnHuir.onClick.AddListener(AccionHuir);
    }

    

    private void ActualizarUIParaTurno(bool esTurnoJugador)
    {
        btnAtacar.interactable = esTurnoJugador;
        btnDefender.interactable = esTurnoJugador;
        btnHuir.interactable = esTurnoJugador;

        if (esTurnoJugador)
        {
            Debug.Log("Es tu turno. ¡Elige una acción!");
        }
        else
        {
            Debug.Log("Esperando al enemigo...");
        }
        ActualizarUi(CntPelea.PELEAACTUAL.PERSONAJE, CntPelea.PELEAACTUAL.ENEMIGO);

    }

    private void InstanciarPersonajesEnEscena()
    {
        CntPelea.InstanciarPersonajesEnEscena(PuntoAparicionPersonaje, PuntoAparicionEnemigo, RotacionEnemigo, EscalaPersonaje);
    }

    private void CargarDatosUI()
    {
        var personaje = CntPelea.PELEAACTUAL.PERSONAJE;
        var enemigo = CntPelea.PELEAACTUAL.ENEMIGO;
        txtNombrePersonaje.text = personaje.ObtenerNombre();
        txtNombreEnemigo.text = enemigo.ObtenerNombre();

    }

    private void ActualizarUi(ICombate personaje, ICombate enemigo)
    {
        ActualizarDatosUIPersonaje(personaje);
        ActualizarDatosUiEnemigo(enemigo);
    }
    private void ActualizarDatosUIPersonaje(ICombate personaje)
    {
        if (BarraVidaPersonaje != null)
        {
            BarraVidaPersonaje.maxValue = personaje.ObtenerVidaMaxima();
            BarraVidaPersonaje.value = personaje.ObtenerVidaActual();
        }
        txtVidaPersonaje.text = $"{personaje.ObtenerVidaActual()}/{personaje.ObtenerVidaMaxima()}";
        
    }

    private void ActualizarDatosUiEnemigo(ICombate enemigo)
    {
        if (BarraVidaEnemigo != null)
        {
            BarraVidaEnemigo.maxValue = enemigo.ObtenerVidaMaxima();
            BarraVidaEnemigo.value = enemigo.ObtenerVidaActual();
        }
        txtVidaEnemigo.text = $"{enemigo.ObtenerVidaActual()}/{enemigo.ObtenerVidaMaxima()}";
    }

    private void EjecutarAccionJugador(IAccionCombate accion, ICombate objetivo)
    {
        CntPelea.EjecutarAccionJugador(accion, objetivo);
    }

    private void MostrarDatosPeleaFinalizada()
    {
        ActualizarUi(CntPelea.PELEAACTUAL.PERSONAJE, CntPelea.PELEAACTUAL.ENEMIGO);
        CartelPeleaFinalizada.SetActive(true);
        var textoCartel = CartelPeleaFinalizada.GetComponentInChildren<TextMeshProUGUI>();
        textoCartel.text = $"El ganador es {CntPelea.DeterminarGanador().ObtenerNombre()}";

    }
}
