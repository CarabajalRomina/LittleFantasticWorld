using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.habitat;
using Assets.scrips.modelo.Entidad;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public Tabla tblEntidad;

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


    private void Cancelar() 
    {
        BorrarForm();
    }
    private void BorrarForm() 
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
    private void Crear()
    {
        CntPersonaje.CrearEntidad(
            txtNombre.text,
            CntPersonaje.REINOS[ddReino.value - 1] ,
            CntPersonaje.DIETAS[ddDieta.value -1],
            CntHabitat.HABITATS[ddHabitats.value - 1],
            int.Parse(txtEnergiaMax.text),
            int.Parse(txtVidaMax.text),
            int.Parse(txtPuntosAtaque.text),
            int.Parse(txtPuntosDefensa.text),
            int.Parse(txtRangoDeAtaque.text)
            );
        BorrarForm();
        foreach (var personaje in CntPersonaje.PERSONAJES)
        {
            Debug.Log(personaje.ToString());
        }
        tblEntidad.CargarTabla<Entidad>(CntPersonaje.PERSONAJES);
    }

    private void Eliminar() { }
    private void Editar() { }
    public void btnNuevoClickeado()
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



