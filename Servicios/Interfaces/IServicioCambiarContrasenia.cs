using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    [ServiceContract]
    public interface IServicioCambiarContrasenia
    {
        [OperationContract]
        bool CambiarContrasenia(string contrasenia, string claveUsuario);

    }
}
