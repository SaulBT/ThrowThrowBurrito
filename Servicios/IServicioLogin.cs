using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    [ServiceContract]
    internal interface IServicioLogin
    {
        [OperationContract]
        bool Login(string nombreUsuario, string contrasenia);
    }
}
