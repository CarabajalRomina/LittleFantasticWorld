using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownManager: MonoBehaviour
{
    TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = transform.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.onValueChanged.AddListener(delegate { SelectedItem(); });
    }


    public void CargarOpciones(List<object> opciones)
    {
        dropdown.options.Clear();

        dropdown.options.Add(new TMP_Dropdown.OptionData("-- Seleccione una opcion --"));

        foreach (var opcion in opciones)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(opcion.ToString()));
        }
    }

    public int SelectedItem()
    {
        return dropdown.value;
    }


}
