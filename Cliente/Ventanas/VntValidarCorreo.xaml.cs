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
        private ServicioRegistrarUsuarioClient proxy;
        public VntValidarCorreo(ParametrosNavegacion parametros)
        {
            try
            {
                InitializeComponent();
                proxy = new ServicioRegistrarUsuarioClient();
                this.usuario = parametros.Usuario;
                this.codigo = parametros.Codigo;
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

        private void btnReenviar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.codigo = proxy.EnviarCodigoCorreo(usuario.Correo);
                mostrarAlerta("Se ha enviado un nuevo código al correo proporcionado.");
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

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txbCodigo.Text.Equals(this.codigo))
                {
                    proxy.RegistrarUsuario(this.usuario);
                    mostrarAlerta("Se ha registrado el usuario exitosamente.");
                    /* 
                     * TODO IMPLEMENTAR FUNCION DE INGRESAR AL MENU PRINCIPAL YA LOGEADO
                     * También debería verificar excepciones con la base de datos desde el servidor
                     */
                }
                else
                {
                    mostrarAlerta("El codigo ingresado no coincide con el enviado al correo.");
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

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            proxy.Close();
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            gFondoNegro.Visibility = Visibility.Hidden;
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
            Console.WriteLine(proxy.State.ToString());
            if (proxy.State == CommunicationState.Closed)
            {
                //NavigationService.GoBack();
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
