using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Clases
{
    public class SolicitudAmigo
    {
        private DateTime fecha;
        private String estado;
        private int idSolicitudAmigo;

        public SolicitudAmigo()
        {
        }

        public SolicitudAmigo(DateTime fecha, string estado, int idSolicitudAmigo)
        {
            this.fecha = fecha;
            this.estado = estado;
            this.idSolicitudAmigo = idSolicitudAmigo;
        }

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        [DataMember]
        public String Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        [DataMember]
        public int IdSolicitudAmigo
        {
            get { return idSolicitudAmigo; }
            set { idSolicitudAmigo = value; }
        }
    }
}
