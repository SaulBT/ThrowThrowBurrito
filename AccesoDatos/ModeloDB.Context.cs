﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModeloDBContainer : DbContext
    {
        public ModeloDBContainer()
            : base("name=ModeloDBContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Amigo> Amigo { get; set; }
        public virtual DbSet<Bloqueado> Bloqueado { get; set; }
        public virtual DbSet<DatosJugadorPartida> DatosJugadorPartida { get; set; }
        public virtual DbSet<Jugador> Jugador { get; set; }
        public virtual DbSet<Partida> Partida { get; set; }
        public virtual DbSet<SolicitudAmigo> SolicitudAmigo { get; set; }
        public virtual DbSet<SolicitudBloqueo> SolicitudBloqueo { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
