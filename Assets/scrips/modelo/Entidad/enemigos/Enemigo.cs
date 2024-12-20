using Assets.scrips.interfaces;
using Assets.scrips.interfaces.pelea;
using Assets.scrips.modelo.interactuables.item.estrategias;
using System;
using UnityEngine;

namespace Assets.scrips.modelo.entidad
{
    public class Enemigo : Entidad, ICombate
    {
        private int Id;

        static int GlobalCount = 0;
        private int VidaActual { get; set; }
        private int VidaMax { get; set; }
        private int PuntosAtaque { get; set; }
        private int PuntosDefensa { get; set; }


        public Enemigo(string nombre, IReino reino, IHabitat habitats, int vidaMax, int puntosAtaque, int puntosDefensa) :base( nombre, reino, habitats) 
        {
            Id = ++GlobalCount;
            VIDAMAX = vidaMax;
            SetVidaActual(vidaMax);
            PUNTOSATAQUE = puntosAtaque;
            PUNTOSDEFENSA = puntosDefensa;
        }

        #region PROPIEDADES 
        public int ID
        {
            get { return Id; }
        }
        public int VIDAACTUAL
        {
            get { return VidaActual; }
        }

        public int VIDAMAX
        {
            get { return VidaMax; }
            set
            {
                if (value > 0)
                {
                    VidaMax = value;
                }
            }
        }

        public int PUNTOSATAQUE
        {
            get { return PuntosAtaque; }
            set
            {
                if (value > 0)
                {
                    PuntosAtaque = value;
                }
            }
        }

        public int PUNTOSDEFENSA
        {
            get { return PuntosDefensa; }
            set
            {
                if (value > 0)
                {
                    PuntosDefensa = value;
                }
            }
        }
        #endregion

        public bool SetVidaActual(int value)
        {
            if (value > 0 || value <= VidaMax)
            {
                VidaActual = value;
                return true;
            }
            else
            {
                if (value > VidaMax)
                {
                    VidaActual = VidaMax;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #region METODOS COMBATE
       
        public int Atacar()
        {
            var dado = Dado.TirarDado();
            return PUNTOSATAQUE + dado;
        }

        public int Defender()
        {
            var dado = Dado.TirarDado();
            return PUNTOSDEFENSA + dado;
        }

        public string ObtenerNombre()
        {
            return NOMBRE;
        }

        public int ObtenerVidaActual()
        {
            return VIDAACTUAL;
        }

        public int ObtenerVidaMaxima()
        {
            return VIDAMAX;
        }

        public bool ActualizarVidaActual(int value)
        {
            return SetVidaActual(value);
        }

        public bool EstaVivo()
        {
            if (VidaActual > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RecibirDanio(int danio)
        {
            ReducirVidaActual(danio);
        }

        public int Defender(int danio)
        {
            throw new NotImplementedException();
        }
        public GameObject ObtenerPrefab()
        {
            return PERSONAJEPREFAB;
        }

        #endregion

        public bool ReducirVidaActual(int valor)
        {
            if (SetVidaActual(VidaActual -= valor))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string[] ObtenerValoresInstancias()
        {
            return new string[] {
            ID.ToString(),
            NOMBRE,
            REINO.ToString(),
            HABITATS.ToString(),
            VIDAMAX.ToString(),
            PUNTOSATAQUE.ToString(),
            PUNTOSDEFENSA.ToString(),
            };
        }
        public override string ToString()
        {
            return $"" +
                $" ENEMIGO: " +
                $" Id: {ID}," +
                $" Nombre: {NOMBRE}," +
                $" Reino: {REINO}," +
                $" Habitats: {HABITATS}," +
                $" Vida maxima: {VIDAMAX}," +
                $" Vida actual: {VIDAACTUAL}," +
                $" Puntos de ataque: {PUNTOSATAQUE}," +
                $" Puntos de defensa: {PUNTOSDEFENSA},";

        }

        public void EjecutarAccion(IAccionCombate accion, ICombate objetivo)
        {
            accion.EjecutarAccion(this, objetivo);
        }

    
    }
}
