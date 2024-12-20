using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scrips.vista.DatosPersonajeUi
{
    public class DatosPersonajeUI: MonoBehaviour
    {
        public Slider BarraVidaPersonaje;
        public Slider BarraEnergiaPersonaje;
        public Slider BarraPuntosAtaque;
        public Slider BarraPuntosDefensa;

        public TextMeshProUGUI txtVidaPersonaje;
        public TextMeshProUGUI txtEnergiaPersonaje;
        public TextMeshProUGUI txtPuntosAtaquePersonaje;
        public TextMeshProUGUI txtPuntosDefensa;
        public TextMeshProUGUI txtNombrePersonaje;



        public void CargarDatosPersonaje(Personaje personaje)
        {
            txtNombrePersonaje.text = personaje.NOMBRE;
            if (BarraVidaPersonaje != null)
            {
                BarraVidaPersonaje.maxValue = personaje.VidaMax;
                BarraVidaPersonaje.value = personaje.VidaActual;
            }
            txtVidaPersonaje.text = $"{personaje.VidaActual} / {personaje.VidaMax}";

            if (BarraEnergiaPersonaje != null)
            {
                BarraEnergiaPersonaje.maxValue = personaje.ENERGIAMAX;
                BarraEnergiaPersonaje.value = personaje.ENERGIAACTUAL;
            }
            txtEnergiaPersonaje.text = $"{personaje.ENERGIAACTUAL} / {personaje.ENERGIAMAX}";

            if (BarraPuntosAtaque != null)
            {
                BarraPuntosAtaque.maxValue = personaje.PUNTOSATAQUE;
                BarraPuntosAtaque.value = personaje.PUNTOSATAQUE;
            }
            txtPuntosAtaquePersonaje.text = $"{personaje.PUNTOSATAQUE} / {personaje.PUNTOSATAQUE}";

            if (BarraPuntosDefensa != null)
            {
                BarraPuntosDefensa.maxValue = personaje.PUNTOSDEFENSA;
                BarraPuntosDefensa.value = personaje.PUNTOSDEFENSA;
            }
            txtPuntosDefensa.text = $"{personaje.PUNTOSDEFENSA} / {personaje.PUNTOSDEFENSA}";
        }
    }
}
