using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.habitat;
using Assets.scrips.modelo.Entidad;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.scrips;
using System.Linq;
using System;



public class CrudEntidadUI : MonoBehaviour
{
    PersonajeController CntPersonaje; 
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

    #region BOTONES
    public Button btnCrear;
    public Button btnActualizar;
    #endregion

    void Start()
    {
        CntPersonaje = PersonajeController.Instancia;
        CargarDropDowns();
        DeshabilitarOHabilitarElementosPanel(pnlForm);
    }
   
    public void Crear()
    {
        if (NoHayCamposVacios())
        {
            lblAviso.text = "";
            if (!CntPersonaje.NOMBRESSELECCIONADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
            {
                lblAvisoNombre.text = "";
                if (CntPersonaje.CrearEntidad(
                    ValidacionForm.NormalizarCadena(txtNombre.text),
                    CntPersonaje.REINOS[ddReino.value - 1],
                    CntPersonaje.DIETAS[ddDieta.value - 1],
                    CntHabitat.HABITATS[ddHabitats.value - 1],
                    int.Parse(txtEnergiaMax.text),
                    int.Parse(txtVidaMax.text),
                    int.Parse(txtPuntosAtaque.text),
                    int.Parse(txtPuntosDefensa.text),
                    int.Parse(txtRangoDeAtaque.text)
                    ))
                {
                    lblAviso.text = "";
                    tblEntidad.CargarTabla<Entidad>(CntPersonaje.PERSONAJES);
                    BorrarForm();
                    DeshabilitarOHabilitarElementosPanel(pnlForm);
                    DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                    lblAviso.text = "Se creo el personaje correctamente";
                    Debug.Log(CntPersonaje.PERSONAJES[0].ToString());
                }else { lblAviso.text = "No se pudo crear el personaje"; }

            }else { lblAvisoNombre.text = "Ya existe ese nombre"; } 
            
        }else{ lblAviso.text = "Seleccione o complete todos los campos";}
    }
       
    public void Eliminar() 
    {
        lblAviso.text = "";
        DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
        if(tblEntidad.Filas.Count > 0 )
        {
            if(tblEntidad.FilasSeleccionadas.Count > 0 )
            {
                lblAviso.text = "";
                foreach (var fila in tblEntidad.FilasSeleccionadas)
                {
                    if (CntPersonaje.Eliminar((Entidad)fila.OBJETO))
                    {
                        lblAviso.text = "Se elimino al personaje correctamente";
                        tblEntidad.QuitarFila(fila);
                    }
                    else
                    {
                        Debug.Log("no se pudo eliminar al personaje");
                    }
                }
                tblEntidad.FilasSeleccionadas.Clear();
                DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);

            }else{ lblAviso.text = "ELIJA UN PERSONAJE PARA EDITAR";}

        }else{ lblAviso.text = "No hay personajes cargados para editar";}       
    }

    public void Editar()
    {
        if (tblEntidad.Filas.Count > 0)
        {
            if (tblEntidad.FilasSeleccionadas.Count > 0)
            {
                DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                DeshabilitarOHabilitarElementosPanel(pnlForm);
                ActivarODesactivarUnBtn(btnCrear);
                ActivarODesactivarUnBtn(btnActualizar);
                lblAviso.text = "";
                BorrarForm();
                CargarFormDatosObj((Personaje)tblEntidad.FilasSeleccionadas.First().OBJETO);
                tblEntidad.HabilitarODeshabilitarInteractividadTabla();

            }else { lblAviso.text = "ELIJA UN PERSONAJE PARA EDITAR"; }

        }else{ lblAviso.text = "No hay personajes cargados para editar"; }
    }  
            
    public void Actualizar()
    {
        if (NoHayCamposVacios())
        {
            lblAviso.text = "";
            Personaje personaje = (Personaje)tblEntidad.FilasSeleccionadas.First().OBJETO;

            if (txtNombre.text == personaje.NOMBRE)
            {
                lblAvisoNombre.text = "";
                if (CntPersonaje.EditarEntidad(
                    personaje,
                    ValidacionForm.NormalizarCadena(txtNombre.text),
                    CntPersonaje.REINOS[ddReino.value - 1],
                    CntPersonaje.DIETAS[ddDieta.value - 1],
                    CntHabitat.HABITATS[ddHabitats.value - 1],
                    int.Parse(txtEnergiaMax.text),
                    int.Parse(txtVidaMax.text),
                    int.Parse(txtPuntosAtaque.text),
                    int.Parse(txtPuntosDefensa.text),
                    int.Parse(txtRangoDeAtaque.text)
                    ))
                {
                    tblEntidad.ActualizarFila<Entidad>(tblEntidad.FilasSeleccionadas.First(), personaje);
                    lblAviso.text = "";
                    BorrarForm();
                    DeshabilitarOHabilitarElementosPanel(pnlForm);
                    DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                    ActivarODesactivarUnBtn(btnCrear);
                    ActivarODesactivarUnBtn(btnActualizar);
                    btnActualizar.interactable = true;
                    tblEntidad.HabilitarODeshabilitarInteractividadTabla();
                    lblAviso.text = "Se creo el personaje correctamente";

                }else { lblAviso.text = "No se pudo crear el personaje"; }

            }else
            {
                if (!CntPersonaje.NOMBRESSELECCIONADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
                {

                    lblAvisoNombre.text = "";
                    if (CntPersonaje.EditarEntidad(
                        (Personaje)tblEntidad.FilasSeleccionadas.First().OBJETO,
                        ValidacionForm.NormalizarCadena(txtNombre.text),
                        CntPersonaje.REINOS[ddReino.value - 1],
                        CntPersonaje.DIETAS[ddDieta.value - 1],
                        CntHabitat.HABITATS[ddHabitats.value - 1],
                        int.Parse(txtEnergiaMax.text),
                        int.Parse(txtVidaMax.text),
                        int.Parse(txtPuntosAtaque.text),
                        int.Parse(txtPuntosDefensa.text),
                        int.Parse(txtRangoDeAtaque.text)
                        ))
                    {
                        lblAviso.text = "";
                        tblEntidad.ActualizarFila<Entidad>(tblEntidad.FilasSeleccionadas.First(), personaje);
                        BorrarForm();
                        DeshabilitarOHabilitarElementosPanel(pnlForm);
                        DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                        ActivarODesactivarUnBtn(btnCrear);
                        ActivarODesactivarUnBtn(btnActualizar);
                        tblEntidad.HabilitarODeshabilitarInteractividadTabla();
                        lblAviso.text = "Se creo el personaje correctamente";

                    }else { lblAviso.text = "No se pudo crear el personaje"; }

                }else { lblAvisoNombre.text = "Ya existe ese nombre"; }
            }      
        }else { lblAviso.text = "Seleccione o complete todos los campos"; }
    }

    private void CargarFormDatosObj(Personaje personaje)
    {
        txtId.text = personaje.ID.ToString();
        txtNombre.text = personaje.NOMBRE;
        ddReino.value = CntPersonaje.REINOS.IndexOf(personaje.REINO) + 1;
        ddDieta.value = CntPersonaje.DIETAS.IndexOf(personaje.DIETA) + 1;
        ddHabitats.value = CntHabitat.HABITATS.IndexOf(personaje.HABITATS) + 1;
        txtEnergiaMax.text = personaje.ENERGIAMAX.ToString();
        txtVidaMax.text = personaje.VIDAMAX.ToString();
        txtPuntosAtaque.text = personaje.PUNTOSATAQUE.ToString();
        txtPuntosDefensa.text = personaje.PUNTOSDEFENSA.ToString();
        txtRangoDeAtaque.text = personaje.RANGOATAQUE.ToString();
    }
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
        lblAviso.text = "";
        lblAvisoNombre.text = "";
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
        lblAviso.text = "";
        lblAvisoNombre.text = "";
    }
    private void CargarDropDowns()
    {
        CargarOpciones(CntPersonaje.DIETAS, ddDieta);
        CargarOpciones(CntHabitat.HABITATS, ddHabitats);
        CargarOpciones(CntPersonaje.REINOS, ddReino);
    }
    private void CargarOpciones<T>(List<T> opciones, TMP_Dropdown dropdown )
    {
        dropdown.ClearOptions();

        dropdown.options.Add(new TMP_Dropdown.OptionData("-- Seleccione una opcion --"));
        dropdown.value = dropdown.options.Count - 1;

        foreach (var opcion in opciones)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(opcion.ToString()));
        }
    }
    private void DeshabilitarOHabilitarElementosPanel(GameObject panel)
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

    private void ActivarODesactivarUnBtn(Button btn)
    {
        btn.gameObject.SetActive(!btn.gameObject.activeInHierarchy);
    }

}



