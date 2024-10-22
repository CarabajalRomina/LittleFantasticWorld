using Assets.scrips;
using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.habitat;
using Assets.scrips.Controllers.reinos;
using Assets.scrips.modelo.entidad;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrudEnemigoUI : MonoBehaviour
{
    EntidadController CntEntidad;
    HabitatController CntHabitat = HabitatController.GetInstancia;
    ReinoController CntReino = ReinoController.GetInstancia;


    #region ComponentesForm
    public TMP_InputField txtId;
    public TMP_InputField txtNombre;
    public TMP_Dropdown ddReino;
    public TMP_Dropdown ddHabitats;
    public TMP_InputField txtVidaMax;
    public TMP_InputField txtPuntosAtaque;
    public TMP_InputField txtPuntosDefensa;
    public TextMeshProUGUI lblAvisoNombre;
    public TextMeshProUGUI lblAviso;
    #endregion

    public Tabla tblEnemigos;
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
        if (CntEntidad.GetPersonajes() != null)
        {
            ActualizarTabla();
        }
    }

    public void Crear()
    {
        if (Utilidades.NoHayCamposVacios(pnlForm))
        {
            lblAviso.text = "";
            if (!CntEntidad.NOMBRESSELECCIONADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
            {
                lblAvisoNombre.text = "";
                if (CntEntidad.CrearEnemigo(
                    ValidacionForm.NormalizarCadena(txtNombre.text),
                    CntReino.REINOS[ddReino.value - 1],
                    CntHabitat.HABITATS[ddHabitats.value - 1],
                    int.Parse(txtVidaMax.text),
                    int.Parse(txtPuntosAtaque.text),
                    int.Parse(txtPuntosDefensa.text)
                    ))
                {
                    lblAviso.text = "";
                    if (CntEntidad.GetPersonajes() != null)
                    {
                        ActualizarTabla();
                    }
                    BorrarForm();
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                    lblAviso.text = "Se creo el enemigo correctamente";
                    Debug.Log(CntEntidad.ENTIDADES[0].ToString());
                }
                else { lblAviso.text = "No se pudo crear el enemigo"; }

            }
            else { lblAvisoNombre.text = "Ya existe ese nombre"; }

        }
        else { lblAviso.text = "Seleccione o complete todos los campos"; }
    }

    private void ActualizarTabla()
    {
        tblEnemigos.ClearTable();
        tblEnemigos.CargarTabla<Enemigo>(CntEntidad.GetEnemigos());
    }

    public void Eliminar()
    {
        lblAviso.text = "";
        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
        if (tblEnemigos.Filas.Count > 0)
        {
            if (tblEnemigos.FilasSeleccionadas.Count > 0)
            {
                lblAviso.text = "";
                foreach (var fila in tblEnemigos.FilasSeleccionadas)
                {
                    if (CntEntidad.EliminarEntidad((Entidad)fila.OBJETO))
                    {
                        lblAviso.text = "Se elimino al enemigo correctamente";
                        tblEnemigos.QuitarFila(fila);
                    }
                    else
                    {
                        Debug.Log("no se pudo eliminar al enemigo");
                    }
                }
                tblEnemigos.FilasSeleccionadas.Clear();
                Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);

            }
            else { lblAviso.text = "ELIJA UN ENEMIGO PARA ELIMINAR"; }

        }
        else { lblAviso.text = "No hay enemigos cargados para editar"; }
    }

    public void Editar()
    {
        if (tblEnemigos.Filas.Count > 0)
        {
            if (tblEnemigos.FilasSeleccionadas.Count > 0)
            {
                Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                Utilidades.ActivarODesactivarUnBtn(btnCrear);
                Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                lblAviso.text = "";
                BorrarForm();
                CargarFormDatosObj((Enemigo)tblEnemigos.FilasSeleccionadas.First().OBJETO);
                tblEnemigos.HabilitarODeshabilitarInteractividadTabla();

            }
            else { lblAviso.text = "ELIJA UN ENEMIGO PARA EDITAR"; }

        }
        else { lblAviso.text = "No hay enemigo cargados para editar"; }
    }

    public void Actualizar()
    {
        if (Utilidades.NoHayCamposVacios(pnlForm))
        {
            lblAviso.text = "";
            Enemigo enemigo = (Enemigo)tblEnemigos.FilasSeleccionadas.First().OBJETO;

            if (txtNombre.text == enemigo.NOMBRE)
            {
                lblAvisoNombre.text = "";
                if (CntEntidad.EditarEnemigo(
                    enemigo,
                    ValidacionForm.NormalizarCadena(txtNombre.text),
                    CntReino.REINOS[ddReino.value - 1],
                    CntHabitat.HABITATS[ddHabitats.value - 1],
                    int.Parse(txtVidaMax.text),
                    int.Parse(txtPuntosAtaque.text),
                    int.Parse(txtPuntosDefensa.text)
                    ))
                {
                    tblEnemigos.ActualizarFila<Enemigo>(tblEnemigos.FilasSeleccionadas.First(), enemigo);
                    lblAviso.text = "";
                    BorrarForm();
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                    Utilidades.ActivarODesactivarUnBtn(btnCrear);
                    Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                    btnActualizar.interactable = true;
                    tblEnemigos.HabilitarODeshabilitarInteractividadTabla();
                    lblAviso.text = "Se creo el personaje correctamente";

                }
                else { lblAviso.text = "No se pudo crear el personaje"; }

            }
            else
            {
                if (!CntEntidad.NOMBRESSELECCIONADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
                {

                    lblAvisoNombre.text = "";
                    if (CntEntidad.EditarEnemigo(
                        (Enemigo)tblEnemigos.FilasSeleccionadas.First().OBJETO,
                        ValidacionForm.NormalizarCadena(txtNombre.text),
                        CntReino.REINOS[ddReino.value - 1],
                        CntHabitat.HABITATS[ddHabitats.value - 1],
                        int.Parse(txtVidaMax.text),
                        int.Parse(txtPuntosAtaque.text),
                        int.Parse(txtPuntosDefensa.text)
                        ))
                    {
                        lblAviso.text = "";
                        tblEnemigos.ActualizarFila<Enemigo>(tblEnemigos.FilasSeleccionadas.First(), enemigo);
                        BorrarForm();
                        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                        Utilidades.ActivarODesactivarUnBtn(btnCrear);
                        Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                        tblEnemigos.HabilitarODeshabilitarInteractividadTabla();
                        lblAviso.text = "Se creo el enemigo correctamente";

                    }
                    else { lblAviso.text = "No se pudo crear el enemigo"; }

                }
                else { lblAvisoNombre.text = "Ya existe ese nombre"; }
            }
        }
        else { lblAviso.text = "Seleccione o complete todos los campos"; }
    }

    private void CargarDropDowns()
    {
        Utilidades.CargarOpcionesEnDropDowns(CntHabitat.HABITATS, ddHabitats);
        Utilidades.CargarOpcionesEnDropDowns(CntReino.REINOS, ddReino);
    }
    private void CargarFormDatosObj(Enemigo enemigo)
    {
        txtId.text = enemigo.ID.ToString();
        txtNombre.text = enemigo.NOMBRE;
        ddReino.value = CntReino.REINOS.IndexOf(enemigo.REINO) + 1;
        ddHabitats.value = CntHabitat.HABITATS.IndexOf(enemigo.HABITATS) + 1;
        txtVidaMax.text = enemigo.VIDAMAX.ToString();
        txtPuntosAtaque.text = enemigo.PUNTOSATAQUE.ToString();
        txtPuntosDefensa.text = enemigo.PUNTOSDEFENSA.ToString();
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
        txtVidaMax.text = "";
        txtPuntosAtaque.text = "";
        txtPuntosDefensa.text = "";
        lblAviso.text = "";
        lblAvisoNombre.text = "";
    }
}
