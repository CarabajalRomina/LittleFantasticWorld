using Assets.scrips.Controllers.entidad;
using Assets.scrips.Controllers.item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scrips.vista.crud
{
    public class CrudItemUI: MonoBehaviour
    {

        ItemController CntItem = ItemController.GetInstancia;

        #region ComponentesForm
        public TMP_InputField txtId;
        public TMP_InputField txtNombre;
        public TMP_Dropdown ddEfecto;
        public TMP_InputField txtDescripcion;
        public TextMeshProUGUI lblAvisoNombre;
        public TextMeshProUGUI lblAviso;
        #endregion

        public Tabla tblItems;
        public GameObject pnlForm;
        public GameObject pnlBtnCrud;

        #region BOTONES
        public Button btnCrear;
        public Button btnActualizar;
        #endregion

        void Start()
        {
            CargarDropDown();
            Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
        }

        public void CrearItem()
        {
            if (Utilidades.NoHayCamposVacios(pnlForm))
            {
                lblAviso.text = "";
                if (!CntItem.NOMBREITEMSELECCIONADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
                {
                    lblAvisoNombre.text = "";
                    if (CntItem.CrearItem(ValidacionForm.NormalizarCadena(txtNombre.text), CntItem.EFECTOSITEM[ddEfecto.value - 1], ValidacionForm.NormalizarCadena(txtDescripcion.text)))
                    {
                        lblAviso.text = "";
                        if (CntItem.ITEMS.Count > 0)
                        {
                            tblItems.CargarTabla<Item>(CntItem.ITEMS);
                        }
                        BorrarForm();
                        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                        lblAviso.text = "Se creo el item correctamente";
                        Debug.Log(CntItem.ITEMS[0].ToString());
                    }
                    else { lblAviso.text = "No se pudo crear el item"; }
                }
                else { lblAviso.text = "Ya existe ese nombre de item"; }
            }
            else { lblAviso.text = "Seleccione o complete todos los campos"; }

        }


        public void Eliminar()
        {
            lblAviso.text = "";
            Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
            if (tblItems.Filas.Count > 0)
            {
                if (tblItems.FilasSeleccionadas.Count > 0)
                {
                    lblAviso.text = "";
                    foreach (var fila in tblItems.FilasSeleccionadas)
                    {
                        if (CntItem.EliminarItem((Item)fila.OBJETO))
                        {
                            lblAviso.text = "Se elimino el item correctamente";
                            tblItems.QuitarFila(fila);
                        }
                        else
                        {
                            Debug.Log("no se pudo eliminar el item");
                        }
                    }
                    tblItems.FilasSeleccionadas.Clear();
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                }
                else { lblAviso.text = "ELIJA UN ITEM PARA ELIMINAR"; }

            }
            else { lblAviso.text = "No hay item cargados para eliminar"; }
        }


        public void EditarItem()
        {
            if (tblItems.Filas.Count > 0)
            {
                if (tblItems.FilasSeleccionadas.Count > 0)
                {
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                    Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                    Utilidades.ActivarODesactivarUnBtn(btnCrear);
                    Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                    lblAviso.text = "";
                    BorrarForm();
                    CargarFormDatosObj((Item)tblItems.FilasSeleccionadas.First().OBJETO);
                    tblItems.HabilitarODeshabilitarInteractividadTabla();

                }
                else { lblAviso.text = "ELIJA UN ITEM PARA EDITAR"; }

            }
            else { lblAviso.text = "No hay items cargados para editar"; }
        }

        public void Actualizar()
        {
            if (Utilidades.NoHayCamposVacios(pnlForm))
            {
                lblAviso.text = "";
                Item item = (Item)tblItems.FilasSeleccionadas.First().OBJETO;

                if (txtNombre.text == item.NOMBRE)
                {
                    lblAvisoNombre.text = "";
                    if (CntItem.EditarItem(
                        item,
                        ValidacionForm.NormalizarCadena(txtNombre.text),
                        CntItem.EFECTOSITEM[ddEfecto.value - 1],
                        ValidacionForm.NormalizarCadena(txtDescripcion.text)
                        ))
                    {
                        tblItems.ActualizarFila<Item>(tblItems.FilasSeleccionadas.First(), item);
                        lblAviso.text = "";
                        BorrarForm();
                        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                        Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                        Utilidades.ActivarODesactivarUnBtn(btnCrear);
                        Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                        btnActualizar.interactable = true;
                        tblItems.HabilitarODeshabilitarInteractividadTabla();
                        lblAviso.text = "Se actualizo el item correctamente";

                    }
                    else { lblAviso.text = "No se pudo actualizar el item"; }

                }
                else
                {
                    if (!CntItem.NOMBREITEMSELECCIONADOS.Contains(ValidacionForm.NormalizarCadena(txtNombre.text)))
                    {
                        lblAvisoNombre.text = "";
                        if (CntItem.EditarItem(
                            (Item)tblItems.FilasSeleccionadas.First().OBJETO,
                            ValidacionForm.NormalizarCadena(txtNombre.text),
                            CntItem.EFECTOSITEM[ddEfecto.value - 1],
                            ValidacionForm.NormalizarCadena(txtDescripcion.text))
                            )
                        {
                            lblAviso.text = "";
                            tblItems.ActualizarFila<Item>(tblItems.FilasSeleccionadas.First(), item);
                            BorrarForm();
                            Utilidades.DeshabilitarOHabilitarElementosPanel(pnlForm);
                            Utilidades.DeshabilitarOHabilitarElementosPanel(pnlBtnCrud);
                            Utilidades.ActivarODesactivarUnBtn(btnCrear);
                            Utilidades.ActivarODesactivarUnBtn(btnActualizar);
                            tblItems.HabilitarODeshabilitarInteractividadTabla();
                            lblAviso.text = "Se actualizo el item correctamente";

                        }
                        else { lblAviso.text = "No se pudo actualizar el item"; }

                    }
                    else { lblAvisoNombre.text = "Ya existe ese nombre"; }
                }
            }
            else { lblAviso.text = "Seleccione o complete todos los campos"; }
        }

        private void CargarFormDatosObj(Item item)
        {
            txtId.text = item.ID.ToString();
            txtNombre.text = item.NOMBRE;
            ddEfecto.value = CntItem.EFECTOSITEM.IndexOf(item.EFECTO) + 1;
            txtDescripcion.text = item.DESCRIPCION;
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
            ddEfecto.value = -1;
            txtDescripcion.text = "";
            lblAviso.text = "";
            lblAvisoNombre.text = "";
        }
        private void CargarDropDown()
        {
            Utilidades.CargarOpcionesEnDropDowns(CntItem.EFECTOSITEM, ddEfecto);
        }
    }
}
