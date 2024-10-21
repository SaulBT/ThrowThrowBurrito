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
using static Cliente.Ventanas.VntRegistrarUsuario;

namespace Cliente.Ventanas
{
    /// <summary>
    /// Lógica de interacción para ValidarCorreo.xaml
    /// </summary>
    public partial class VntValidarCorreo : Page
    {
        private string codigo;
        private Usuario usuario;
        public VntValidarCorreo(ParametrosNavegacion parametros)
        {
            InitializeComponent();
            this.usuario = parametros.Usuario;
            this.codigo = parametros.Codigo;
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            ServicioRegistrarUsuario.IServicioRegistrarUsuario Proxy = new ServicioRegistrarUsuarioClient();
            this.codigo = Proxy.EnviarCodigoCorreo(usuario.Correo);
        }

        private void btnReenviar_Click(object sender, RoutedEventArgs e)
        {
            ServicioRegistrarUsuario.IServicioRegistrarUsuario Proxy = new ServicioRegistrarUsuarioClient();
            if (txbCodigo.Text.Equals(this.codigo))
            {
                Proxy.RegistrarUsuario(this.usuario);
                Console.WriteLine("EL CODIGO SÍ CONINCIDEEE");
                //TODO IMPLEMENTAR FUNCION DE INGRESAR AL MENU PRINCIPAL YA LOGEADO
            }
            else
            {
                Console.WriteLine("El codigo ingresado no coincide con el enviado al correo");
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
