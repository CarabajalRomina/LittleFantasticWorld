using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.habitat;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CrudEntidadUI : MonoBehaviour
{
    PersonajeController CntPersonaje = PersonajeController.GetInstancia;
    HabitatController CntHabitat = HabitatController.GetInstancia;

    #region ComponentesForm
    public TMP_InputField txtId;
    public TMP_InputField txtNombre;
    public TMP_Dropdown ddReino;
    public TMP_Dropdown ddHabitats;
    public TMP_Dropdown ddDieta;
    public TMP_InputField txtVidaMax;
    public TMP_InputField txtEnergiaMax;
    public TMP_InputField txtPuntosAtaque;
    public TMP_InputField txtPuntosDefensa;
    public TMP_InputField txtRangoDeAtaque;
    #endregion
    #region Botones
    public Button btnNuevo;
    public Button btnBorrar;
    public Button btnCrear;
    public Button btnEditar;
    public Button btnEliminar;
    public Button btnCancelar;
    #endregion

    void Start()
    {
        CargarDropDowns();
        AsignarListenerBtn();
    }

    void AsignarListenerBtn()
    {
        if (btnNuevo != null && btnEditar != null && btnEliminar != null && btnCrear != null && btnBorrar != null && btnCancelar != null)
        {
            btnNuevo.onClick.AddListener(btnNuevoClickeado);
            btnEditar.onClick.AddListener(Editar);
            btnEliminar.onClick.AddListener(Eliminar);
            btnCrear.onClick.AddListener(Crear);
            btnBorrar.onClick.AddListener(BorrarForm);
            btnCancelar.onClick.AddListener(Cancelar);
        }
    }


    private void Cancelar() { }
    private void BorrarForm() { }
    private void Crear()
    {
        var g = int.Parse(txtEnergiaMax.text);
        var gu = int.Parse(txtVidaMax.text);
        var go = int.Parse(txtPuntosAtaque.text);
        var gp = int.Parse(txtPuntosDefensa.text);
        var pe = int.Parse(txtRangoDeAtaque.text);

        CntPersonaje.CrearEntidad(
            txtNombre.text,
            CntPersonaje.REINOS[ddReino.value],
            CntPersonaje.DIETAS[ddDieta.value],
            CntHabitat.HABITATS[ddHabitats.value],
            int.Parse(txtEnergiaMax.text),
            int.Parse(txtVidaMax.text),
            int.Parse(txtPuntosAtaque.text),
            int.Parse(txtPuntosDefensa.text),
            int.Parse(txtRangoDeAtaque.text)
            );
        foreach(var personaje in CntPersonaje.PERSONAJES)
        {
            Debug.Log(personaje.ToString());
        }    
    }
    private void Eliminar() { }
    private void Editar() { }
    private void btnNuevoClickeado()
    {

    }



    private void CargarDropDowns()
    {
        CargarOpciones(CntPersonaje.DIETAS, ddDieta);
        CargarOpciones(CntHabitat.HABITATS, ddHabitats);
        CargarOpciones(CntPersonaje.REINOS, ddReino);
    }
    public void CargarOpciones<T>(List<T> opciones, TMP_Dropdown dropdown )
    {
        dropdown.ClearOptions();

        dropdown.options.Add(new TMP_Dropdown.OptionData("-- Seleccione una opcion --"));
        dropdown.value = dropdown.options.Count - 1;

        foreach (var opcion in opciones)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(opcion.ToString()));
        }
    }
}



