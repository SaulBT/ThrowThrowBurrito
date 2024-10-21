using Cliente.ServicioRegistrarUsuario;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.Ventanas
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuario.xaml
    /// </summary>
    public partial class VntRegistrarUsuario : Page
    {
        public VntRegistrarUsuario()
        {
            InitializeComponent();
            Console.WriteLine("constructor vacio");
        }

        public VntRegistrarUsuario(Usuario usuario)
        {
            InitializeComponent();
            txbNombreUsuario.Text = usuario.NombreUsuario;
            txbContrasenia.Text = usuario.Contrasenia;
            txbCorreo.Text = usuario.Correo;
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            ServicioRegistrarUsuario.ServicioRegistrarUsuarioClient Proxy = new ServicioRegistrarUsuario.ServicioRegistrarUsuarioClient();

            //TODO: HACER VALIDACION DE QUE NO SEAN NULL
            if (!string.IsNullOrEmpty(txbNombreUsuario.Text) && !string.IsNullOrEmpty(txbContrasenia.Text) && !string.IsNullOrEmpty(txbCorreo.Text))
            {
                ServicioRegistrarUsuario.Usuario usuario = new ServicioRegistrarUsuario.Usuario()
                {
                    NombreUsuario = txbNombreUsuario.Text,
                    Contrasenia = txbContrasenia.Text,
                    Correo = txbCorreo.Text,
                };
                if (Proxy.ValidarDatos(usuario))
                {
                    ParametrosNavegacion parametros = new ParametrosNavegacion(Proxy.EnviarCodigoCorreo(usuario.Correo), usuario);
                    VntValidarCorreo validarCorreo = new VntValidarCorreo(parametros);
                    NavigationService.Navigate(validarCorreo);
                }
                else
                {
                    Console.WriteLine("Formato incorrecto de datos");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Campos vacíos");
                return;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Ventanas/MenuPrincipal.xaml", UriKind.Relative));
        }

        public class ParametrosNavegacion
        {
            public string Codigo { get; set; }
            public Usuario Usuario { get; set; }

            public ParametrosNavegacion(string codigo, Usuario usuario)
            {
                Codigo = codigo;
                Usuario = usuario;
            }
        }



    }
}
