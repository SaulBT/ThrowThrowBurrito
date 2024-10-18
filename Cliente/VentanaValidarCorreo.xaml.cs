using Cliente.Servidor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;
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
    /// Lógica de interacción para VentanaValidarCorreo.xaml
    /// </summary>
    public partial class VentanaValidarCorreo : Window
    {
        string codigo = null;
        Usuario usuario = null;

        public VentanaValidarCorreo(string codigo, Usuario usuario)
        {
            this.usuario = usuario;
            this.codigo = codigo;
            InitializeComponent();
        }

        private void btnReenviar_Click(object sender, RoutedEventArgs e)
        {
            Servidor.IServicioRegistrarUsuario Proxy = new ServicioRegistrarUsuarioClient();

            this.codigo = Proxy.EnviarCodigoCorreo(usuario.Correo);
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            Servidor.IServicioRegistrarUsuario Proxy = new ServicioRegistrarUsuarioClient();
            if (tbCodigo.Text.Equals(this.codigo))
            {
                Proxy.RegistrarUsuario(this.usuario);
                Console.WriteLine("EL CODIGO SÍ CONINCIDEEE");
                
            } else
            {
                Console.WriteLine("El codigo ingresado no coincide con el enviado al correo");
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            VentanaRegistroUsuario ventanaRegistroUsuario = new VentanaRegistroUsuario();
            ventanaRegistroUsuario.Show();
            this.Close();
        }
    }
}
