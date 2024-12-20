using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.juego;
using Assets.scrips.Controllers.jugador;
using Assets.scrips.vista.DatosPersonajeUi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionPersonajeUI : MonoBehaviour
{
    //COMPONENTES UI
    public GameObject PnlPersonajes;
    public GameObject PnlDatosPersonaje;
    public GameObject BasePersonaje;

    //PREFAB
    public GameObject MarcoPersonajePrefab;

    //CONTROLADORAS
    private EntidadController CntEntidad;
    private JuegoController CntJuego;
    private ControllerEscenas CntEscenas;
    JugadorController CntJugador = JugadorController.GetInstancia;

    public DatosPersonajeUI DatosPersonajeUI;

    private Vector3 PuntoAparicionPersonaje;
    private Dictionary<int, GameObject> PersonajesInstanciados = new Dictionary<int, GameObject>();
    private Personaje PersonajeSeleccionado;
    private GameObject InstanciaPersonajeActiva;



    void Start()
    {
        CntEscenas = ControllerEscenas.Instancia;
        CntEntidad = EntidadController.Instancia;
        PuntoAparicionPersonaje = BasePersonaje.transform.position + new Vector3(0, 4.6f, 0);
        MostrarPersonajes();
    }


    private void MostrarPersonajes()
    {
        var personajes = CntEntidad.GetPersonajes();

        if (personajes.Count > 0)
        {
            if (PnlPersonajes != null)
            {
                foreach (var personaje in personajes)
                {
                    
                    if (MarcoPersonajePrefab != null)
                    {
                        GameObject nuevaTarjeta = Instantiate(MarcoPersonajePrefab, PnlPersonajes.transform);
                        Button btnMarco = nuevaTarjeta.GetComponentInChildren<Button>();
                        if (btnMarco != null)
                        {
                            btnMarco.onClick.AddListener(() => MostrarPersonajeSeleccionado(personaje, nuevaTarjeta));
                        }
                    }
                }
            }
        }
    }

    private void MostrarPersonajeSeleccionado(Personaje personaje, GameObject nuevaTarjeta)
    {
        if (BasePersonaje != null)
        {
            if (InstanciaPersonajeActiva != null)
            {
                InstanciaPersonajeActiva.SetActive(false);
            }

            if (PersonajesInstanciados.ContainsKey(personaje.ID))
            {
                PersonajesInstanciados[personaje.ID].SetActive(true);
                InstanciaPersonajeActiva = PersonajesInstanciados[personaje.ID];
            }
            else
            {
                InstanciarPersonajeEnBase(personaje, PuntoAparicionPersonaje);
            }

            DatosPersonajeUI.CargarDatosPersonaje(personaje);
            PersonajeSeleccionado = personaje;
        }
    }

    private void InstanciarPersonajeEnBase(Personaje personaje, Vector3 puntoAparicion)
    {
        var personajePrefab = personaje.PERSONAJEPREFAB.gameObject;
        if (personajePrefab != null)
        {
            var InstanciaPersonaje = ControllerEscenas.Instancia.InstanciarModelo(personajePrefab, puntoAparicion, new Vector3(20f, 20f, 20f), Quaternion.Euler(0, 180, 0));   
            if (InstanciaPersonaje != null)
            {
                PersonajesInstanciados[personaje.ID] = InstanciaPersonaje;
                InstanciaPersonajeActiva = InstanciaPersonaje;
            }
        }
    }


    public void ConfirmarPersonajeSeleccionado()
    {
        if(CntJugador.PLAYER != null)
        {
            CntJugador.PLAYER.PERSONAJESELECCIONADO = PersonajeSeleccionado;
            CntEscenas.CambiarEscena(3);

        }
    }
}


