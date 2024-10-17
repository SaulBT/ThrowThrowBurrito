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

            using (ServiceHost host = new ServiceHost(typeof(Servicios.ImplementacionRegistrarUsuario)))
            {
                host.Open();
                Console.WriteLine("Servidor en ejecución...");
                Console.ReadLine();
            }
        }
    }
}
