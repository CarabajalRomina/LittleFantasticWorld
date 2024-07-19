using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class Singleton<T> where T : class, new ()
    {
        private static T instancia;

        public static T GetInstancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new T();
                }
                return instancia;
            }
        }
    }
}
