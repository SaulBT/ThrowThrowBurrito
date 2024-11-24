using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class RepositorioCallbacks<T>
    {
        private readonly ConcurrentDictionary<ICommunicationObject, T> callbacks = new ConcurrentDictionary<ICommunicationObject, T>();

        public void AgregarCallback(ICommunicationObject canal, T callback)
        {
            if (canal != null && callback != null)
            {
                canal.Faulted += (sender, args) =>
                {
                    Console.WriteLine("Borrando cliente por excepción");
                    eliminarCallback(canal);
                };

                canal.Closed += (sender, args) =>
                {
                    Console.WriteLine("Borrando cliente por desconexión");
                    eliminarCallback(canal);
                };

                callbacks.TryAdd(canal, callback);
                Console.WriteLine("Agegando cliente");
            }
        }

        public void eliminarCallback(ICommunicationObject canal)
        {
            if (canal != null)
            {
                callbacks.TryRemove(canal, out _);
            }
        }
    }
}
