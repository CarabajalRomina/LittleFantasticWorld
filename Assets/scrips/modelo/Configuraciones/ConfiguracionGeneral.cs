using UnityEngine;

namespace Assets.scrips.modelo.configuraciones
{
    public static class ConfiguracionGeneral
    {
        #region TERRENOS
        public static readonly int CantMaxEnemigosXTerreno = 3;
        public static readonly int CantidadMaxComidaXTerreno = 2;
        public static readonly int CantidadMaxItemsXTerreno = 2;
        public static readonly int CantMaxPersonajesXTerreno = 1;
        #endregion

        #region ENTIDADES
        public static readonly int VidaMaxEntidades = 200;
        public static Vector2 CoordenadasOffset = new Vector2(0f, 0f);
        #endregion

        #region MOVIMIENTO
        public static readonly float VelocidadMovimientoPersonaje = 5f;
        #endregion

        #region INVENTARIO
        public static readonly int CantMaxInteractuablesInventario = 5;
        #endregion

        #region ESTRATEGIAITEMS
        public static readonly int ValorCritico = 40; //es en porcentaje 
        public static readonly int AtaqueBasePersonaje = 15;//puntos de vida
        public static readonly int AtaqueBaseEnemigo = 12;//puntos de vida
        public static readonly int PuntosDeVida = 10; //tanto para aumentar o descontar 
        public static readonly int CostoMinimoDeAccion = 10; // tanto para descontar como para aumentar
        #endregion

       
    }
}