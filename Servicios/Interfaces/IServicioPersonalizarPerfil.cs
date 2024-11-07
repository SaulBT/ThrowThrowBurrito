using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace Servicios.Interfaces
{
    [ServiceContract]
    public interface IServicioPersonalizarPerfil
    {
        [OperationContract]
        bool GuardarCambios(Perfil perfil, string claveUsuario);
        
    }

    [DataContract]
    public class Perfil
    {
        private byte[] foto;
        private string nombreUsuario;
        private string descripcion;


        [DataMember]
        public byte[] Foto { get { return foto; } set { foto = value; } }

        [DataMember]
        public string NombreUsuario { get { return nombreUsuario; } set { nombreUsuario = value; } }

        [DataMember]
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
    }
}
