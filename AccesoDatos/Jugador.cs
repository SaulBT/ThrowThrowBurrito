//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesoDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Jugador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Jugador()
        {
            this.Amigo = new HashSet<Amigo>();
            this.Amigo1 = new HashSet<Amigo>();
            this.Bloqueado = new HashSet<Bloqueado>();
            this.Bloqueado1 = new HashSet<Bloqueado>();
            this.DatosJugadorPartida = new HashSet<DatosJugadorPartida>();
        }
    
        public string descripcion { get; set; }
        public byte[] fotoPerfil { get; set; }
        public string correoElectronico { get; set; }
        public string claveUsuario { get; set; }
        public string contrasenia { get; set; }
        public string estado { get; set; }
        public string nombreUsuario { get; set; }
        public Nullable<bool> esInvitado { get; set; }
        public int idJugador { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Amigo> Amigo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Amigo> Amigo1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bloqueado> Bloqueado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bloqueado> Bloqueado1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatosJugadorPartida> DatosJugadorPartida { get; set; }
    }
}
