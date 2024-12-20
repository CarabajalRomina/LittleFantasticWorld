using System.Globalization;
using System.Text.RegularExpressions;
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
