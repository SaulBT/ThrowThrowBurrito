using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Clases
{
    public class Amigo
    {
        private int idAmigo;
        private int claveUsuarioAmigo;

        public Amigo()
        {

        }

        public Amigo(int idAmigo, int claveUsuarioAmigo)
        {
            this.idAmigo = idAmigo;
            this.claveUsuarioAmigo = claveUsuarioAmigo;
        }

        [DataMember]
        public int IdAmigo
        {
            get { return idAmigo; }
            set { idAmigo = value; }
        }
        [DataMember]
        public int ClaveUsuarioAmigo
        {
            get { return claveUsuarioAmigo; }
            set { claveUsuarioAmigo = value; }
        }

    }
}
