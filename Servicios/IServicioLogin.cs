using System;
using System.Collections;
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
        Dictionary<bool,object> Login(string nombreUsuario, string contrasenia);
    }
}
