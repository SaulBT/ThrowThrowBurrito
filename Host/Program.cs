using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost hostUsuarios = new ServiceHost(typeof(Servicios.Implementaciones.ImplementacionGestionUsuarios)))
            using (ServiceHost hostJuego = new ServiceHost(typeof(Servicios.Implementaciones.ImplementacionJuego)))
            {
                hostUsuarios.Open();
                hostJuego.Open();

                Console.WriteLine("El servidor se está ejecutando.");
                Console.ReadLine();
            }
        }
    }
}
