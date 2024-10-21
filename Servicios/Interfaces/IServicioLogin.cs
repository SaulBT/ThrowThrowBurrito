using AccesoDatos;
using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    [ServiceContract]
    public interface IServicioLogin
    {
        [OperationContract]
        [FaultContract(typeof(ExcepcionServicioLogin))]
        Jugador Login(string nombreUsuario, string contrasenia);

    }

    [DataContract]
    public class ExcepcionServicioLogin
    {
        private string mensaje;

        public ExcepcionServicioLogin()
        {
        }

        [DataMember]
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }
    }
}
