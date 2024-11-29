using Assets.scrips.interfaces;
using Assets.scrips.interfaces.efecto;
using Assets.scrips.interfaces.interactuable;
using Assets.scrips.interfaces.posicionable;

public class Item : IInteractuable, IDescribible, IPosicionable
{
    int Id;
    static int GlobalCount;
    string Nombre;
    IEfectoItem Efecto;
    string Descripcion;
    Terreno TerrenoActual;

    public Item(string nombre, IEfectoItem efecto, string descripcion)
    {
        Id = ++GlobalCount;
        NOMBRE = nombre;
        EFECTO = efecto;
        DESCRIPCION = descripcion;
    }

    #region PROPIEDADES
    public int ID
    {
        get { return Id; }
    }
    public string NOMBRE
    {
        get { return Nombre; }
        set
        {
            if (value != null)
            {
                Nombre = value;
            }
        }
    }

    public string DESCRIPCION
    {
        get { return Descripcion; }
        set
        {
            if (value != null)
            {
                Descripcion = value;
            }
        }
    }
    public Terreno TERRENOACTUAL
    {
        get { return TerrenoActual; }
        set { TerrenoActual = value; }
    }
    public IEfectoItem EFECTO
    {
        get { return Efecto; }
        set
        {
            if (value != null)
            {
                Efecto = value;
            }
        }
    }

    #endregion

    public bool Interactuar(Personaje entidad)
    {
        if (Efecto.AplicarEfecto(entidad))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string[] ObtenerValoresInstancias()
    {
        return new string[] {
            ID.ToString(),
            NOMBRE,
            EFECTO.ToString(),
            DESCRIPCION,
            };
    }

    public void EstablecerPosicion(Terreno terrenoDestino)
    {
        TerrenoActual = terrenoDestino;
        TerrenoActual.AgregarInteractuable(this);
    }

    public override string ToString()
    {
        return $"" +
            $" ITEM: " +
            $" Id: {ID}," +
            $" Nombre: {NOMBRE}," +
            $" Efecto: {EFECTO}," +
            $" Descripcion: {Descripcion}";
    }

    public string GetNombre()
    {
        return NOMBRE;
    }
}
