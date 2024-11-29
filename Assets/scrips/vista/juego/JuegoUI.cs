using Assets.scrips.Controllers;
using Assets.scrips.Controllers.juego;
using Assets.scrips.Controllers.jugador;
using Assets.scrips.interfaces.interactuable;
using Assets.scrips.modelo.entidad;
using Assets.scrips.vista.DatosPersonajeUi;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JuegoUI : SingletonMonoBehaviour<JuegoUI>
{
    #region COMPONENETES UI
    public GameObject DatosInteractuablesPrefab;
    public GameObject DatosEnemigosPrefab;

    public GameObject PnlDatosTerreno;
    public GameObject PnlDatosPersonaje;
    public GameObject PnlInventario;

    public Slider BarraVidaPersonaje;
    public Slider BarraEnergiaPersonaje;
    public Slider BarraPuntosAtaque;
    public Slider BarraPuntosDefensa;
    public GameObject ImgPersonaje;

    public TextMeshProUGUI txtVidaPersonaje;
    public TextMeshProUGUI txtEnergiaPersonaje;
    public TextMeshProUGUI txtPuntosAtaquePersonaje;
    public TextMeshProUGUI txtPuntosDefensa;
    public TextMeshProUGUI txtNombrePersonaje;

    #endregion

    JuegoController CntJuego;
    JugadorController CntJugador;
    ControllerMovimiento CntMovimiento;
    public DatosPersonajeUI DatosPersonaje;

   

    protected override void Awake()
    {
        // Llamamos a la implementación de Awake en el Singleton para que se encargue de la instancia
        base.Awake();

        CntMovimiento = ControllerMovimiento.Instancia;
        CntJuego = JuegoController.Instancia;
        CntJugador = JugadorController.GetInstancia;
    }

    private void OnEnable()
    {
        // Nos suscribimos al evento de la clase PersonajeController
        if (CntMovimiento != null && CntJugador.PLAYER.PERSONAJESELECCIONADO != null)
        {
            CntJugador.PLAYER.PERSONAJESELECCIONADO.OnMovimientoCompletado += ActualizarDatosPersonajeYterreno;

        }
        if (CntJuego != null)
        {

           // CntJuego.OnPersonajeInstanciado += MostrarDatosPersonajeSeleccionado;

        }
    }

    private void OnDisable()
    {
        // Nos desuscribimos del evento para evitar fugas de memoria
        if (CntMovimiento != null )
        {
            //CntJuego.PERSONAJESELECCIONADO.OnMovimientoCompletado -= MostrarDatosDeTerreno;
        }
        if(CntJuego != null)
        {
            //CntJuego.OnPersonajeInstanciado -= MostrarDatosPersonajeSeleccionado;

        }
    }

 
    public void Start()
    {
       MostrarDatosPersonajeSeleccionado();
    }

    public void MostrarDatosDeTerreno(Terreno terreno)
    {
        if (terreno.ESTADO is Ocupado)
        {
            if (PnlDatosTerreno != null)
            {
                EliminarDatosTerreno();
                MostrarInteractuablesEnTerreno(terreno);
                MostrarEnemigosEnTerreno(terreno);
            }
        }
    }

    public void ActualizarDatosPersonajeYterreno(Terreno terreno)
    {
        MostrarDatosDeTerreno(terreno);
        MostrarDatosPersonajeSeleccionado();

    }

    private void MostrarInteractuablesEnTerreno(Terreno terrenoActual)
    {
        if (terrenoActual.INTERACTUABLES.Count > 0)
        {
            if (DatosInteractuablesPrefab != null)
            {
                foreach (var interactuable in terrenoActual.INTERACTUABLES)
                {
                    GameObject nuevaTarjeta = Instantiate(DatosInteractuablesPrefab, PnlDatosTerreno.transform); ;

                    TextMeshProUGUI nombreItemText = nuevaTarjeta.GetComponentInChildren<TextMeshProUGUI>();
                    if (nombreItemText != null)
                    {
                        nombreItemText.text = interactuable.GetNombre();
                    }
                    Button[] btnUsar = nuevaTarjeta.GetComponentsInChildren<Button>();
                    if (btnUsar != null)
                    {
                        btnUsar[1].onClick.AddListener(() => InteractuarConObjeto(interactuable,nuevaTarjeta));
                    }
                }
            }
        }
    }

    private void MostrarEnemigosEnTerreno(Terreno terreno)
    {
        var enemigos = terreno.GetEnemigos();

        if (enemigos.Count > 0)
        {
            if (DatosEnemigosPrefab != null)
            {
                foreach (var enemigo in enemigos)
                {
                    GameObject nuevaTarjeta = Instantiate(DatosEnemigosPrefab, PnlDatosTerreno.transform);

                    // TextMeshProUGUI nombreEnemigoText = nuevaTarjeta.GetComponentInChildren<TextMeshProUGUI>();
                    TextMeshProUGUI[] datosText = nuevaTarjeta.GetComponentsInChildren<TextMeshProUGUI>();

                    Slider sliderVida = nuevaTarjeta.GetComponentInChildren<Slider>();
                    if (sliderVida != null)
                    {
                        sliderVida.maxValue = enemigo.VIDAMAX;
                        sliderVida.value = enemigo.VIDAACTUAL;

                    }
                    if (datosText != null)
                    {
                        datosText[0].text = enemigo.NOMBRE;
                    }
                    datosText[1].text = $"{enemigo.VIDAACTUAL}/{enemigo.VIDAMAX}";

                }

            }
        }
    }

    private void MostrarDatosPersonajeSeleccionado()
    {
        DatosPersonaje.CargarDatosPersonaje(CntJugador.PLAYER.PERSONAJESELECCIONADO);
    }

    private void EliminarDatosTerreno()
    {
        if (PnlDatosTerreno.transform.childCount > 0)
        {
            foreach (Transform child in PnlDatosTerreno.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void InteractuarConObjeto(IInteractuable interactuable, GameObject tarjetaPrefab)
    {
        if (CntJuego.InteractuarConObjeto(interactuable))
        {
            Debug.Log($"puede interactuar con el {interactuable.GetNombre()}");
            Destroy(tarjetaPrefab);
        }
        else
        {
            Debug.Log($"No se puede interactuar con el {interactuable.GetNombre()}");

        }
    }


}
