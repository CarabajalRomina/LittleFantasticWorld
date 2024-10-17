using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.habitat;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.scrips;
using System.Linq;
using Assets.scrips.Controllers.reinos;
using Assets.scrips.modelo.entidad;



public class CrudPersonajeUI : MonoBehaviour
{
    EntidadController CntEntidad; 
    HabitatController CntHabitat = HabitatController.GetInstancia;
    ReinoController CntReino = ReinoController.GetInstancia;

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
        CntEntidad = EntidadController.Instancia;
        CargarDropDowns();
        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
    }
   
    public void Crear()
    {
        if (Utilidades.NoHayCamposVacios(pnlForm))
        {
            lblAviso.text = "";
            if (!CntEntidad.NOMBRESSELECCIONADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
            {
                lblAvisoNombre.text = "";
                if (CntEntidad.CrearPersonaje(
                    ValidacionForm.NormalizarCadena(txtNombre.text),
                    CntReino.REINOS[ddReino.value - 1],
                    CntEntidad.DIETAS[ddDieta.value - 1],
                    CntHabitat.HABITATS[ddHabitats.value - 1],
                    int.Parse(txtEnergiaMax.text),
                    int.Parse(txtVidaMax.text),
                    int.Parse(txtPuntosAtaque.text),
                    int.Parse(txtPuntosDefensa.text),
                    int.Parse(txtRangoDeAtaque.text)
                    ))
                {
                    lblAviso.text = "";
                    if(CntEntidad.GetPersonajes() != null)
                    {
                        tblEntidad.CargarTabla<Personaje>(CntEntidad.GetPersonajes());
                    }                 
                    BorrarForm();
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                    lblAviso.text = "Se creo el personaje correctamente";
                    Debug.Log(CntEntidad.ENTIDADES[0].ToString());
                }else { lblAviso.text = "No se pudo crear el personaje"; }

            }else { lblAvisoNombre.text = "Ya existe ese nombre"; } 
            
        }else{ lblAviso.text = "Seleccione o complete todos los campos";}
    }
       
    public void Eliminar() 
    {
        lblAviso.text = "";
        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
        if(tblEntidad.Filas.Count > 0 )
        {
            if(tblEntidad.FilasSeleccionadas.Count > 0 )
            {
                lblAviso.text = "";
                foreach (var fila in tblEntidad.FilasSeleccionadas)
                {
                    if (CntEntidad.EliminarEntidad((Entidad)fila.OBJETO))
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
                Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);

            }else{ lblAviso.text = "ELIJA UN PERSONAJE PARA ELIMINAR";}

        }else{ lblAviso.text = "No hay personajes cargados para eliminar"; }       
    }

    public void Editar()
    {
        if (tblEntidad.Filas.Count > 0)
        {
            if (tblEntidad.FilasSeleccionadas.Count > 0)
            {
                Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                Utilidades.ActivarODesactivarUnBtn(btnCrear);
                Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                lblAviso.text = "";
                BorrarForm();
                CargarFormDatosObj((Personaje)tblEntidad.FilasSeleccionadas.First().OBJETO);
                tblEntidad.HabilitarODeshabilitarInteractividadTabla();

            }else { lblAviso.text = "ELIJA UN PERSONAJE PARA EDITAR"; }

        }else{ lblAviso.text = "No hay personajes cargados para editar"; }
    }  
            
    public void Actualizar()
    {
        if (Utilidades.NoHayCamposVacios(pnlForm))
        {
            lblAviso.text = "";
            Personaje personaje = (Personaje)tblEntidad.FilasSeleccionadas.First().OBJETO;

            if (txtNombre.text == personaje.NOMBRE)
            {
                lblAvisoNombre.text = "";
                if (CntEntidad.EditarPersonaje(
                    personaje,
                    ValidacionForm.NormalizarCadena(txtNombre.text),
                    CntReino.REINOS[ddReino.value - 1],
                    CntEntidad.DIETAS[ddDieta.value - 1],
                    CntHabitat.HABITATS[ddHabitats.value - 1],
                    int.Parse(txtEnergiaMax.text),
                    int.Parse(txtVidaMax.text),
                    int.Parse(txtPuntosAtaque.text),
                    int.Parse(txtPuntosDefensa.text),
                    int.Parse(txtRangoDeAtaque.text)
                    ))
                {
                    tblEntidad.ActualizarFila<Personaje>(tblEntidad.FilasSeleccionadas.First(), personaje);
                    lblAviso.text = "";
                    BorrarForm();
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                    Utilidades.ActivarODesactivarUnBtn(btnCrear);
                    Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                    btnActualizar.interactable = true;
                    tblEntidad.HabilitarODeshabilitarInteractividadTabla();
                    lblAviso.text = "Se creo el personaje correctamente";

                }else { lblAviso.text = "No se pudo crear el personaje"; }

            }else
            {
                if (!CntEntidad.NOMBRESSELECCIONADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
                {

                    lblAvisoNombre.text = "";
                    if (CntEntidad.EditarPersonaje(
                        (Personaje)tblEntidad.FilasSeleccionadas.First().OBJETO,
                        ValidacionForm.NormalizarCadena(txtNombre.text),
                        CntReino.REINOS[ddReino.value - 1],
                        CntEntidad.DIETAS[ddDieta.value - 1],
                        CntHabitat.HABITATS[ddHabitats.value - 1],
                        int.Parse(txtEnergiaMax.text),
                        int.Parse(txtVidaMax.text),
                        int.Parse(txtPuntosAtaque.text),
                        int.Parse(txtPuntosDefensa.text),
                        int.Parse(txtRangoDeAtaque.text)
                        ))
                    {
                        lblAviso.text = "";
                        tblEntidad.ActualizarFila<Personaje>(tblEntidad.FilasSeleccionadas.First(), personaje);
                        BorrarForm();
                        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                        Utilidades.ActivarODesactivarUnBtn(btnCrear);
                        Utilidades.ActivarODesactivarUnBtn(btnActualizar);
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
        ddReino.value = CntReino.REINOS.IndexOf(personaje.REINO) + 1;
        ddDieta.value = CntEntidad.DIETAS.IndexOf(personaje.DIETA) + 1;
        ddHabitats.value = CntHabitat.HABITATS.IndexOf(personaje.HABITATS) + 1;
        txtEnergiaMax.text = personaje.ENERGIAMAX.ToString();
        txtVidaMax.text = personaje.VIDAMAX.ToString();
        txtPuntosAtaque.text = personaje.PUNTOSATAQUE.ToString();
        txtPuntosDefensa.text = personaje.PUNTOSDEFENSA.ToString();
        txtRangoDeAtaque.text = personaje.RANGOATAQUE.ToString();
    }
    public void btnNuevoClickeado()
    {
        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
    }
    public void Cancelar()
    {
        BorrarForm();
        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
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
        Utilidades.CargarOpcionesEnDropDowns(CntEntidad.DIETAS, ddDieta);
        Utilidades.CargarOpcionesEnDropDowns(CntHabitat.HABITATS, ddHabitats);
        Utilidades.CargarOpcionesEnDropDowns(CntReino.REINOS, ddReino);
    }
   
   
 

    

}



