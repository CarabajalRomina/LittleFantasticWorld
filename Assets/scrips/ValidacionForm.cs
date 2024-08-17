using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;

namespace Assets.scrips
{
    public static class ValidacionForm
    {
        public static bool EsUnNombreValido(string nombre)
        {
            return nombre.Length > 3;
        }

        public static bool EstaVacioElInput(string nombre)
        {
            return string.IsNullOrWhiteSpace(nombre);
        }

        public static bool SeSeleccionoUnValor(TMP_Dropdown dp)
        {
            if (dp.value == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string NormalizarCadena(string cadena) 
        {
            cadena = cadena.Trim(); // elimino espacios en blanco al principio y fin
            cadena = Regex.Replace(cadena, @"\s+", " ");
            cadena = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cadena.ToLower());

            return cadena;
        }

    }
}
