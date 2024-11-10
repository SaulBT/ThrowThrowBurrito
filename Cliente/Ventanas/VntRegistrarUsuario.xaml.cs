using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Cliente.ServiciosGestionUsuarios;

namespace Cliente.Ventanas
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuario.xaml
    /// </summary>
    public partial class VntRegistrarUsuario : Page
    {
        private ServicioRegistrarUsuarioClient servicio;
        public VntRegistrarUsuario()
        {
            try
            {
                InitializeComponent();
                servicio= new ServicioRegistrarUsuarioClient();
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
        }

        public VntRegistrarUsuario(Usuario usuario)
        {
            try
            {
                InitializeComponent();
                servicio = new ServicioRegistrarUsuarioClient();
                txbNombreUsuario.Text = usuario.NombreUsuario;
                pwbContrasenia.Password = usuario.Contrasenia;
                txbCorreo.Text = usuario.Correo;
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
        }
        
        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbNombreUsuario.Text) && !string.IsNullOrEmpty(pwbContrasenia.Password) && !string.IsNullOrEmpty(txbCorreo.Text))
            {
                Usuario usuario = new Usuario()
                {
                    NombreUsuario = txbNombreUsuario.Text,
                    Contrasenia = pwbContrasenia.Password,
                    Correo = txbCorreo.Text,
                };
                try
                {
                    if (validarDatos(usuario))
                    {
                        ParametrosNavegacion parametros = new ParametrosNavegacion(servicio.EnviarCodigoCorreo(usuario.Correo), usuario);
                        VntValidarCorreo validarCorreo = new VntValidarCorreo(parametros);
                        NavigationService.Navigate(validarCorreo);
                    }
                    else
                    {
                        return;
                    }
                }
                catch (EndpointNotFoundException ex)
                {
                    mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                    Console.WriteLine(ex.Message);
                    servicio.Abort();
                }
                catch (CommunicationException ex)
                {
                    mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                    Console.WriteLine(ex.Message);
                    servicio.Abort();
                }
                catch (Exception ex)
                {
                    mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                    Console.WriteLine(ex.Message);
                    servicio.Abort();
                }
            }
            else
            {
                mostrarAlerta("No deje los campos vacíos por favor.");
                return;
            }
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            vntLogin vntLogin = new vntLogin();
            NavigationService.Navigate(vntLogin);
            servicio.Close();
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

        private bool validarDatos(Usuario usuario)
        {
            bool nombreValidado =  NombreEsValido(usuario.NombreUsuario);
            bool contraseniaValidada = ContraseniaEsValida(usuario.Contrasenia);
            bool correoValidado = CorreoEsValido(usuario.Correo);
            if (nombreValidado && contraseniaValidada && correoValidado)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            gFondoNegro.Visibility = Visibility.Hidden;
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
            Console.WriteLine(servicio.State.ToString());
            if (servicio.State == CommunicationState.Closed)
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

        public bool NombreEsValido(string nombre)
        {
            if ((nombre.Length >= 3) && (nombre.Length <= 20))
            {
                foreach (char c in nombre)
                {
                    if (!char.IsLetterOrDigit(c) && c != '-' && c != '.')
                    {
                        tbcErrorNombreUsuario.Text = "Formato inválido!";
                        tbcErrorNombreUsuario.Visibility = Visibility.Visible;
                        return false;
                    }
                }
                if (servicio.ValidarNombreNoRepetido(nombre))
                {
                    tbcErrorNombreUsuario.Visibility = Visibility.Hidden;
                    return true;
                }
                tbcErrorNombreUsuario.Text = "Nombre ya ocupado!";
                tbcErrorNombreUsuario.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                tbcErrorNombreUsuario.Text = "Formato inválido!";
                tbcErrorNombreUsuario.Visibility = Visibility.Visible;
                return false;
            }
        }

        public bool ContraseniaEsValida(string contrasenia)
        {
            if ((contrasenia.Length >= 8) && (contrasenia.Length <= 100))
            {
                int contNumero = 0, contMayuscula = 0, contMinuscula = 0;
                foreach (char c in contrasenia)
                {
                    if (char.IsNumber(c))
                    {
                        contNumero++;
                    }
                    else if (char.IsUpper(c))
                    {
                        contMayuscula++;
                    }
                    else if (char.IsLower(c))
                    {
                        contMinuscula++;
                    }
                }
                if (contNumero > 0 && contMayuscula > 0 && contMinuscula > 0)
                {
                    tbcErrorContrasenia.Visibility = Visibility.Hidden;
                    return true;
                }
                else
                {
                    tbcErrorContrasenia.Visibility = Visibility.Visible;
                    return false;
                }
            }
            else
            {
                tbcErrorContrasenia.Visibility = Visibility.Visible;
                return false;
            }
        }

        public bool CorreoEsValido(string correo)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(correo);
                if (addr.Address == correo)
                {
                    if (servicio.ValidarCorreoNoRepetido(correo))
                    {
                        tbcErrorCorreo.Visibility = Visibility.Hidden;
                        return true;
                    } 
                    else
                    {
                        tbcErrorCorreo.Text = ("Correo ya ocupado!");
                        tbcErrorCorreo.Visibility = Visibility.Visible;
                        return false;
                    }
                }
                else
                {
                    tbcErrorCorreo.Text = "Formato inválido!";
                    tbcErrorCorreo.Visibility = Visibility.Visible;
                    return false;
                }
            }
            catch
            {
                tbcErrorCorreo.Text = "Formato inválido!";
                tbcErrorCorreo.Visibility = Visibility.Visible;
                return false;
            }
        }
    }
}
