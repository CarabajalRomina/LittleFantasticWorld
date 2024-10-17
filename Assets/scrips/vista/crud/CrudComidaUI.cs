using Assets.scrips;
using Assets.scrips.Controllers.comida;
using Assets.scrips.Controllers.entidad;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrudComidaUI : MonoBehaviour
{
    EntidadController CntEntidad;
    ComidaController CntComida = ComidaController.GetInstancia;


    #region ComponentesForm
    public TMP_InputField txtId;
    public TMP_InputField txtNombre;
    public TMP_Dropdown ddDieta;
    public TMP_InputField txtCalorias;
    public TextMeshProUGUI lblAvisoNombre;
    public TextMeshProUGUI lblAviso;
    #endregion

    public Tabla tblComidas;
    public GameObject pnlForm;
    public GameObject pnlBtnCrud;

    #region BOTONES
    public Button btnCrear;
    public Button btnActualizar;
    #endregion


    void Start()
    {
        CntEntidad = EntidadController.Instancia;
        CargarDropDown();
        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
    }


    public void CrearComida()
    {
        if (Utilidades.NoHayCamposVacios(pnlForm))
        {
            lblAviso.text = "";
            if (!CntComida.NOMBRECOMIDASELECCIONADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
            {
                lblAvisoNombre.text = "";
                if (CntComida.CrearComida(ValidacionForm.NormalizarCadena(txtNombre.text), CntEntidad.DIETAS[ddDieta.value - 1], int.Parse(txtCalorias.text)))
                {
                    lblAviso.text = "";
                    if(CntComida.COMIDAS.Count > 0)
                    {
                        tblComidas.CargarTabla<Comida>(CntComida.COMIDAS);
                    }
                    BorrarForm();
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                    lblAviso.text = "Se creo la comida correctamente";
                    Debug.Log(CntComida.COMIDAS[0].ToString());
                }
                else { lblAviso.text = "No se pudo crear la comida"; }
            }
            else { lblAviso.text = "Ya existe ese nombre de comida"; }
        }
        else { lblAviso.text = "Seleccione o complete todos los campos"; }

    }

    public void Eliminar()
    {
        lblAviso.text = "";
        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
        if (tblComidas.Filas.Count > 0)
        {
            if (tblComidas.FilasSeleccionadas.Count > 0)
            {
                lblAviso.text = "";
                foreach (var fila in tblComidas.FilasSeleccionadas)
                {
                    if (CntComida.EliminarComida((Comida)fila.OBJETO))
                    {
                        lblAviso.text = "Se elimino la comida correctamente";
                        tblComidas.QuitarFila(fila);
                    }
                    else
                    {
                        Debug.Log("no se pudo eliminar la comida");
                    }
                }
                tblComidas.FilasSeleccionadas.Clear();
                Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
            }
            else { lblAviso.text = "ELIJA UNA COMIDA PARA ELIMINAR"; }

        }
        else { lblAviso.text = "No hay comidas cargados para eliminar"; }
    }

    public void EditarComida()
    {
        if (tblComidas.Filas.Count > 0)
        {
            if (tblComidas.FilasSeleccionadas.Count > 0)
            {
                Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                Utilidades.ActivarODesactivarUnBtn(btnCrear);
                Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                lblAviso.text = "";
                BorrarForm();
                CargarFormDatosObj((Comida)tblComidas.FilasSeleccionadas.First().OBJETO);
                tblComidas.HabilitarODeshabilitarInteractividadTabla();

            }
            else { lblAviso.text = "ELIJA UNA COMIDA PARA EDITAR"; }

        }
        else { lblAviso.text = "No hay comidas cargados para editar"; }
    }

    public void Actualizar()
    {
        if (Utilidades.NoHayCamposVacios(pnlForm))
        {
            lblAviso.text = "";
            Comida comida = (Comida)tblComidas.FilasSeleccionadas.First().OBJETO;

            if (txtNombre.text == comida.NOMBRE)
            {
                lblAvisoNombre.text = "";
                if (CntComida.EditarComida(
                    comida,
                    ValidacionForm.NormalizarCadena(txtNombre.text),
                    CntEntidad.DIETAS[ddDieta.value - 1],
                    int.Parse(txtCalorias.text)
                    ))
                {
                    tblComidas.ActualizarFila<Comida>(tblComidas.FilasSeleccionadas.First(), comida);
                    lblAviso.text = "";
                    BorrarForm();
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                    Utilidades.ActivarODesactivarUnBtn(btnCrear);
                    Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                    btnActualizar.interactable = true;
                    tblComidas.HabilitarODeshabilitarInteractividadTabla();
                    lblAviso.text = "Se actualizo la comida correctamente";

                }
                else { lblAviso.text = "No se pudo actualizar la comida"; }

            }
            else
            {
                if (!CntComida.NOMBRECOMIDASELECCIONADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
                {
                    lblAvisoNombre.text = "";
                    if (CntComida.EditarComida(
                        (Comida)tblComidas.FilasSeleccionadas.First().OBJETO,
                        ValidacionForm.NormalizarCadena(txtNombre.text),
                        CntEntidad.DIETAS[ddDieta.value - 1],
                        int.Parse(txtCalorias.text)))
                    {
                        lblAviso.text = "";
                        tblComidas.ActualizarFila<Comida>(tblComidas.FilasSeleccionadas.First(), comida);
                        BorrarForm();
                        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                        Utilidades.ActivarODesactivarUnBtn(btnCrear);
                        Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                        tblComidas.HabilitarODeshabilitarInteractividadTabla();
                        lblAviso.text = "Se creo la comida correctamente";

                    }
                    else { lblAviso.text = "No se pudo crear la comida"; }

                }
                else { lblAvisoNombre.text = "Ya existe ese nombre"; }
            }
        }
        else { lblAviso.text = "Seleccione o complete todos los campos"; }
    }
    private void CargarFormDatosObj(Comida comida)
    {
        txtId.text = comida.ID.ToString();
        txtNombre.text = comida.NOMBRE;
        ddDieta.value = CntEntidad.DIETAS.IndexOf(comida.TIPODIETA) + 1;
        txtCalorias.text = comida.CALORIAS.ToString();
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
        ddDieta.value = -1;
        txtCalorias.text = "";
        lblAviso.text = "";
        lblAvisoNombre.text = "";
    }
    private void CargarDropDown()
    {
        Utilidades.CargarOpcionesEnDropDowns(CntEntidad.DIETAS, ddDieta);
    }
}
