using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ImplementacionPersonalizarPerfil : IServicioPersonalizarPerfil
    {
        private ModeloDBContainer _contexto;
        public ImplementacionPersonalizarPerfil() { }
        public ImplementacionPersonalizarPerfil(ModeloDBContainer contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }
        public bool GuardarCambios(Perfil perfil, string claveUsuario)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var jugador = contexto.Jugador.FirstOrDefault(c => c.claveUsuario == claveUsuario);
                if (jugador != null)
                {
                    jugador.nombreUsuario = perfil.NombreUsuario;
                    jugador.descripcion = perfil.Descripcion;
                    jugador.fotoPerfil = perfil.Foto;

                    contexto.Entry(jugador).State = EntityState.Modified;
                    contexto.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
