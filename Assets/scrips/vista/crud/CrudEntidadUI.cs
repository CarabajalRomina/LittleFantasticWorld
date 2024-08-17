using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.habitat;
using Assets.scrips.modelo.Entidad;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.scrips;
using System;



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
    public TextMeshProUGUI lblAvisoNombre;
    public TextMeshProUGUI lblAviso;

    #endregion

    public Tabla tblEntidad;
    public GameObject pnlForm;
    public GameObject pnlBtnCrud;

    void Start()
    {
        CargarDropDowns();
        DeshabilitarOHabilitarElementosPanel(pnlForm);
    }
   
    public void Crear()
    {
        if (NoHayCamposVacios())
        {
            lblAviso.text = "";
            if (!CntPersonaje.NOMBRESUTILIZADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
            {
                lblAvisoNombre.text = "";
                CntPersonaje.CrearEntidad(
                    ValidacionForm.NormalizarCadena(txtNombre.text),
                    CntPersonaje.REINOS[ddReino.value - 1],
                    CntPersonaje.DIETAS[ddDieta.value - 1],
                    CntHabitat.HABITATS[ddHabitats.value - 1],
                    int.Parse(txtEnergiaMax.text),
                    int.Parse(txtVidaMax.text),
                    int.Parse(txtPuntosAtaque.text),
                    int.Parse(txtPuntosDefensa.text),
                    int.Parse(txtRangoDeAtaque.text)
                    );
                foreach (var personaje in CntPersonaje.PERSONAJES)
                {
                    Debug.Log(personaje.ToString());
                }

                tblEntidad.CargarTabla<Entidad>(CntPersonaje.PERSONAJES);
                BorrarForm();
                DeshabilitarOHabilitarElementosPanel(pnlForm);
                DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
            }
            else { lblAvisoNombre.text = "Ya existe ese personaje, elija otro"; }
        
        }
        else
        {
            lblAviso.text = "Seleccione o complete todos los campos";

        }
    }
       
    public void Eliminar() 
    {
 
    }
    public void Editar() { }
    public void btnNuevoClickeado()
    {
        DeshabilitarOHabilitarElementosPanel(pnlForm);
        DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
    }
    public void Cancelar()
    {
        BorrarForm();
        DeshabilitarOHabilitarElementosPanel(pnlForm);
        DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
    }
    public void BorrarForm()
    {
        txtId.text = "";
        txtNombre.text = "";
        ddReino.value = -1;
        ddHabitats.value = -1;
        ddDieta.value = -1;
        txtVidaMax.text = "";
        txtEnergiaMax.text = "";
        txtPuntosAtaque.text = "";
        txtPuntosDefensa.text = "";
        txtRangoDeAtaque.text = "";
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
    void DeshabilitarOHabilitarElementosPanel(GameObject panel)
    {
        foreach (var selectable in panel.GetComponentsInChildren<Selectable>())
        {
            selectable.interactable = !selectable.interactable;
        }
    }
    private bool NoHayCamposVacios()
    {
        TMP_InputField[] inputFields = pnlForm.GetComponentsInChildren<TMP_InputField>();

        for(var i = 1;i< inputFields.Length; i ++)
        {
            if (ValidacionForm.EstaVacioElInput(inputFields[i].text))
            {
                return false;
            }             
        }
        return true;
    }
}



