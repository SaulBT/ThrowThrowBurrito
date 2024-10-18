using AccesoDatos;
using Servicios.clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ImplementacionPersonalizarPerfil : IServicioPersonalizarPerfil
    {
        public bool GuardarCambios(Perfil perfil, string claveUsuario)
        {
            if (NombreEsValido(perfil.NombreUsuario) && FotoEsValida(perfil.Foto) && DescripcionEsValida(perfil.Descripcion))
            {
                using (var contexto = new ModeloDBContainer())
                {
                    var jugador = contexto.Jugador.FirstOrDefault(c => c.claveUsuario == claveUsuario);
                    if (jugador != null)
                    {
                        jugador.nombreUsuario = perfil.NombreUsuario;
                        jugador.descripcion = perfil.Descripcion;
                        jugador.fotoPerfil = perfil.Foto;
                        contexto.SaveChanges();
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            } else
            {
                return false;
            }
        }

        public Perfil ObtenerPerfil(string claveUsuario)
        {
            using (var contexto = new ModeloDBContainer())
            {
                var jugador = contexto.Jugador
                    .Where(j => j.claveUsuario == claveUsuario)
                    .Select(j => new { j.nombreUsuario, j.descripcion, j.fotoPerfil })
                    .FirstOrDefault();
                if (jugador != null)
                {
                    Perfil perfil = new Perfil()
                    {
                        NombreUsuario = jugador.nombreUsuario,
                        Foto = jugador.fotoPerfil,
                        Descripcion = jugador.descripcion
                    };
                    return perfil;
                }
            }
            return null;
        }

        //Métodos no pertenecientes a la interfaz

        public bool NombreEsValido(string nombre)
        {
            if ((nombre.Length >= 3) && (nombre.Length <= 20))
            {
                foreach (char c in nombre)
                {
                    if (!char.IsLetterOrDigit(c) && c != '-' && c != '.')
                    {
                        Console.WriteLine("El nombre contiene símbolos no permitidos");
                        return false;
                    }
                }
                Console.WriteLine("Nombre valido");
                return true;
            }
            else
            {
                Console.WriteLine("El nombre es mayor a 20 caracteres o menor a 3");
                return false;
            }
        }

        public bool FotoEsValida(byte[] foto)
        {
            const int mb = 1024 * 1024;
            if (foto.Length < 5 * mb)
            {
                return true;
            } else
            {
                return false;
            }
            
        }

        public bool DescripcionEsValida(string descripcion)
        {
            if (descripcion.Length < 200)
            {
                return true;
            } else
            {
                return false;
            }
            
        }

    }
}
