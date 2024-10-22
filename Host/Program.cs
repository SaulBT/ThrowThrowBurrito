using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost hostBurrito = new ServiceHost(typeof(Servicios.Implementaciones.ImplementacionLogin)))
            using (ServiceHost hostChat = new ServiceHost(typeof(Servicios.Implementaciones.ImplementacionChat)))
            {
                hostBurrito.Open();
                hostChat.Open();

                Console.WriteLine("El servidor se está ejecutando.");
                Console.ReadLine();
            }

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
