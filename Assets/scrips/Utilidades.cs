using UnityEngine;


namespace Assets.scrips
{
    public static class Utilidades
    {
        // Genera un número aleatorio dentro de un rango específico
        public static int GenerarNumeroAleatorio(int min, int max)
        {
            return Random.Range(min, max);
        }
    }
}
