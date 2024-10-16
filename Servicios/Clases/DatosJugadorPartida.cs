using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Clases
{
    internal class DatosJugadorPartida
    {
        private int puntaje;
        private int idAspecto;
        private Boolean expulsado;
        private int idDatosJugadorPartida;

        public DatosJugadorPartida()
        {

        }

        public DatosJugadorPartida(int puntaje, int idAspecto, bool expulsado, int idDatosJugadorPartida)
        {
            this.puntaje = puntaje;
            this.idAspecto = idAspecto;
            this.expulsado = expulsado;
            this.idDatosJugadorPartida = idDatosJugadorPartida;
        }

        [DataMember]
        public int Puntaje
        {
            get { return puntaje; }
            set { puntaje = value; }
        }

        [DataMember]
        public int IdAspecto
        {
            get { return idAspecto; }
            set { idAspecto = value; }
        }

        [DataMember]
        public Boolean Expulsado
        {
            get { return expulsado; }
            set { expulsado = value; }
        }

        [DataMember]
        public int IdDatosJugadorPartida
        {
            get { return IdDatosJugadorPartida; }
            set { idDatosJugadorPartida = value; }
        }

    }
}
