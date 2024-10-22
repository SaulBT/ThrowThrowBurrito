using Cliente.ServicioRegistrarUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.Ventanas
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuario.xaml
    /// </summary>
    public partial class VntRegistrarUsuario : Page
    {
        private ServicioRegistrarUsuario.ServicioRegistrarUsuarioClient proxy;
        public VntRegistrarUsuario()
        {
            try
            {
                InitializeComponent();
                proxy = new ServicioRegistrarUsuario.ServicioRegistrarUsuarioClient();
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
        }

        public VntRegistrarUsuario(Usuario usuario)
        {
            try
            {
                InitializeComponent();
                proxy = new ServicioRegistrarUsuario.ServicioRegistrarUsuarioClient();
                txbNombreUsuario.Text = usuario.NombreUsuario;
                txbContrasenia.Text = usuario.Contrasenia;
                txbCorreo.Text = usuario.Correo;
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                //TODO: HACER VALIDACION DE QUE NO SEAN NULL
                if (!string.IsNullOrEmpty(txbNombreUsuario.Text) && !string.IsNullOrEmpty(txbContrasenia.Text) && !string.IsNullOrEmpty(txbCorreo.Text))
                {
                    ServicioRegistrarUsuario.Usuario usuario = new ServicioRegistrarUsuario.Usuario()
                    {
                        NombreUsuario = txbNombreUsuario.Text,
                        Contrasenia = txbContrasenia.Text,
                        Correo = txbCorreo.Text,
                    };
                    if (proxy.ValidarDatos(usuario))
                    {
                        ParametrosNavegacion parametros = new ParametrosNavegacion(proxy.EnviarCodigoCorreo(usuario.Correo), usuario);
                        VntValidarCorreo validarCorreo = new VntValidarCorreo(parametros);
                        NavigationService.Navigate(validarCorreo);
                    }
                    else
                    {
                        mostrarAlerta("Formato incorrecto de datos, corríjalos e intente de nuevo.");
                        return;
                        /*
                         * TODO pasar las verificaciones del servidor al cliente 
                         * para poder especificar cuál campo tiene formato incorrecto
                         */
                    }
                }
                else
                {
                    mostrarAlerta("Los campos están vacíos, por favor llénelos todos.");
                    return;
                }
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            vntLogin vntLogin = new vntLogin();
            NavigationService.Navigate(vntLogin);
            proxy.Close();
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

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            gFondoNegro.Visibility = Visibility.Hidden;
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
            Console.WriteLine(proxy.State.ToString());
            if (proxy.State == CommunicationState.Closed)
            {
                NavigationService.GoBack();
            }
        }

        private void mostrarAlerta(string mensaje)
        {
            gFondoNegro.Visibility = Visibility.Visible;
            gVentanaEmergente.Visibility = Visibility.Visible;
            tbcMensajeEmergente.Text = mensaje;
        }
    }
}
