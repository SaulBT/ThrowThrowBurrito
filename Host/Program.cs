using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Host
{
    public class Program
    {
        static void Main(string[] args)
        {

            using (ServiceHost hostRegistrarUsuario = new ServiceHost(typeof(Servicios.ImplementacionRegistrarUsuario)))
            using (ServiceHost hostPersonalizarPerfil = new ServiceHost(typeof(Servicios.ImplementacionPersonalizarPerfil)))
            {
                hostRegistrarUsuario.Open();
                hostPersonalizarPerfil.Open();

                Console.WriteLine("Servidor en ejecución...");
                Console.ReadLine();
            }
        }
    }
}
