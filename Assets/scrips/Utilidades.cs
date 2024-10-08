using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.scrips
{
    public static class Utilidades
    {
        // Genera un número aleatorio dentro de un rango específico
        public static int GenerarNumeroAleatorio(int min, int max)
        {
            return Random.Range(min, max);
        }


        public static void CargarOpcionesEnDropDowns<T>(List<T> opciones, TMP_Dropdown dropdown)
        {
            dropdown.ClearOptions();

            dropdown.options.Add(new TMP_Dropdown.OptionData("-- Seleccione una opcion --"));
            dropdown.value = dropdown.options.Count - 1;

            foreach (var opcion in opciones)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData(opcion.ToString()));
            }
        }

        public static void DeshabilitarOHabilitarElementosPanel(GameObject panel)
        {
            foreach (var selectable in panel.GetComponentsInChildren<Selectable>())
            {
                selectable.interactable = !selectable.interactable;
            }
        }

        public static bool NoHayCamposVacios(GameObject pnlForm)
        {
            TMP_InputField[] inputFields = pnlForm.GetComponentsInChildren<TMP_InputField>();

            for (var i = 1; i < inputFields.Length; i++)
            {
                if (ValidacionForm.EstaVacioElInput(inputFields[i].text))
                {
                    return false;
                }
            }
            return true;
        }

        public static void ActivarODesactivarUnBtn(Button btn)
        {
            btn.gameObject.SetActive(!btn.gameObject.activeInHierarchy);
        }
    }
}
