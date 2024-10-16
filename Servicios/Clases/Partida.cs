using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Clases
{
    internal class Partida
    {
        private String estado;
        private DateTime fecha;
        private int duracion;
        private String codigoPartida;
        private int tiempoGuerra;
        private int puntajeVictoria;
        private String nombreGanador;
        private int idPartida;

        public Partida()
        {

        }

        public Partida(string estado, DateTime fecha, int duracion, string codigoPartida, int tiempoGuerra, int puntajeVictoria, string nombreGanador, int idPartida)
        {
            this.estado = estado;
            this.fecha = fecha;
            this.duracion = duracion;
            this.codigoPartida = codigoPartida;
            this.tiempoGuerra = tiempoGuerra;
            this.puntajeVictoria = puntajeVictoria;
            this.nombreGanador = nombreGanador;
            this.idPartida = idPartida;
        }

        [DataMember]
        public String Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        [DataMember]
        public int Duracion
        {
            get { return duracion; }
            set { duracion = value; }
        }

        [DataMember]
        public String CodigoPartida
        {
            get { return codigoPartida; }
            set { codigoPartida = value; }
        }

        [DataMember]
        public int TiempoGuerra
        {
            get { return tiempoGuerra; }
            set { tiempoGuerra = value; }
        }

        [DataMember]
        public int PuntajeVictoria
        {
            get { return puntajeVictoria; }
            set { puntajeVictoria = value; }
        }

        [DataMember]
        public String NombreGanador
        {
            get { return nombreGanador; }
            set { nombreGanador = value; }
        }

        [DataMember]
        public int IdPartida
        {
            get { return idPartida; }
            set { idPartida = value; }
        }

    }
}
