using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cliente
{
    /// <summary>
    /// Lógica de interacción para VentanaRegistroUsuario.xaml
    /// </summary>
    public partial class VentanaRegistroUsuario : Window
    {
        public VentanaRegistroUsuario()
        {
            InitializeComponent();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Servidor.ServicioRegistrarUsuarioClient Proxy = new Servidor.ServicioRegistrarUsuarioClient();

            //TODO: HACER VALIDACION DE QUE NO SEAN NULL

            Servidor.Usuario usuario = new Servidor.Usuario()
            {
                NombreUsuario = tbNombreUsuario.Text,
                Contrasenia = tbContrasenia.Text,
                Correo = tbCorreo.Text,
            };

            //TODO: HACER LA MADRE DEL CALLBACK PQ SINO RESPONDE EL SERVER EL CLIENTE MUERE

            if (Proxy.ValidarDatos(usuario))
            {
                Proxy.EnviarCodigoCorreo(usuario.Correo);
                if (Proxy.ValidarCorreo("Aca va el texto del textbox del codigo"))
                {
                    Proxy.RegistrarUsuario(usuario);
                } else
                {
                    //EN CASO DE NO SER EL MISMO CODIGO
                }
            } else
            {
                //en caso de datos con formato incorrecto
            }


        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
