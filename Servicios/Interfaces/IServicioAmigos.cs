using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    [ServiceContract]
    internal interface IServicioAmigos
    {
        [OperationContract]
        Amigo[] CargarAmigos(int idJugador);

        [OperationContract]
        void EliminarAmigo(int idJugador, int idAmigo);
    }
}
