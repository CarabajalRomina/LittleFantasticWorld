using Assets.scrips.Controllers.jugador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.scrips.vista
{
    public class CrearJugadorUI : MonoBehaviour
    {
        public TextMeshProUGUI lblAviso;
        public TMP_InputField txtNombreJugador;
        public GameObject pnlPopup;

        public TextMeshProUGUI lblNombreJugador;

        JugadorController CntJugador = JugadorController.GetInstancia;

        public void CrearUsuario()
        {
            if (!ValidacionForm.EstaVacioElInput(txtNombreJugador.text) && ValidacionForm.EsUnNombreValido(txtNombreJugador.text))
            {
                if (!CntJugador.CrearUsuario(ValidacionForm.NormalizarCadena(txtNombreJugador.text)))
                {
                    lblAviso.text = "Ese nombre ya existe";
                }
                else
                {
                    if (CntJugador != null && lblNombreJugador != null)
                    {
                        lblNombreJugador.text = CntJugador.PLAYER.NOMBREJUGADOR;
                    }
                    Destroy(pnlPopup);                
                }
            }
            else
            {
                lblAviso.text = "* El nombre tiene que tener mas de 3 letras";
            }
        }
    }
}
