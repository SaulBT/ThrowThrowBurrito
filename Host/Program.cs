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
            using (ServiceHost hostBurrito = new ServiceHost(typeof(Servicios.ImplementacionLogin)))
            {
                hostBurrito.Open();
                Console.WriteLine("El servidor se est[a ejecutando.");
                Console.ReadLine();
            }
        }
    }
}
